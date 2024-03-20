using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GASGoogleSheetDownloader<T> : ITableDownloader<T> where T : class
{
    // // https://script.google.com/home/projects/1YkrYjL74-PsUwZrQqeC2MIW3s0WT3NiEgh1fNJqg2DRVpVN2q5b3iuer/edit?pli=1
    // https://script.google.com/u/0/home/projects/1Wfd2d_Z96N9niGa01_yEq5vth3kK1BXo46AhjCPzaA0bYH1W379cHjvA/edit

    string GASPostURL;
    string excelID;
    string sheetName;

    public GASGoogleSheetDownloader(string GASPostURL, string excelID, string sheetName)
    {
        this.GASPostURL = GASPostURL;
        this.excelID = excelID;
        this.sheetName = sheetName;
    }

    async void ITableDownloader<T>.DownloadTalbeDatas(Action<T[]> onFinishedCallback)
    {
        await AsyncGetRequest((bool success, string json) =>
        {
            if (!success)
            {
                Debug.LogError("SyncData failed: " + json);
                return;
            }
            JsonWrapper<T> wrapper = JsonUtility.FromJson<JsonWrapper<T>>("{\"items\":" + json + "}");
            onFinishedCallback.Invoke(wrapper.items);
        });
    }
    async Task AsyncGetRequest(Action<bool, string> onFinishedCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("sheetID", excelID);
        form.AddField("sheetName", sheetName);

        using (UnityWebRequest request = UnityWebRequest.Post(GASPostURL, form))
        {
            request.SendWebRequest();

            while (!request.isDone)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                onFinishedCallback.Invoke(false, request.error);
            }
            else
            {
                onFinishedCallback.Invoke(true, request.downloadHandler.text);
            }
        }
    }

    [System.Serializable]
    class JsonWrapper<T>
    {
        public T[] items;
    }
}