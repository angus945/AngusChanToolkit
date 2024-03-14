using System;
using UnityEngine;

namespace AngusChanToolkit.Unity
{
    public class EventArgs_DataRequest : EventArgs { }
    public class GlobalDataRequester : MonoBehaviour
    {
        static GlobalDataRequester instance;

        DataRequester requester = new DataRequester();

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
        }
        void OnDestroy()
        {
            instance = null;
        }

        static void Initial()
        {
            if (instance != null) return;

            GameObject instanceObject = new GameObject("DataRequest Events");
            instance = instanceObject.AddComponent<GlobalDataRequester>();
        }
        public static void RegestProvider<T>(RequestProvideHandler provideHandler) where T : EventArgs_DataRequest
        {
            Initial();

            instance.requester.RegestDataProvider<T>(provideHandler);
        }
        public static void RemoveDataProvider<T>(RequestProvideHandler provideHandler) where T : EventArgs_DataRequest
        {
            Initial();

            instance.requester.RemoveDataProvider<T>(provideHandler);
        }
        public static T RequestData<T>() where T : EventArgs_DataRequest
        {
            Initial();

            return instance.requester.RequestData<T>();
        }
        public static bool TryRequestData<T>(out T data) where T : EventArgs_DataRequest
        {
            Initial();

            return instance.requester.TryRequestData<T>(out data);
        }
    }
}