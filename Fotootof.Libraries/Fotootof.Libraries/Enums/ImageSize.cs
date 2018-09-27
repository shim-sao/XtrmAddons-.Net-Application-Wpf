using System.Xml.Serialization;

namespace Fotootof.Libraries.Enums
{
    /// <summary>
    /// Enumerator Fotootof Libraries Image Size.
    /// </summary>
    public enum ImageSize
    {
        /// <summary>
        /// Icon. 
        /// </summary>
        [XmlEnum(Name = "Icon")]
        Icon = 32,

        /// <summary>
        /// Thumbnail. 
        /// </summary>
        [XmlEnum(Name = "Thumbnail")]
        Thumbnail = 64,

        /// <summary>
        /// Vignette. 
        /// </summary>
        [XmlEnum(Name = "Vignette")]
        Vignette = 96,

        /// <summary>
        /// Large. 
        /// </summary>
        [XmlEnum(Name = "Large")]
        Large = 128,

        /// <summary>
        /// X-Large. 
        /// </summary>
        [XmlEnum(Name = "XLarge")]
        XLarge = 256
    }
}
