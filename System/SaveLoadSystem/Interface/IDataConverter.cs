using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataConverter
{
    string ConvertData(ISavableItem item);
    T ConvertData<T>(string data) where T : ISavableItem;
    
    string ConvertDatas(ISavableItem[] items);
    T[] ConvertDatas<T>(string data) where T : ISavableItem;



}
