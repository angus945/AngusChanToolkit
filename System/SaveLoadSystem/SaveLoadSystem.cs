using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveSlot : ISavableItem
{
    public bool enable;
    public string name;
    public List<string> keys;
}

[System.Serializable]
public class SaveLoadSystem
{
    bool initialized;

    IDataConverter converter;
    IDataReadWriter readWriter;

    int slotCount;
    SaveSlot[] slots;

    public SaveLoadSystem(int slotCount, IDataConverter converter, IDataReadWriter readWriter)
    {
        this.slotCount = slotCount;
        this.converter = converter;
        this.readWriter = readWriter;
    }

    void Initial()
    {
        if (initialized) return;

        if (readWriter.TryReadData(typeof(SaveSlot).Name, out string slotData))
        {
            Debug.Log(slotData);
            slots = converter.ConvertDatas<SaveSlot>(slotData);
        }
        else
        {
            slots = new SaveSlot[slotCount];
        }

        initialized = true;
    }

    public void SetSlotEnable(int index, bool enable)
    {
        Initial();

        if (index >= slotCount) return;

        if (enable)
        {
            slots[index].enable = true;
        }
        else
        {
            //TODO Delete slot datas
            slots[index].enable = false;
        }

    }

    void SaveGlobal()
    {

    }
    public void SaveSlot(int slot, ISavableItem item)
    {
        Initial();

        string key = $"slot-{slot}_{item.GetType().Name}";
        string data = converter.ConvertData(item);
        readWriter.WriteData(key, data);
    }
    public T LoadSlot<T>(int slot) where T : ISavableItem
    {
        Initial();

        string key = $"slot-{slot}_{typeof(T).Name}";
        string data = readWriter.ReadData(key);

        return converter.ConvertData<T>(data);
    }


}
