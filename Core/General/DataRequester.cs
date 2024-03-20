using System;
using System.Collections.Generic;

//public interface IRequestData { }
public delegate EventArgs RequestProvideHandler();
public class DataRequester
{
    Dictionary<Type, EventArgs> dataTable = new Dictionary<Type, EventArgs>();
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

    public void RegestData<T>(T data) where T : EventArgs
    {
        Type type = typeof(T);

        if (dataTable.ContainsKey(type))
        {
            dataTable[type] = data;
        }
        else
        {
            dataTable.Add(type, data);
        }
    }
    public void RemoveData<T>() where T : EventArgs
    {
        Type type = typeof(T);
        if (dataTable.ContainsKey(type))
        {
            dataTable.Remove(type);
        }
    }

    public T RequestData<T>() where T : EventArgs
    {
        Type type = typeof(T);

        if (dataTable.ContainsKey(type))
        {
            return dataTable[type] as T;
        }
        else if (providerTable.ContainsKey(type))
        {
            return providerTable[type].Invoke() as T;
        }
        else return null;
    }
    public bool TryRequestData<T>(out T data) where T : EventArgs
    {
        Type type = typeof(T);

        if (dataTable.ContainsKey(type))
        {
            data = dataTable[type] as T;
            return true;
        }
        else if (providerTable.ContainsKey(type))
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
