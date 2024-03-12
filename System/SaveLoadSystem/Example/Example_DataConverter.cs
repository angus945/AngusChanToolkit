using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_DataConverter : IDataConverter
{
    [System.Serializable]
    class Wrapper<T>
    {
        public T[] Items;
    }

    string IDataConverter.ConvertData(ISavableItem item)
    {
        return JsonUtility.ToJson(item, true);
    }
    T IDataConverter.ConvertData<T>(string data)
    {
        return JsonUtility.FromJson<T>(data);
    }

    string IDataConverter.ConvertDatas(ISavableItem[] items)
    {
        Wrapper<ISavableItem> wrapper = new Wrapper<ISavableItem>();
        wrapper.Items = items;
        return JsonUtility.ToJson(wrapper);
    }

    T[] IDataConverter.ConvertDatas<T>(string data)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(data);
        return wrapper.Items;
    }
}
