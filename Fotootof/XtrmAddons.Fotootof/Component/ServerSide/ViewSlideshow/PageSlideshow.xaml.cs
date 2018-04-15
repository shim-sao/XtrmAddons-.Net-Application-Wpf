using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XtrmAddons.Fotootof.Lib.Base.Classes.Pages;

namespace XtrmAddons.Fotootof.Component.ServerSide.ViewSlideshow
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class PageSlideshow : PageBase
    {
        #region Properties

        /// <summary>
        /// Property to access to the page album model.
        /// </summary>
        public PageSlideshowModel<PageSlideshow> Model { get; private set; }

        /// <summary>
        /// Property to access to the album id.
        /// </summary>
        private int ItemId { get; }

        #endregion



        #region Variables

        /// <summary>
        /// Variable list of pictures.
        /// </summary>
        private ObservableCollection<Classes.UI.BitmapSource> ListPictures = SlideshowSession.ListPictures;

        /// <summary>
        /// Variable list index to start.
        /// </summary>
        private int Start = SlideshowSession.Start;

        /// <summary>
        ///  Variable current list index.
        /// </summary>
        private int Current = SlideshowSession.Current;

        /// <summary>
        /// Variable store if slide show is running
        /// </summary>
        private bool isRunning = false;

        /// <summary>
        /// Variable delay for slide show
        /// </summary>
        private int delay = 3000;

        #endregion



        #region Methods
        
        /// <summary>
        /// 
        /// </summary>
        public PageSlideshow()
        {
            InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            
            Display();
        }

        #endregion



        private void ToggleFullScreenModeButton()
        {
            ApplicationView view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
                // The SizeChanged event will be raised when the exit from full-screen mode is complete.
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                    // The SizeChanged event will be raised when the entry to full-screen mode is complete.
                }
            }
        }

        private void ToggleFullScreenModeButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleFullScreenModeButton();
        }

        #region ---------- Display ----------
        private void Display()
        {
            if (ListPictures.Count > 0)
            {
                DisplayPicture(Start);
            }
        }

        private void DisplayPicture(int index)
        {
            BitmapImage hack = ListPictures[index].BitmapImage;
            UIE_I_Picture.Source = ListPictures[index].BitmapImage;
        }
        #endregion ---------- Display ----------


        private void CoreWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.PageDown:
                case VirtualKey.Down:
                case VirtualKey.Right:
                    ShowNext();
                    break;

                case VirtualKey.PageUp:
                case VirtualKey.Up:
                case VirtualKey.Left:
                case VirtualKey.Back:
                    ShowPreview();
                    break;
                    
                case VirtualKey.Enter:
                case VirtualKey.Space:
                    if (!isRunning)
                        StartSlide(false);
                    else
                        PauseSlide();
                    break;

                case VirtualKey.Escape:
                    StopSlide();
                    break;
            }
        }

        private void HistoryBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }


        private void OnPicturePointerReleased(object sender, PointerRoutedEventArgs e)
        {
            delay = 3000;
            StartSlide();
        }

        private void StartSlide(bool ToggleFullScreenMode = true)
        {
            if (ToggleFullScreenMode)
                ToggleFullScreenModeButton();

            if (isRunning)
            {
                StopSlide();
            }
            else
            {
                InitSlide();
            }
        }

        private async void InitSlide()
        {
            isRunning = true;

            UIE_B_MainMenu.Visibility = Visibility.Collapsed;
            UIE_B_Picture.Margin = new Thickness(0, 0, 0, 0);

            UIE_B_Preview.Visibility = Visibility.Collapsed;
            UIE_B_Next.Visibility = Visibility.Collapsed;

            await Task.Delay(delay);
            await slide();
        }

        private void StopSlide()
        {
            isRunning = false;

            UIE_B_MainMenu.Visibility = Visibility.Visible;
            UIE_B_Picture.Margin = new Thickness(0, 60, 0, 0);

            UIE_B_Preview.Visibility = Visibility.Visible;
            UIE_B_Next.Visibility = Visibility.Visible;
        }

        private void PauseSlide()
        {
            isRunning = false;
        }

        private async Task slide()
        {
            if (isRunning)
            {
                if (Current > ListPictures.Count - 1)
                {
                    Current = 0;
                }

                DisplayPicture(Current);

                await Task.Delay(delay);

                Current++;

                await slide();
            }
        }


        private void SlideShow_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            delay = Int32.Parse(b.Content.ToString()) * 1000;
            StartSlide();
        }

        private void ShowPreview_Click(object sender, RoutedEventArgs e)
        {
            ShowPreview();
        }

        private void ShowPreview()
        {
            Current--;

            if (Current < 0)
            {
                Current = ListPictures.Count - 1;
            }

            DisplayPicture(Current);
        }

        private void ShowNext_Click(object sender, RoutedEventArgs e)
        {
            ShowNext();
        }

        private void ShowNext()
        {
            Current++;

            if (Current > ListPictures.Count - 1)
            {
                Current = 0;
            }

            DisplayPicture(Current);
        }


        #region Page Navigation

        /// <summary>
        /// Method to show setting view.
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event</param>
        private void ShowViewSetting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingView));
        }

        #endregion
    }
}
