using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Picture.Classes;
using XtrmAddons.Net.Picture.Extensions;

namespace XtrmAddons.Fotootof.SideServer.Layouts.TreeViews.SystemStorage
{
    /// <summary>
    /// 
    /// </summary>
    internal class TreeViewItemDriveInfo : TreeViewItem
    {
        #region Variables
        
        /// <summary>
        /// Variable logger.
        /// </summary>
        private static readonly log4net.ILog log =
        	log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="di"></param>
        public TreeViewItemDriveInfo(DriveInfo di)
        {
            // Get the icon image of the Drive.
            BitmapImage icon = Win32Icon.IconFromHandle(di.Name).ToBitmap().ToBitmapImage();

            // Create the main content Grid.
            Grid header = new Grid();
            header.Height = 20;
            ColumnDefinition gr1 = new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) };
            header.ColumnDefinitions.Add(gr1);
            header.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            // Create the Title.
            StackPanel title = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Height = 20,
                Children =
                {
                    new Border()
                    {
                        Child = new Image
                        {
                            Width = icon.Width,
                            Height = icon.Height,
                            Source = icon
                        }
                    },

                    new TextBlock
                    {
                        Text = di.Name.ToString(),
                        Margin = new Thickness(5,0,0,0)
                    }
                }
            };

            // Create Drive special informations.
            string inf = "NaN";
            try
            {
                inf = di.RootDirectory.GetFiles().Length.ToString();
                inf = $"{inf}/{di.RootDirectory.GetDirectories().Length.ToString()}";
                inf = $"{inf} - {di.AvailableFreeSpace / (1024 * 1024 * 1024)}/{di.TotalSize / (1024 * 1024 * 1024)}Go";
            }
            catch (Exception e)
            {
                log.Debug(e.Output(), e);
                log.Debug($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name} : {di?.Name}");
            }

            // Create TextBlock for the special informations.
            TextBlock count = new TextBlock
            {
                Text = inf,
                Margin = new Thickness(0, 0, 10, 0),
                FontStyle = FontStyles.Italic,
                FontSize = 10
            };

            Grid.SetColumn(title, 0);
            Grid.SetColumn(count, 1);
            header.Children.Add(title);
            header.Children.Add(count);

            Header = header;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            Tag = di;
            Style = Application.Current.Resources["TreeViewItemStyle"] as Style;

            Items.Add("Loading...");
        }
    }
}
