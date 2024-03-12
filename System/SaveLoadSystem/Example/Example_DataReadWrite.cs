using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_DataReadWrite : IDataReadWriter
{
    void IDataReadWriter.WriteData(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
    }

    string IDataReadWriter.ReadData(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    bool IDataReadWriter.TryReadData(string key, out string data)
    {
        if (PlayerPrefs.HasKey(key))
        {
            data = PlayerPrefs.GetString(key);
            return true;
        }
        else
        {
            data = "";
            return false;
        }
    }

}
