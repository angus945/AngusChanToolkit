using System;
using System.Collections.Generic;

//public interface IRequestData { }
public delegate EventArgs RequestProvideHandler();
public class DataRequester
{
    Dictionary<Type, RequestProvideHandler> providerTable = new Dictionary<Type, RequestProvideHandler>();

    public void RegestDataProvider<T>(RequestProvideHandler provider) where T : EventArgs
    {
        Type type = typeof(T);

        if (providerTable.ContainsKey(type))
        {
            providerTable[type] = provider;
        }
        else
        {
            providerTable.Add(type, provider);
        }
    }
    public void RemoveDataProvider<T>(RequestProvideHandler provider) where T : EventArgs
    {
        Type type = typeof(T);
        if (providerTable.ContainsKey(type))
        {
            providerTable[type] -= provider;
        }
    }
    public T RequestData<T>() where T : EventArgs
    {
        Type type = typeof(T);

        if (providerTable.ContainsKey(type))
        {
            return providerTable[type].Invoke() as T;
        }
        else return null;
    }
    public bool TryRequestData<T>(out T data) where T : EventArgs
    {
        Type type = typeof(T);

        if (providerTable.ContainsKey(type))
        {
            data = providerTable[type].Invoke() as T;
            return true;
        }
        else
        {
            data = null;
            return false;
        }
    }
}
