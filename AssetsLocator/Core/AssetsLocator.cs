using System;
using System.Collections.Generic;
using System.IO;

namespace AngusChanToolkit.DataDriven
{
    public interface ILogger
    {
        void Print(string message);
        void Print(object message);
        void PrintError(string message);
    }
    public interface IFileConverter
    {
        void FromJsonOverwrite(string json, object objectToOverride);
        T FromJson<T>(string json);
        T[] FromJsonArray<T>(string json);
        T ReadCustom<T>(string name, byte[] rawData) where T : class;

        public string ToJson(object obj, bool prettyPrint);
    }

    [System.Serializable]
    public class AssetsLocator
    {
        List<AssetsIdentifier> assetsIdentifiers = new List<AssetsIdentifier>();
        List<string> registPath = new List<string>();

        ILogger logger;
        IFileConverter converter;

        public AssetsLocator(ILogger logger, IFileConverter converter)
        {
            this.logger = logger;
            this.converter = converter;
        }

        public void RegistDirectory(string path)
        {
            if (registPath.Contains(path)) return;

            registPath.Add(path);
        }
        public void RebuildDirectory()
        {
            assetsIdentifiers.Clear();

            for (int rootIndex = 0; rootIndex < registPath.Count; rootIndex++)
            {
                string loadRoot = registPath[rootIndex];
                string[] loadFolder = Directory.GetDirectories(loadRoot);

                for (int folderIndex = 0; folderIndex < loadFolder.Length; folderIndex++)
                {
                    string folderPath = loadFolder[folderIndex];

                    CreateIdentfier(folderPath);
                }
            }
        }
        AssetsIdentifier CreateIdentfier(string path)
        {
            string itemPath = ($"{path}/{AssetsIdentifier.FILE_NAME}.json");

            if (!File.Exists(itemPath))
            {
                logger.PrintError($"assetsIdentifier lost, path: {itemPath}");
                return null;
            }

            string json = File.ReadAllText(itemPath);

            AssetsIdentifier item = new AssetsIdentifier(path, logger, converter);
            converter.FromJsonOverwrite(json, item);

            assetsIdentifiers.Add(item);


            return item;
        }

        bool TryGetItem(string name, out AssetsIdentifier identifier)
        {
            identifier = assetsIdentifiers.Find(n => n.itemName == name);

            return identifier != null;
        }
        AssetsIdentifier[] GetItems()
        {
            return assetsIdentifiers.ToArray();
        }
        AssetsDirectory[] GetDirectorys(string directoryName)
        {
            List<AssetsDirectory> directories = new List<AssetsDirectory>();

            for (int i = 0; i < assetsIdentifiers.Count; i++)
            {
                AssetsIdentifier content = assetsIdentifiers[i];

                if (content.TryGetDirectory(directoryName, out AssetsDirectory directory))
                {
                    directories.Add(directory);
                }
            }

            return directories.ToArray();
        }
        AssetsPointer[] GetPointers(string directoryName, string fileName)
        {
            AssetsDirectory[] directories = GetDirectorys(directoryName);
            List<AssetsPointer> pointers = new List<AssetsPointer>();

            for (int i = 0; i < directories.Length; i++)
            {
                AssetsDirectory directory = directories[i];

                if (directory.TryGetFileWithName(fileName, out AssetsPointer pointer))
                {
                    pointers.Add(pointer);
                }
            }

            return pointers.ToArray();
        }
        AssetsPointer[] GetPointers(string directoryName)
        {
            AssetsDirectory[] directories = GetDirectorys(directoryName);
            List<AssetsPointer> pointers = new List<AssetsPointer>();

            for (int dirIndex = 0; dirIndex < directories.Length; dirIndex++)
            {
                AssetsDirectory directory = directories[dirIndex];

                pointers.AddRange(directory.pointers);
            }

            return pointers.ToArray();
        }

        public TOutput[] LoadObjects<TOutput>(string directoryName, string fileName, Func<AssetsPointer, TOutput[]> converter)
        {
            AssetsPointer[] pointers = GetPointers(directoryName, fileName);
            List<TOutput> outputs = new List<TOutput>();

            for (int i = 0; i < pointers.Length; i++)
            {
                outputs.AddRange(converter.Invoke(pointers[i]));
            }

            return outputs.ToArray();
        }
        public TOutput[] LoadObjects<TOutput>(string directoryName, string fileName, Func<AssetsPointer, TOutput> converter)
        {
            AssetsPointer[] pointers = GetPointers(directoryName, fileName);
            List<TOutput> outputs = new List<TOutput>();

            for (int i = 0; i < pointers.Length; i++)
            {
                outputs.Add(converter.Invoke(pointers[i]));
            }

            return outputs.ToArray();
        }
        public TOutput[] LoadObjects<TOutput>(string directoryName, Func<AssetsPointer, TOutput> converter)
        {
            AssetsPointer[] pointers = GetPointers(directoryName);
            List<TOutput> outputs = new List<TOutput>();

            for (int i = 0; i < pointers.Length; i++)
            {
                outputs.Add(converter.Invoke(pointers[i]));
            }

            return outputs.ToArray();
        }

        public void StoreObject(string root, string directoryName, string fileName, object storeObj)
        {
            if (!TryGetItem(root, out AssetsIdentifier identifier))
            {
                string content = "{ \"itemName\": \"" + root + "\" }";
                string folderPath = $"{registPath[0]}/{root}";
                string identifierPath = $"{registPath[0]}/{root}/{AssetsIdentifier.FILE_NAME}.json";

                Directory.CreateDirectory(folderPath);
                File.WriteAllText(identifierPath, content);

                identifier = CreateIdentfier(folderPath);
            }

            if (!identifier.TryGetDirectory(directoryName, out AssetsDirectory directory))
            {
                string createDirectory = $"{registPath[0]}/{root}\\{directoryName}";
                directory = identifier.CreateDirectory(createDirectory);
            }

            if (!directory.TryGetFileWithName(fileName, out AssetsPointer pointer))
            {
                string saveFile = $"{registPath[0]}/{root}/{directoryName}\\{fileName}.json";

                pointer = directory.CreatePointer(saveFile);
            }

            string json = converter.ToJson(storeObj, false);
            pointer.WriteString(json);
            //File.WriteAllText(saveFile, json, System.Text.Encoding.UTF8);

        }
    }
}
