using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Example_SavableItem : ISavableItem
{
    // string ISavableItem.itemKey { get => "example save item"; }

    public int integerData;
    public float floatData;
    public string stringData;

}
