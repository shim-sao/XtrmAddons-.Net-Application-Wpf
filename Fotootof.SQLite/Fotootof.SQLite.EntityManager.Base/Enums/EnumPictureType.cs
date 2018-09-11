using Fotootof.SQLite.EntityManager.Enums;
using System.Xml.Serialization;

namespace Fotootof.SQLite.EntityManager.Enums
{
    /// <summary>
    /// Enum Fotootof SQLite Entity Manager Picture Type.
    /// </summary>
    public enum EnumPictureType
    {
        /// <summary>
        /// Picture Type Original. 
        /// </summary>
        [XmlEnum(Name = "Original")]
        Original = 0,

        /// <summary>
        /// Picture Type Thumbnail. 
        /// </summary>
        [XmlEnum(Name = "Thumbnail")]
        Thumbnail = 1,

        /// <summary>
        /// Picture Type Preview. 
        /// </summary>
        [XmlEnum(Name = "Preview")]
        Preview = 2,

        /// <summary>
        /// Picture Type Background. 
        /// </summary>
        [XmlEnum(Name = "Background")]
        Background = 3
    }
}


/// <summary>
/// Class Fotootof SQLite Entity Manager Picture Type Extension.
/// </summary>
public static class EnumPictureTypeExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string ToString(this EnumPictureType type)
    {
        switch(type)
        {
            case EnumPictureType.Original :
                return "Original";

            case EnumPictureType.Thumbnail :
                return "Thumbnail";

            case EnumPictureType.Preview :
                return "Preview";

            case EnumPictureType.Background:
                return "Background";
        }

        return null;
    }
}