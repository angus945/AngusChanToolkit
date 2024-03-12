using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AngusChanToolkit.DataDriven
{
    [System.Serializable]
    public class AssetsDirectory
    {
        public string name;
        public string path;

        internal List<AssetsPointer> pointers;

        ILogger logger;
        IFileConverter converter;

        internal AssetsDirectory(string path, ILogger logger, IFileConverter converter)
        {
            this.path = path;
            this.name = path.Split('\\').Last();

            this.logger = logger;
            this.converter = converter;

            pointers = LoadFiles(path, logger, converter);
        }

        List<AssetsPointer> LoadFiles(string path, ILogger logger, IFileConverter converter)
        {
            string[] pathes = Directory.GetFiles(path).Where(file => !file.EndsWith(".meta")).ToArray();

            List<AssetsPointer> files = new List<AssetsPointer>();
            for (int i = 0; i < pathes.Length; i++)
            {
                files.Add(new AssetsPointer(pathes[i], logger, converter));
            }

            return files;
        }

        internal bool TryGetFileWithName(string name, out AssetsPointer pointer)
        {
            if (pointers.Count == 0)
            {
                pointer = null;
                return false;
            }
            else
            {
                for (int i = 0; i < pointers.Count; i++)
                {
                    if (pointers[i].name == name)
                    {
                        pointer = pointers[i];
                        return true;
                    }
                }

            }

            pointer = null;
            return false;
        }
        internal AssetsPointer CreatePointer(string path)
        {
            AssetsPointer pointer = new AssetsPointer(path, logger, converter);

            pointers.Add(pointer);

            return pointer;
        }

        public override string ToString()
        {
            return $"folder: {name}";
        }
    }
}
