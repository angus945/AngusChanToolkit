using System;
using System.Collections;
using System.Collections.Generic;

namespace AngusChanToolkit
{
    public class Observer
    {
        Dictionary<Type, EventHandler> gamePlayEvents = new Dictionary<Type, EventHandler>();

        public void AddListener<T>(EventHandler listener) where T : EventArgs
        {
            Type type = typeof(T);
            if (gamePlayEvents.ContainsKey(type))
            {
                gamePlayEvents[type] += listener;
            }
            else
            {
                gamePlayEvents.Add(type, listener);
            }
        }
        public void RemoveListener<T>(EventHandler listener) where T : EventArgs
        {
            Type type = typeof(T);
            if (gamePlayEvents.ContainsKey(type))
            {
                gamePlayEvents[type] -= listener;
            }
        }
        public void TriggerEvent<T>(object sender, T args) where T : EventArgs
        {
            Type type = typeof(T);

            if (gamePlayEvents.ContainsKey(type))
            {
                gamePlayEvents[type]?.Invoke(sender, args);
            }
        }
    }
}