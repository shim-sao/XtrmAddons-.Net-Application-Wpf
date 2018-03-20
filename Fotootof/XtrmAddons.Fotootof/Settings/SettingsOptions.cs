using System.Diagnostics;
using System.Threading.Tasks;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Application.Serializable.Elements.XmlRemote;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Network;

namespace XtrmAddons.Fotootof.Settings
{
    /// <summary>
    /// Class XtrmAddons Fotootof Settings Preferences.
    /// </summary>
    public static class SettingsOptions
    {
        /// <summary>
        /// Method to initialize server application.
        /// </summary>
        public static async Task<Server> InitializeServerAsync()
        {            
            return await Task.Run(() =>
            {
                Server server = ApplicationBase.Options.Remote.Servers.FindDefault();

                // Create default server parameters if not exists.
                if (server == null || server.Key == null)
                {
                    server = new Server
                    {
                        Key = "default",
                        Name = "Default Server",
                        Default = true
                    };

                    ApplicationBase.Options.Remote.Servers.AddDefaultUnique(server);
                }


                // Initialize web server host or ip address.
                if (server.Host.IsNullOrWhiteSpace())
                {
                    server.Host = NetworkInformations.GetLocalHostIp();
                    ApplicationBase.Options.Remote.Servers.ReplaceKeyDefaultUnique(server);
                }

                // Initialize web server port.
                if (server.Port.IsNullOrWhiteSpace())
                {
                    server.Port = NetworkInformations.GetAvailablePort(9293).ToString();
                    ApplicationBase.Options.Remote.Servers.ReplaceKeyDefaultUnique(server);
                }

                server = ApplicationBase.Options.Remote.Servers.FindDefault();
                Trace.WriteLine("Server address : http://" + server.Host + ":" + server.Host);

                return server;
            });
        }
    }
}
