using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace Fotootof.HttpServer
{
    /// <summary>
    /// Class Http Request.
    /// </summary>
    public class HttpRequest : WebServerRequest
    {

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Variable path to get route.
        /// </summary>
        // private readonly string _dll = "XtrmAddons.Fotootof.Lib.{0}";
        private readonly string _dll = "Fotootof.Plugin.{0}";

        /// <summary>
        /// Variable object router.
        /// </summary>
        private object _router = null;


        private Dictionary<string, Type> _mapping = new Dictionary<string, Type>();


        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="ctx"></param>
        public HttpRequest(HttpListenerContext ctx) : base(ctx) { }


        /// <summary>
        /// 
        /// </summary>
        public string Namespace { get; } = "Fotootof.Plugin.{0}.Router.{1}Route";

        /// <summary>
        /// 
        /// </summary>
        public string ComponentPath => 
            string.Format(Namespace, Uri.RequestType.ToLower().UCFirst(), Uri.ComponentName.ToLower().UCFirst());

        /// <summary>
        /// 
        /// </summary>
        public string Dll =>
            string.Format(_dll, Uri.RequestType.ToLower().UCFirst());

        /// <summary>
        /// Accessors Server Component.
        /// </summary>
        /// <exception cref="MemberAccessException"></exception>
        public object Component => GetComponent();

        /// <summary>
        /// Method to get Server Component.
        /// </summary>
        /// <exception cref="MemberAccessException"></exception>
        private object GetComponent()
        {
            try
            {
                if (_mapping.ContainsKey(ComponentPath).Equals(false))
                {
                    _mapping.Add(ComponentPath, Type.GetType(ComponentPath + "," + Dll));
                }

                Type classType = _mapping[ComponentPath];

                return Activator.CreateInstance(classType, Uri);
            }
            catch (Exception e)
            {
                string message = string.Format("Cannot create component [{0}] instance : ", ComponentPath);
                log.Debug(message);
                log.Debug(e.GetType() + " : " + e.Message);

                throw new ApplicationException(message + e.Message, e);
            }
        }

        /// <summary>
        /// Accessors Server Component route method.
        /// </summary>
        /// <exception cref="InvalidDataException"></exception>
        private MethodInfo ComponentMethodRoute
        {
            get
            {
                MethodInfo route = null;

                try
                {
                    route = ComponentPath.ToType().GetMethod(Uri.MethodName ?? "Index");
                }
                catch (Exception e)
                {
                    string message = string.Format("Cannot create component route method {0}.{1} : ", ComponentPath, Uri.MethodName);
                    log.Debug(message);
                    log.Debug(e.GetType() + " : " + e.Message);

                    throw new InvalidDataException(message + e.Message, e);
                }

                return route;
            }
        }

        /// <summary>
        /// Accessors Server response.
        /// </summary>
        public WebServerResponseData Response
        {
            get
            {
                return GetResponseData();
            }
        }

        /// <summary>
        /// Method to get the default response of the server.
        /// </summary>
        private WebServerResponseData GetDefaultResponse()
        {
            log.Warn("Trying to serve default Http response for default or special requests.");

            WebServerResponseData response = null;

            try
            {
                // Check for special ico request.
                if (Uri.ComponentName == "Index" && Uri.Extension == ".ico")
                {
                    log.Debug("Serving Http response for special .ico request.");

                    response = new WebServerResponseData(Uri.RelativeUrl);
                    response.ServeFile(@"Assets\Images\Icons\Favicon.ico");
                    return response;
                }

                // Create url filename.
                string filename = Uri.RelativeUrl;

                // Check valid url default format.
                if (Uri.RelativeUrl == "" || Uri.RelativeUrl == "/")
                {
                    log.Debug($"Initialize default filename format.");
                    filename = "/index.html";
                }

                // Serve direct link.
                if (Uri.Extension != "" || filename == "/index.html")
                {
                    log.Debug("Serving Http response for default server link [Empty | / | index.html].");

                    response = new WebServerResponseData(Uri.RelativeUrl);
                    response.ServeFile(filename, "Public");
                    return response;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Output(), ex);
                return null;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private WebServerResponseData GetResponseData()
        {
            log.Info($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {Uri.RelativeUrl}");
            log.Debug($"Uri.ComponentName : {Uri.ComponentName}");;
            log.Debug($"Uri.Extension : {Uri.Extension}");

            // Try to get default server response.
            WebServerResponseData response = GetDefaultResponse();

            if(response != null)
            {
                return response;
            }

            log.Warn("Trying to serve Http response for generated URL document.");

            try
            {
                object[] post = null;

                if (IsPOST)
                {
                    post = new object[] { _POST };

                    log.Info($"Http method invoke : {ComponentMethodRoute}, {Component}, {_POST.Count}, {post.Length}");
                }
                else
                {
                    log.Info($"Http method invoke : {ComponentMethodRoute}, {Component}");
                }
                
                _router = ComponentMethodRoute.Invoke(Component, post);
            }
            catch (Exception e)
            {
                log.Debug("Invoke component method failed.");
                log.Debug(e.Output(), e);
                log.Debug($"URI AbsoluteUrl : {Uri.AbsoluteUrl}");
                log.Debug($"URI ComponentName : {Uri.ComponentName}");
                log.Debug($"Component : {Component}");
                log.Debug($"ComponentMethodRoute : {ComponentMethodRoute}");

                if (Uri.Params != null)
                {
                    log.Debug($"URI Params Length : {Uri.Params.Length}");
                }
                else
                {
                    log.Debug($"URI Params Length : 0");
                }

                log.Debug("End server request processed with error...");

                throw new InvalidDataException(e.Message, e);
            }

            log.Debug("End server request process...");

            response = (WebServerResponseData)_router;

            return response;





            /*
            try
            {
                log.Debug("Serving Http response for direct link.");

                string filename = Uri.RelativeUrl;
               
                if (Uri.RelativeUrl == "" || Uri.RelativeUrl == "/")
                {
                    log.Debug("Empty or / URL => /index.html");

                    filename = "/index.html";
                }

                if (Uri.Extension != "" || filename == "/index.html")
                {
                    log.Debug("Empty or / URL => [Public]/index.html");

                    response = new WebServerResponseData(Uri.RelativeUrl);
                    response.ServeFile(filename, "Public");
                    return response;
                }

                log.Info("Serving Http response for direct link => FileNotFoundException.");

                throw new FileNotFoundException();
            }

            // Try to generate URL document if file is not found.
            #pragma warning disable CS0168
            catch (FileNotFoundException fe)
            {
                log.Info("Serving Http response for generated URL document", fe);

                try
                {
                    object[] post = new object[] { };

                    if (IsPOST)
                    {
                        string data = _POST;
                        post = new object[] { data };
                    }


                    if (IsPOST)
                        _router = ComponentMethodRoute.Invoke(Component, post);
                    else
                        _router = ComponentMethodRoute.Invoke(Component, null);
                }
                catch (Exception e)
                {
                    log.Debug("Invoke component method failed.");
                    log.Debug(e.GetType() + " : " + e.Message);
                    log.Debug(string.Format("URL : {0}", Uri.AbsoluteUrl));
                    log.Debug(string.Format("URI : {0}", Uri.RelativeUrl));
                    log.Debug(string.Format("ComponentName : {0}", Uri.ComponentName));
                    log.Debug(string.Format("ViewName : {0}", Uri.MethodName));
                    log.Debug(string.Format("Component : {0}", Component));
                    log.Debug(string.Format("ComponentMethodRoute : {0}", ComponentMethodRoute));
                    if (Uri.Params != null)
                        log.Debug(string.Format("Params : {0}", Uri.Params.Length));
                    else
                        log.Debug("Params : 0");

                    log.Debug("End server request process with error...");

                    throw new InvalidDataException(e.Message, e);
                }

                log.Debug("End server request process...");
            }

            // Return Response error server 500.
            catch (Exception e)
            {
                return null;
            }
            */
        }
    }
}