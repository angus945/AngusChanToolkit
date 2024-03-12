using System;
using UnityEngine;

namespace AngusChanToolkit.DataDriven.Unity
{
    public class UnityFileConvert : IFileConverter
    {
        public T FromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);   
        }
        public T[] FromJsonArray<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
        public void FromJsonOverwrite(string json, object objectToOverride)
        {
            JsonUtility.FromJsonOverwrite(json, objectToOverride);
        }
        public T ReadCustom<T>(string name, byte[] rawData) where T : class
        {
            switch (typeof(T))
            {
                case Type texture when texture == typeof(Texture2D):

                    Texture2D image = new Texture2D(2, 2);
                    image.name = name;
                    image.LoadImage(rawData);
                    image.filterMode = FilterMode.Bilinear;
                    return image as T;

                default:
                    break;
            }

            return null;
        }

        public string ToJson(object obj, bool prettyPrint)
        {
            return JsonUtility.ToJson(obj, prettyPrint);
        }

        //public static string ToJson<T>(T[] array)
        //{
        //    Wrapper<T> wrapper = new Wrapper<T>();
        //    wrapper.Items = array;
        //    return JsonUtility.ToJson(wrapper);
        //}
        //public static string ToJson<T>(T[] array, bool prettyPrint)
        //{
        //    Wrapper<T> wrapper = new Wrapper<T>();
        //    wrapper.Items = array;
        //    return JsonUtility.ToJson(wrapper, prettyPrint);
        //}

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
