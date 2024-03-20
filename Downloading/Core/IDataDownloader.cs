using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITableDownloader<T> where T : class
{
    void DownloadTalbeDatas(Action<T[]> onFinishedCallback);
}
