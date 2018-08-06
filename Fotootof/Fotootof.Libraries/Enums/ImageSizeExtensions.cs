using System;
using System.Windows;

namespace Fotootof.Libraries.Enums
{
    /// <summary>
    /// Class Fotootof Libraries Enumerator Image Size Extensions.
    /// </summary>
    public static class ImageSizeExtensions
    {
        /// <summary>
        /// Method to get an image size value.
        /// </summary>
        /// <param name="imageSize">The image size.</param>
        /// <returns>The double value of the image size.</returns>
        public static double ToDouble(this ImageSize imageSize)
        {
            switch (imageSize)
            {
                case ImageSize.Icon:
                    return 32;

                case ImageSize.Thumbnail:
                    return 64;

                case ImageSize.Vignette:
                    return 96;

                case ImageSize.Large:
                    return 128;

                case ImageSize.XLarge:
                    return 256;

                default:
                    return 32;
            }
        }

        /// <summary>
        /// Method to get an image size to Size.
        /// </summary>
        /// <param name="imageSize">The image size.</param>
        /// <returns>The Size value of the image size.</returns>
        public static Size ToSize(this ImageSize imageSize)
        {
            return new Size(imageSize.ToDouble(), imageSize.ToDouble());
        }

        /// <summary>
        /// Method to get an image size value.
        /// </summary>
        /// <param name="ext">The image size.</param>
        /// <returns>The double value of the image size.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ImageSize Index(int index)
        {
            if (index.Equals(null))
            {
                throw new ArgumentNullException(nameof(index));
            }

            if (index < 0 || index > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            switch (index)
            {
                case 0:
                    return ImageSize.Icon;

                case 1:
                    return ImageSize.Thumbnail;

                case 2:
                    return ImageSize.Vignette;

                case 3:
                    return ImageSize.Large;

                case 4:
                    return ImageSize.XLarge;

                default:
                    return 0;
            }
        }
    }
}
