using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GASGoogleSheetDownloader : MonoBehaviour, IDataDownloader
{
    // // https://script.google.com/home/projects/1YkrYjL74-PsUwZrQqeC2MIW3s0WT3NiEgh1fNJqg2DRVpVN2q5b3iuer/edit?pli=1
    // https://script.google.com/u/0/home/projects/1Wfd2d_Z96N9niGa01_yEq5vth3kK1BXo46AhjCPzaA0bYH1W379cHjvA/edit

    string GASPostURL;
    string excelID;
    string sheetName;

    Coroutine syncRoutine;

    public void SetSyncer(string GASPostURL, string excelID, string sheetName)
    {
        this.GASPostURL = GASPostURL;
        this.excelID = excelID;
        this.sheetName = sheetName;
    }
    void IDataDownloader.SyncDatas<T>(Action<T[]> onFinishedCallback)
    {
        if (syncRoutine != null) StopCoroutine(syncRoutine);

        syncRoutine = StartCoroutine(SyncData((bool success, string json) =>
        {
            if (!success)
            {
                Debug.LogError("SyncData failed: " + json);
                return;
            }

            JsonWrapper<T> wrapper = JsonUtility.FromJson<JsonWrapper<T>>("{\"items\":" + json + "}");
            onFinishedCallback?.Invoke(wrapper.items);
        }));
    }
    IEnumerator SyncData(Action<bool, string> onFinishCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("sheetID", excelID);
        form.AddField("sheetName", sheetName);

        using (UnityWebRequest www = UnityWebRequest.Post(GASPostURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                onFinishCallback.Invoke(false, www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                onFinishCallback.Invoke(true, json);
            }
        }
    }

    [System.Serializable]
    class JsonWrapper<T>
    {
        public T[] items;
    }
}