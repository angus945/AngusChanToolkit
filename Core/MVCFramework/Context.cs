using System;
using System.Collections;
using System.Collections.Generic;

namespace AngusChanToolkit.MVC
{
    public class Context
    {
        Observer observer = new Observer();
        DataRequester requester = new DataRequester();

        // Observer
        public void AddListener<T>(EventHandler listener) where T : EventArgs
        {
            observer.AddListener<T>(listener);
        }
        public void RemoveListener<T>(EventHandler listener) where T : EventArgs
        {
            observer.RemoveListener<T>(listener);
        }
        public void TriggerEvent<T>(object sender, T args) where T : EventArgs
        {
            observer.TriggerEvent<T>(sender, args);
        }

        // DataRequester
        public void RegestDataProvider<T>(RequestProvideHandler provider) where T : EventArgs
        {
            requester.RegestDataProvider<T>(provider);
        }
        public void RemoveDataProvider<T>(RequestProvideHandler provider) where T : EventArgs
        {
            requester.RemoveDataProvider<T>(provider);
        }
        public void RegestData<T>(T data) where T : EventArgs
        {
            requester.RegestData<T>(data);
        }
        public void RemoveData<T>() where T : EventArgs
        {
            requester.RemoveData<T>();
        }

        public T RequestData<T>() where T : EventArgs
        {
            return requester.RequestData<T>();
        }
        public bool TryRequestData<T>(out T data) where T : EventArgs
        {
            return requester.TryRequestData<T>(out data);
        }
    }
}