using System.Xml.Serialization;

namespace Fotootof.Libraries.Enums
{
    /// <summary>
    /// XtrmAddons Net Picture Picture Type Enumerator.
    /// </summary>
    [System.Obsolete("Fotootof.SQLite.EntityManager.Enums.EnumPictureType")]
    public enum ImageType
    {
        /// <summary>
        /// Picture Type Picture. 
        /// </summary>
        [XmlEnum(Name = "Picture")]
        Picture = 1,

        /// <summary>
        /// Picture Type Thumbnail. 
        /// </summary>
        [XmlEnum(Name = "Thumbnail")]
        Thumbnail = 2,

        /// <summary>
        /// Picture Type Original. 
        /// </summary>
        [XmlEnum(Name = "Original")]
        Original = 3
    }
}
