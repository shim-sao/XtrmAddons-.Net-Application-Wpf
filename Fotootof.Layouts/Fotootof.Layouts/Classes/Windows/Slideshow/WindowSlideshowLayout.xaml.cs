using Fotootof.Collections.Entities;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fotootof.Layouts.Windows.Slideshow
{
    /// <summary>
    /// Class Fotootof Layouts Windows Slideshow.
    /// </summary>
    public partial class WindowSlideshowLayout : Window
    {
        /// <summary>
        /// Property to access to the <see cref="WindowSlideshowModel"/>.
        /// </summary>
        public WindowSlideshowModel Model { get; private set; }

        /// <summary>
        /// Accessors page slideshow view model.
        /// </summary>
        public ZoomBorder CurrentZoom
            => FindName("ZoomBorderCurrentPictureName") as ZoomBorder;

        /// <summary>
        /// Class Fotootof Layouts Windows Slideshow Constructor.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="picture"></param>
        public WindowSlideshowLayout(PictureEntityCollection collection, PictureEntity picture)
        {
            InitializeComponent();

            Model = new WindowSlideshowModel(this)
            {
                Pictures = collection,
                CurrentPicture = picture,
                CurrentIndex = collection.IndexOf(picture)
            };
        }

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
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Mouse button event arguments</param>
        private void OnMenuTopMouseEnter(object sender, MouseEventArgs e)
        {
            (FindName("StackPanelMenuTop") as StackPanel).Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Mouse button event arguments</param>
        private void OnMenuTopMouseLeave(object sender, MouseEventArgs e)
        {
            (FindName("StackPanelMenuTop") as StackPanel).Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Mouse button event arguments</param>
        private void OnMenuBottomMouseEnter(object sender, MouseEventArgs e)
        {
            (FindName("StackPanelMenuBottom") as StackPanel).Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Mouse button event arguments</param>
        private void OnMenuBottomMouseLeave(object sender, MouseEventArgs e)
        {
            (FindName("StackPanelMenuBottom") as StackPanel).Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Mouse button event arguments</param>
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
        /// <param name="e">Mouse button event arguments</param>
        private void OnPlay_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Model.PlayAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">Mouse button event arguments</param>
        private void OnRefresh_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ImagePicture.Width = Math.Max(GridSlideshow.ActualWidth, Model.CurrentPicture.OriginalWidth);
            ImagePicture.Height = Math.Max(GridSlideshow.ActualHeight, Model.CurrentPicture.OriginalHeight);
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
        /// Method called on preview preview mouse button event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/></param>
        private void OnPreview_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Model.PreviewAsync();
        }

        /// <summary>
        /// Method called on next preview mouse button event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e">The Mouse button event arguments <see cref="MouseButtonEventArgs"/></param>
        private void OnNext_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Model.NextAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The <see cref="object"/> sender of the event.</param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(Model != null)
            {
                Model.Delay = 1000 * (int)((Slider)sender).Value;
            }
        }
    }
}
