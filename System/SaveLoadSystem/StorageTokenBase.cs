using System;
using UnityEngine;

namespace DataStorage
{
    public abstract class StorageTokenBase : ScriptableObject
    {
        public static void ClearAllStorgeData()
        {
            PlayerPrefs.DeleteAll();
        }

        public abstract string storgeKey { get; }
        protected abstract void OnClear();

#if UNITY_EDITOR
        [Header("Debug Opion")]
        [SerializeField] bool printOnStoring;
#endif

        public void ApplyToStorge()
        {
            string jsonData = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(storgeKey, jsonData);

#if UNITY_EDITOR
            if(printOnStoring)
            {
                Debug.Log(JsonUtility.ToJson(this, true));
            }
#endif
        }
        public void LoadData()
        {
            if (PlayerPrefs.HasKey(storgeKey))
            {
                string jsonData = PlayerPrefs.GetString(storgeKey);
                JsonUtility.FromJsonOverwrite(jsonData, this);
            }
            else
            {
                string jsonData = JsonUtility.ToJson(this);
                PlayerPrefs.SetString(storgeKey, jsonData);
            }
        }
        public void DelectData()
        {
            PlayerPrefs.DeleteKey(storgeKey);

            OnClear();
        }
    }
}
