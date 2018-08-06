using Fotootof.Libraries.Collections.Entities;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Windows.Slideshow
{
    public class WindowSlideshowModel : WindowLayoutModel<WindowSlideshowLayout>
    {
        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private PictureEntityCollection pictures;

        /// <summary>
        /// 
        /// </summary>
        private PictureEntity currentPicture;

        /// <summary>
        /// 
        /// </summary>
        private int delay = 3000;

        /// <summary>
        /// 
        /// </summary>
        private int startIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        private int currentIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        private bool isPlaying = false;

        #endregion



        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public PictureEntityCollection Pictures
        {
            get => pictures;
            set
            {
                pictures = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PictureEntity CurrentPicture
        {
            get => currentPicture;
            set
            {
                if(value != null)
                {
                    value.OriginalPath = !value.OriginalPath.IsNullOrWhiteSpace() ? value.OriginalPath : value.PicturePath;
                    value.OriginalWidth = value.OriginalWidth == 0 ? value.OriginalWidth : value.PictureWidth;
                    value.OriginalHeight = value.OriginalHeight == 0 ? value.OriginalHeight : value.PictureHeight;
                }

                currentPicture = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Delay
        {
            get => delay;
            set
            {
                delay = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPlaying
        {
            get => isPlaying;
            set
            {
                isPlaying = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StartIndex
        {
            get => startIndex;
            set
            {
                startIndex = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentIndex
        {
            get => currentIndex;
            set
            {
                currentIndex = value;
                NotifyPropertyChanged();
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class XtrmAddons Fotootof Component Page Slideshow Model Constructor.
        /// </summary>
        /// <param name="pageBase"></param>
        public WindowSlideshowModel(WindowSlideshowLayout pageBase)
            : base(pageBase) { }

        #endregion



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public async void NextAsync()
        {
            ResetZoom();

            if (CurrentIndex >= Pictures.Count - 1)
            {
                CurrentIndex = 0;
            }
            else
            {
                CurrentIndex++;
            }

            if (CurrentIndex >= 0 && CurrentIndex < Pictures.Count)
            {
                CurrentPicture = Pictures[CurrentIndex];
            }
            else
            {
                CurrentIndex = 0;
            }

            if (IsPlaying)
            {
                await Task.Delay(Delay);
                NextAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e"></param>
        public async void PreviewAsync()
        {
            ResetZoom();

            if (CurrentIndex <= 0)
            {
                CurrentIndex = Pictures.Count - 1;
            }
            else
            {
                CurrentIndex--;
            }

            if (CurrentIndex >= 0 && CurrentIndex < Pictures.Count)
            {
                CurrentPicture = Pictures[CurrentIndex];
            }
            else
            {
                CurrentIndex = 0;
            }

            if (IsPlaying)
            {
                await Task.Delay(Delay);
                PreviewAsync();
            }
        }

        /// <summary>
        /// Method to reset the image zoom.
        /// </summary>
        private void ResetZoom()
        {
            ((ZoomBorder)ControlView.GetPropertyValue("CurrentZoom")).Reset();
        }

        /// <summary>
        /// Method to start playing slideshow asynchronous.
        /// </summary>
        /// <param name="sender">The object sender of the event.</param>
        /// <param name="e">Mouse button event arguments</param>
        public async void PlayAsync()
        {
            IsPlaying = true;
            await Task.Delay(Delay);
            NextAsync();
        }
    }

    #endregion
}