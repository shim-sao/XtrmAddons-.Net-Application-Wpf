using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using Fotootof.SQLite.EntityManager.Enums.EntityHelper;
using Fotootof.SQLite.EntityManager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XtrmAddons.Net.HttpWebServer.Requests;
using XtrmAddons.Net.HttpWebServer.Responses;

namespace XtrmAddons.Fotootof.Lib.Api.Router
{
    /// <summary>
    /// Class XtrmAddons Fotootof Libraries Api Router Sections.
    /// </summary>
    public class SectionRoute : Router
    {
        #region Variables

        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router Sections.
        /// </summary>
        public SectionRoute() : base() { }

        /// <summary>
        /// Class XtrmAddons Fotootof Libraries Api Router Sections.
        /// </summary>
        /// <param name="uri">The Uri to parse.</param>
        public SectionRoute(WebServerRequestUrl uri) : base(uri) { }

        /// <summary>
        /// Method to get a list of associated Sections of an User.
        /// </summary>
        /// <returns>WPF Web Server response data of list of Sections.</returns>
        public override WebServerResponseData Index()
        {
            log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            log.Info($"{MethodBase.GetCurrentMethod().Name} : Serving section request. Please wait...");

            if (!IsAuth())
            {
                log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response user not auth.");
                return ResponseNotAuth();
            }
            
            try
            {
                var a = Uri.QueryString;

                // Check if request params are correct.
                if(Uri.QueryString.Count == 0)
                {
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Request empty.");
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response => Section not found or doesn't exists.");
                    return ResponseError404("Section not found or doesn't exists.");
                }

                // Check if request params are correct.
                if(!Uri.QueryString.Keys.Cast<string>().Contains("id"))
                {
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Request param [id] not fount.");
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response => Section not found or doesn't exists.");
                    return ResponseError404("Section not found or doesn't exists.");
                }

                int id = int.Parse(Uri.QueryString["id"]);

                // Get user and dependencies.
                UserEntity user = GetAuthUser();
                SectionEntity entity = null;

                // Try to found section in dependencies.
                foreach (AclGroupEntity group in user.AclGroups)
                {
                    AclGroupEntity ag = Database.AclGroups.SingleOrDefault
                        (
                            new AclGroupOptionsSelect
                            {
                                PrimaryKey = group.PrimaryKey,
                                Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                            }
                        );

                    entity = ag.Sections.ToList().Find(x => x.PrimaryKey == id);

                    if (entity != null)
                    {
                        break;
                    }
                }

                // Check if folder is associated or not.
                if (entity == null)
                {
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Section not found in user association.");
                    log.Info($"{MethodBase.GetCurrentMethod().Name} : Return response Section not found or doesn't exists.");
                    return ResponseError404("Section not found or doesn't exists.");
                }

                // Ensure to select dependencies.
                log.Info("Api : Creating section informations. Please wait...");
                entity = Database.Sections.SingleOrNull
                    (
                        new SectionOptionsSelect
                        {
                            PrimaryKey = entity.PrimaryKey,
                            Dependencies = new List<EnumEntitiesDependencies> { EnumEntitiesDependencies.All }
                        }
                    );

                Content["Authentication"] = true;
                Content["Response"] = ConvertJsonAuthSection(entity);
                return ResponseContentToJson();
            }
            catch (Exception e)
            {
                log.Fatal($"{MethodBase.GetCurrentMethod().Name} : Serving section request failed.", e);
                return ResponseError500();
            }
        }
    }
}