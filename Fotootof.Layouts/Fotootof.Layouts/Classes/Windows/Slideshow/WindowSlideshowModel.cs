using Fotootof.Collections.Entities;
using Fotootof.Libraries.Windows;
using Fotootof.SQLite.EntityManager.Data.Tables.Entities;
using System.Threading.Tasks;
using XtrmAddons.Net.Common.Extensions;

namespace Fotootof.Layouts.Windows.Slideshow
{
    /// <summary>
    /// Class Fotootof Layouts Windows Slideshow.
    /// </summary>
    public class WindowSlideshowModel : WindowLayoutModel<WindowSlideshowLayout>
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
                if(pictures != value)
                {
                    pictures = value;
                    NotifyPropertyChanged();
                }
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
                if (currentPicture != value)
                {
                    if (value != null)
                    {
                        value.OriginalPath = !value.OriginalPath.IsNullOrWhiteSpace() ? value.OriginalPath : value.PicturePath;
                        value.OriginalWidth = value.OriginalWidth == 0 ? value.OriginalWidth : value.PictureWidth;
                        value.OriginalHeight = value.OriginalHeight == 0 ? value.OriginalHeight : value.PictureHeight;
                    }

                    currentPicture = value;
                    NotifyPropertyChanged();
                }
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
                if (delay != value)
                {
                    delay = value;
                    NotifyPropertyChanged();
                }
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
                if (isPlaying != value)
                {
                    isPlaying = value;
                    NotifyPropertyChanged();
                }
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
                if (startIndex != value)
                {
                    startIndex = value;
                    NotifyPropertyChanged();
                }
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
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion



        #region Constructor

        /// <summary>
        /// Class Fotootof Layouts Windows Slideshow Constructor.
        /// </summary>
        /// <param name="controlView">The page associated to the model.</param>
        public WindowSlideshowModel(WindowSlideshowLayout controlView) : base(controlView) { }

        #endregion



        #region Constructor

        /// <summary>
        /// Method to move to the next Picture.
        /// </summary>
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
        /// Method to move to the preview Picture.
        /// </summary>
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
        public async void PlayAsync()
        {
            IsPlaying = true;
            await Task.Delay(Delay);
            NextAsync();
        }
    }

    #endregion
}