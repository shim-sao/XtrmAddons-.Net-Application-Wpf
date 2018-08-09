using Fotootof.Libraries.HttpHelpers.HttpServer;
using Fotootof.Libraries.Models;
using System;
using System.Windows.Controls;
using XtrmAddons.Fotootof.Lib.HttpServer;

namespace Fotootof.Menus
{
    /// <summary>
    /// Class Fotootof Main Menu Horizontal Model.
    /// </summary>
    public class MainMenuHorizontalModel : ModelBase<MainMenuHorizontalLayout>
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 
        /// </summary>
        public bool isServerStarted;

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private Grid moduleContainer = new Grid();

        /// <summary>
        /// 
        /// </summary>
        public Grid ModuleContainer
        {
            get
            {
                return moduleContainer;
            }
            set
            {
                if(moduleContainer != value)
                {
                    moduleContainer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsServerStarted
        {
            get
            {
                return isServerStarted;
            }
            set
            {
                if (isServerStarted != value)
                {
                    isServerStarted = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("IsServerStopped");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsServerStopped => !IsServerStarted;

        #endregion


        #region Constructor

        /// <summary>
        /// Class Fotootof Main Menu Horizontal Model Constructor.
        /// </summary>
        public MainMenuHorizontalModel(MainMenuHorizontalLayout controlView) : base(controlView)
        {
            HttpServerBase.NotifyServerStartedHandlerOnce += HttpServerBase_NotifyServerHandlerOnce;
            HttpServerBase.NotifyServerStoppedHandlerOnce += HttpServerBase_NotifyServerHandlerOnce;
            IsServerStarted = HttpWebServerApplication.IsStarted;
        }

        #endregion


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HttpServerBase_NotifyServerHandlerOnce(object sender, EventArgs e)
        {
            IsServerStarted = HttpWebServerApplication.IsStarted;
        }

        #endregion
    }
}