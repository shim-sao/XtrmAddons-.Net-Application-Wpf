using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using XtrmAddons.Net.Common.Extensions;
using XtrmAddons.Net.Picture.Classes;
using XtrmAddons.Net.Picture.Extensions;

namespace Fotootof.Components.Server.Browser.Layouts.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    internal class TreeViewItemDirectoryInfo : TreeViewItem
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
        public TreeViewItemDirectoryInfo(DirectoryInfo di)
        {
            // Get the icon image of the Drive.
            BitmapImage icon = Win32Icon.IconFromHandle(di.FullName).ToBitmap().ToBitmapImage();

            double opacity = 1;
            if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                opacity = 0.5;
            }

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
                },
                Opacity = opacity
            };

            // Create Drive special informations.
            string inf = "NaN";
            try
            {
                inf = di.GetFiles().Length.ToString();
                inf = $"{inf}/{di.GetDirectories().Length.ToString()}";
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
