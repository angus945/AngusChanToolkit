using System;
using UnityEngine;

namespace AngusChanToolkit.Unity
{
    public class EventArgs_Global : EventArgs { }
    public class GlobalObserver : MonoBehaviour
    {
        static GlobalObserver instance;

        Observer globalObserver = new Observer();

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

            GameObject instanceObject = new GameObject("GamePlay Events");
            instance = instanceObject.AddComponent<GlobalObserver>();
        }
        public static void AddListener<T>(EventHandler listener) where T : EventArgs_Global
        {
            Initial();

            instance.globalObserver.AddListener<T>(listener);
        }
        public static void RemoveListener<T>(EventHandler listener) where T : EventArgs_Global
        {
            Initial();

            instance.globalObserver.RemoveListener<T>(listener);
        }
        public static void TriggerEvent<T>(object sender, T args) where T : EventArgs_Global
        {
            Initial();

            instance.globalObserver.TriggerEvent(sender, args);
        }
    }
}
