using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using XtrmAddons.Net.Application;
using XtrmAddons.Net.Picture.Extensions;
using XtrmAddons.Net.SystemIO;

namespace XtrmAddons.Fotootof.Lib.Base.Classes.Images
{
    public class ImageManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static Size SizeThumnail { get; set; } = new Size(256, 256);


        #region Generic

        /// <summary>
        /// <para>Method to get a path to a cache directory.</para>
        /// <para>Create directory if not exists.</para>
        /// </summary>
        /// <param name="relativeUrl">The relative url of the directory based from the cache directory.</param>
        /// <returns>The absolute path to the directory.</returns>
        public static async Task<string> GetDirectoryAsync(string relativeUrl)
        {
            return await SysDirectory.CreateAsync(ApplicationBase.Directories.Cache, relativeUrl);
        }

        /// <summary>
        /// <para>Method to get a path to a cache directory.</para>
        /// <para>Create directory if not exists.</para>
        /// </summary>
        /// <param name="relativeUrl">The relative url of the directory based from the cache directory.</param>
        /// <returns>The absolute path to the directory.</returns>
        public static string GetDirectory(string relativeUrl)
        {
            return SysDirectory.Create(ApplicationBase.Directories.Cache, relativeUrl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DateTime GetDateTaken(string filename)
        {
            dynamic d = filename.PictureMetadata().Data;

            DateTime date = DateTime.MinValue;
            if (d.DateTaken != null)
            {
                date = DateTime.Parse(d.DateTaken);
            }

            return date;
        }

        #endregion



        #region Album

        /// <summary>
        /// Method to get a path to all albums directory.
        /// <para>Create directory if not exists.</para>
        /// </summary>
        /// <returns>The absolute path to all albums directory.</returns>
        public static async Task<string> GetAlbumsDirectoryAsync()
        {
            return await GetDirectoryAsync("albums");
        }

        /// <summary>
        /// Method to get a path to all albums directory.
        /// <para>Create directory if not exists.</para>
        /// </summary>
        /// <returns>The absolute path to all albums directory.</returns>
        public static string GetAlbumsDirectory()
        {
            return GetDirectory("albums");
        }

        /// <summary>
        /// Method to get a path to a single album.
        /// <para>Create directory if not exists.</para>
        /// </summary>
        /// <param name="albumId">The primary key of the album.</param>
        /// <returns>The path to the album directory.</returns>
        public static async Task<string> GetAlbumDirectoryAsync(int albumId)
        {
            // Format Album path by Primary Key
            int root = (int)Math.Floor((double)(albumId / 1024));
            string path = Path.Combine(root.ToString().PadLeft(4, '0'), albumId.ToString().PadLeft(4, '0'));

            return await SysDirectory.CreateAsync(await GetAlbumsDirectoryAsync(), path);
        }

        /// <summary>
        /// Method to get a path to an album.
        /// </summary>
        /// <param name="albumId">The primary key of the album.</param>
        /// <returns>The path to the album directory.</returns>
        public static string GetAlbumImageAbsolutePath(int albumId, ImageType imgType, string fileExt)
        {
            return Path.Combine(GetAlbumsDirectory(), GetRelativePathAlbumImage(albumId, imgType, fileExt));
        }

        /// <summary>
        /// Method to get a path to an album.
        /// </summary>
        /// <param name="albumId">The primary key of the album.</param>
        /// <returns>The path to the album directory.</returns>
        public static async Task<string> GetAbsolutePathAlbumImageAsync(int albumId, ImageType imgType, string fileExt)
        {
            return Path.Combine(await GetAlbumsDirectoryAsync(), GetRelativePathAlbumImage(albumId, imgType, fileExt));
        }

        /// <summary>
        /// Method to get a path to an album.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static string GetAbsolutePathAlbumImage(string imagePath)
        {
            return Path.Combine(GetAlbumsDirectory(), imagePath);
        }

        /// <summary>
        /// Method to get a path to an album.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static async Task<string> GetAbsolutePathAlbumImageAsync(string imageRelativePath)
        {
            return Path.Combine(await GetAlbumsDirectoryAsync(), imageRelativePath);
        }

        /// <summary>
        /// Method to get a path to an album.
        /// </summary>
        /// <param name="albumId">The primary key of the album.</param>
        /// <returns>The path to the album directory.</returns>
        public static string GetRelativePathAlbumImage(int albumId, ImageType imgType, string fileExt)
        {
            // Format Album path by Primary Key
            int root = (int)Math.Floor((double)(albumId / 1024));
            string path = Path.Combine(root.ToString().PadLeft(4, '0'), albumId.ToString().PadLeft(4, '0'));

            if (imgType == ImageType.Picture || imgType == ImageType.Original)
            {
                return Path.Combine(path, "fanart" + fileExt);
            }

            if (imgType == ImageType.Thumbnail)
            {
                return Path.Combine(path, "cover" + fileExt);
            }

            return "";
        }

        /// <summary>
        /// Method to get a picture path.
        /// </summary>
        public static async Task<Image> CreateAlbumImage(string sourceFilename, ImageType imgType, int albumId)
        {
            string destFileName = await GetAbsolutePathAlbumImageAsync(albumId, imgType, Path.GetExtension(sourceFilename));
            File.Copy(sourceFilename, destFileName, true);

            if (imgType == ImageType.Picture)
            {
                return Image.FromFile(destFileName);
            }

            if (imgType == ImageType.Thumbnail)
            {
                Image img;
                using (Image src = Image.FromFile(destFileName))
                {
                    using (Image imgResized = src.ResizeRatio(SizeThumnail.Width))
                    {
                        img = imgResized.Crop(SizeThumnail.Width, SizeThumnail.Height);
                    }
                }
                img.Save(destFileName);

                return img;
            }

            return null;
        }

        #endregion
    }
}