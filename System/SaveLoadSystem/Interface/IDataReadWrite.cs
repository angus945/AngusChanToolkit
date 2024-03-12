using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataReadWriter 
{
    void WriteData(string key, string data);

    string ReadData(string key);
    bool TryReadData(string key, out string data);
}
