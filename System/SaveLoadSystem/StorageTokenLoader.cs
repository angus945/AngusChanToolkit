using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStorage
{
    public class StorageTokenLoader
    {
        static Dictionary<Type, StorageTokenBase> StorgeDataTable = new Dictionary<Type, StorageTokenBase>();

        public static T LoadStorageToken<T>() where T : StorageTokenBase
        {
            if(StorgeDataTable.TryGetValue(typeof(T), out StorageTokenBase dataStorage))
            {
                return dataStorage as T;
            }
            else
            {
                T data = ScriptableObject.CreateInstance<T>();
                StorgeDataTable.Add(typeof(T), data);
                data.LoadData();

                return data;
            }
        }
    }
}
