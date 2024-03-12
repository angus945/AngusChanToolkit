using System;
using System.IO;
using System.Linq;
using System.Xml;
//using UnityEngine;

namespace AngusChanToolkit.DataDriven
{
    [System.Serializable]
    public class AssetsPointer
    {
        public string name;
        public string format;
        public string path;

        ILogger logger;
        IFileConverter converter;

        internal AssetsPointer(string path, ILogger logger, IFileConverter converter)
        {
            this.path = path;
            this.logger = logger;
            this.converter = converter;

            string[] fileName = path.Split('\\').Last().Split('.');

            name = fileName[0];
            format = fileName[1];
        }

        public string ReadString()
        {
            return File.ReadAllText(path);
        }
        public T ReadCustomClass<T>() where T : class
        {
            byte[] data = File.ReadAllBytes(path);

            return converter.ReadCustom<T>(name, data);
        }
        public XmlDocument ReadXML()
        {
            byte[] data = File.ReadAllBytes(path);

            string xml = System.Text.Encoding.UTF8.GetString(data);
            XmlDocument docs = new XmlDocument();

            docs.LoadXml(xml);
            return docs;
        }
        public T ReadJsonObject<T>()
        {
            return converter.FromJson<T>(ReadString());
        }
        public T[] ReadJsonObjects<T>()
        {
            return converter.FromJsonArray<T>(ReadString());
        }

        internal void WriteString(string data)
        {
            File.WriteAllText(path, data);
        }
    }
}
