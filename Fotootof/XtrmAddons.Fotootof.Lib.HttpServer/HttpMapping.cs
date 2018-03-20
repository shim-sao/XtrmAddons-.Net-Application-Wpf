using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XtrmAddons.Net.Xml;

namespace XtrmAddons.Fotootof.Lib.HttpServer
{
    public class HttpMapping
    {
        /// <summary>
        /// Variable path to file where stored configuration files.
        /// </summary>
        public static string MappingFile { get; set; }

        /// <summary>
        /// Variable dictionary of mapping.
        /// </summary>
        public static Dictionary<string, Type> Map = new Dictionary<string, Type>();

        /// <summary>
        /// Variable dictionary of settings.
        /// </summary>
        private static Dictionary<string, string> _settings = null;

        /// <summary>
        /// 
        /// </summary>
        public static void Load(string mappingFileName)
        {
            Create(mappingFileName);

            foreach (KeyValuePair<string, string> setting in _settings)
            {
                Map.Add(setting.Key, Type.GetType(setting.Key + "," + setting.Value));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Type Get(string key, string mappingFileName="")
        {
            if(Map == null)
            {
                Load(mappingFileName);
            }

            if(Map.ContainsKey(key))
            {
                return Map[key];
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, Type value, string mappingFileName = "")
        {
            if (Map == null)
            {
                Load(mappingFileName);
            }

            Map[key] = value;
        }

        /// <summary>
        /// Method to create a categories list.
        /// </summary>
        private static void Create(string mappingFileName, bool force = true)
        {
            MappingFile = mappingFileName;

            if (_settings == null)
            {
                _settings = new Dictionary<string, string>();
                ParseXmlFile();
            }
        }

        /// <summary>
        /// Method to save categories list.
        /// </summary>
        public static void Save(string mappingFileName = "")
        {
            // Create file in case of...
            Create(mappingFileName, false);

            // Build XML document.
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("configuration");
            xmlDoc.AppendChild(rootNode);

            foreach (var setting in _settings)
            {
                XmlNode categoryNode = PropertyToXmlNode(xmlDoc, setting.Key, setting.Value);
                rootNode.AppendChild(categoryNode);
            }
            
            using (FileStream SourceStream = XMLManager.Open(MappingFile))
            {
                SourceStream.SetLength(0);
                SourceStream.Flush();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Save(SourceStream);
            }
        }

        /// <summary>
        /// Method to get property to XML node.
        /// </summary>
        /// <param name="xmlDoc">The XmlDocument to create property node.</param>
        private static XmlNode PropertyToXmlNode(XmlDocument xmlDoc, string key, string value)
        {
            XmlNode node = xmlDoc.CreateElement("property");

            XmlAttribute attribute = xmlDoc.CreateAttribute("key");
            attribute.Value = key;
            node.Attributes.Append(attribute);

            attribute = xmlDoc.CreateAttribute("value");
            attribute.Value = value;
            node.Attributes.Append(attribute);

            return node;
        }

        /// <summary>
        /// Method to add new category.
        /// </summary>
        private static void ParseXmlFile()
        {
            XmlNodeList nodes = XMLManager.LoadToDocList(MappingFile);
            
            try
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlAttributeCollection att = nodes[i].Attributes;
                    _settings.Add(
                        att.GetNamedItem("key").InnerText,
                        att.GetNamedItem("value").InnerText
                    );
                }
            }
            catch {}
        }
    }
}
