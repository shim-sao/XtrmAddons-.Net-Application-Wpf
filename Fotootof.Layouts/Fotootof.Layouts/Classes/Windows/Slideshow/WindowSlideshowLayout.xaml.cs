using Fotootof.Collections.Entities;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fotootof.Layouts.Windows.Slideshow
{
    /// <summary>
    /// Class XtrmAddons Fotootof Layouts Windows Slideshow.
    /// </summary>
    public partial class WindowSlideshowLayout : Window
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
        /// Property to access to the <see cref="WindowSlideshowModel"/>.
        /// </summary>
        public WindowSlideshowModel Model { get; private set; }

        /// <summary>
        /// Property to access to the <see cref="ZoomBorder"/>.
        /// </summary>
        public ZoomBorder CurrentZoom
            => (ZoomBorder)FindName("ZoomBorderCurrentPictureName");

        #endregion



        #region Constructors

        /// <summary>
        /// Class XtrmAddons Fotootof Layouts Windows Constructor.
        /// </summary>
        /// <param name="collection">A <see cref="PictureEntityCollection"/>.</param>
        /// <param name="picture">A <see cref="PictureEntity"/>.</param>
        public WindowSlideshowLayout(PictureEntityCollection collection, PictureEntity picture)
        {
            Theme.ThemeLoader.MergeThemeTo(Resources);

            InitializeComponent();

            Model = new WindowSlideshowModel(this)
            {
                Pictures = collection,
                CurrentPicture = picture,
                CurrentIndex = collection.IndexOf(picture)
            };
        }
        
        #endregion



        #region Methods

        /// <summary>
        /// Method called on <see cref="Window"/> <see cref="FrameworkElement"/> loaded event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed event arguments <see cref="RoutedEventArgs"/></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

        /// <summary>
        /// Method called on <see cref="UIElement"/> menu top mouse enter event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseEventArgs"/>.</param>
        private void MenuTop_MouseEnter(object sender, MouseEventArgs e)
        {
            ((UIElement)FindName("GridMenuTopName")).Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method called on <see cref="UIElement"/> menu top mouse leave event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseEventArgs"/>.</param>
        private void MenuTop_MouseLeave(object sender, MouseEventArgs e)
        {
            ((UIElement)FindName("GridMenuTopName")).Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Method called on <see cref="UIElement"/> menu bottom mouse enter event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseEventArgs"/>.</param>
        private void MenuBottom_MouseEnter(object sender, MouseEventArgs e)
        {
            ((UIElement)FindName("GridMenuBottomName")).Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Method called on <see cref="UIElement"/> menu bottom mouse enter event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseEventArgs"/>.</param>
        private void MenuBottom_MouseLeave(object sender, MouseEventArgs e)
        {
            ((UIElement)FindName("GridMenuBottomName")).Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/>.</param>
        private void OnMinus_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double dw = ImagePicture.ActualWidth * 0.80;
            double dh = ImagePicture.ActualHeight * 0.80;

            if (dw >= 24 && dh >= 24)
            {
                ImagePicture.Width = dw;
            }
        }

        /// <summary>
        /// Method called on ..C.. mouse button event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/></param>
        private void OnPlus_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double dw = ImagePicture.ActualWidth * 1.2;
            double dh = ImagePicture.ActualHeight * 1.2;

            if (dw <= 40000 && dh <= 40000)
            {
                ImagePicture.Width = dw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/>.</param>
        private void OnPlay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Model.PlayAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/>.</param>
        private void OnRefresh_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var root = (Grid)FindName("GridBlockRootName");
            ImagePicture.Width = Math.Max(root.ActualWidth, Model.CurrentPicture.OriginalWidth);
            ImagePicture.Height = Math.Max(root.ActualHeight, Model.CurrentPicture.OriginalHeight);
            CurrentZoom.Reset();
        }

        /// <summary>
        /// Method called on stop preview mouse button event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/></param>
        private void OnStop_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Model.IsPlaying = false;
        }

        /// <summary>
        /// Method called on preview Picture preview mouse button event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/></param>
        private void PicturePreview_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Model.PreviewAsync();
        }

        /// <summary>
        /// Method called on next Picture preview mouse button event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/></param>
        private void PictureNext_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Model.NextAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The routed property changed event arguments <see cref="RoutedPropertyChangedEventArgs{T}"/>.</param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(Model != null)
            {
                Model.Delay = 1000 * (int)((Slider)sender).Value;
            }
        }
        
        #endregion
    }
}
