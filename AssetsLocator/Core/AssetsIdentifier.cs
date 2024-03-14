using System;
using System.IO;
using System.Collections.Generic;

namespace AngusChanToolkit.DataDriven
{
    [System.Serializable]
    public class AssetsIdentifier
    {
        public const string FILE_NAME = "_assetIdentifier";

        ILogger logger;
        IFileConverter converter;

        public string itemName;
        public string rootPath;
        public List<AssetsDirectory> directories; 

        public AssetsIdentifier(string rootPath, ILogger logger, IFileConverter converter)
        {
            this.rootPath = rootPath;
            this.logger = logger;
            this.converter = converter;

            directories = LoadFolders(rootPath, logger, converter);
        }
        List<AssetsDirectory> LoadFolders(string path, ILogger logger, IFileConverter converter)
        {
            string[] pathes = Directory.GetDirectories(path);

            List<AssetsDirectory> folders = new List<AssetsDirectory>();
            for (int i = 0; i < pathes.Length; i++)
            {
                folders.Add(new AssetsDirectory(pathes[i], logger, converter));
            }

            return  folders;
        }
        public bool TryGetDirectory(string name, out AssetsDirectory directory)
        {
            for (int i = 0; i < directories.Count; i++)
            {
                if(directories[i].name == name)
                {
                    directory = directories[i];
                    return true;
                }
            }

            directory = null;
            return false;
        }

        public AssetsDirectory CreateDirectory(string createDirectory)
        {
            Directory.CreateDirectory(createDirectory);

            AssetsDirectory newDirectory = new AssetsDirectory(createDirectory, logger, converter);
            directories.Add(newDirectory);

            return newDirectory;
        }

        public override string ToString()
        {
            return $"name: {itemName}, path: {rootPath}";
        }
    }
}
