using Fotootof.Layouts.Dialogs;
using Fotootof.Libraries.Windows;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Forms;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.SystemIO;

namespace Fotootof.Layouts.Settings
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts Settings.
    /// </summary>
    public partial class WindowSettingsLayout : WindowLayoutForm
    {
        #region Variables

        /// <summary>
        /// Variable logger <see cref="log4net.ILog"/>.
        /// </summary>
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public new WindowSettingsModel Model
        {
            get => (WindowSettingsModel)model;
            protected set { model = value; }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Settings Constructor.
        /// </summary>
        public WindowSettingsLayout()
        {
            // Initialize window component.
            InitializeComponent();

            // Initialize window data model.
            InitializeModel();
        }

        #endregion



        #region Methods

        /// <summary>
        /// Method called on Window loaded event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Routed event atguments.</param>
        protected void Window_Load(object sender, RoutedEventArgs e)
        {
            // Add model to data context for binding.
            DataContext = Model;
        }

        /// <summary>
        /// Method to initialize the Model on Windows construct.
        /// </summary>
        protected void InitializeModel()
        {
            Model = new WindowSettingsModel(this);
        }

        /// <summary>
        /// Method called to open directory in Explorer on FrameworkElement Click event.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void OpenDirectoryInExplorer_Click(object sender, RoutedEventArgs e)
        {
            // Get the path to the directory.
            string path = (string)((FrameworkElement)sender).Tag;

            // Check if the directory exists.
            // Check if user has the right to access to the directory.
            try
            {
                DirectoryInfo di = new DirectoryInfo(@path);
                if (di.HasDirectoryPermissions(FileSystemRights.Read))
                {
                    Process.Start("explorer", di.FullName);
                }
                else
                {
                    MessageBoxs.Warning("Directory access denied !");
                }
            }
            catch (Exception ex)
            {
                MessageBoxs.Error(ex);
                log.Error(ex.Output(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeUserDirectory_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                DialogResult result = dlg.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    if (Model.Directories.Base == dlg.SelectedPath)
                    {
                        return;
                    }


                    // Check if the directory exists.
                    // Check if user has the right to access to the directory.
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(@dlg.SelectedPath);
                        if (!di.HasDirectoryPermissions(FileSystemRights.Write))
                        {
                            MessageBoxs.Warning("You don't have the right to write in the the destination Directory !");
                            return;
                        }

                        if(!SysDirectory.IsDirectoryEmpty(dlg.SelectedPath))
                        {
                            MessageBoxs.Warning("The destination Directory is not empty !");
                            //return;

                           // MessageBoxResult process = System.Windows.MessageBox.Show
                           // (
                           //     "To take effect of the User Directory changes the application will be restarted. Continue ?",
                           //     Translation.DWords.ApplicationName,
                           //     MessageBoxButton.YesNoCancel,
                           //     MessageBoxImage.Question
                           //);
                        }

                        string oldBase = Model.Directories.Base;
                        Model.Directories.Base = dlg.SelectedPath;

                        Model.Directories.Cache = Model.Directories.Cache.Replace(oldBase, dlg.SelectedPath);
                        Model.Directories.Config = Model.Directories.Config.Replace(oldBase, dlg.SelectedPath);
                        Model.Directories.Data = Model.Directories.Data.Replace(oldBase, dlg.SelectedPath);
                        Model.Directories.Theme = Model.Directories.Theme.Replace(oldBase, dlg.SelectedPath);
                    
                        Model.Database.Source = Model.Database.Source.Replace(oldBase, dlg.SelectedPath);

                        ApplicationBase.Save();

                        SysDirectory.Copy(oldBase, dlg.SelectedPath);

                        MessageBoxs.Warning("To take effect of the User Directory changes the application will be restarted.");

                        System.Windows.Forms.Application.Restart();
                        System.Windows.Application.Current.Shutdown();
                    }
                    catch (Exception ex)
                    {
                        MessageBoxs.Error(ex);
                        log.Error(ex.Output(), ex);
                    }
                }
            }
        }

        #endregion
    }
}
