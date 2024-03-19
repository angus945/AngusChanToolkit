using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataDownloader
{
    void SyncDatas<T>(Action<T[]> onFinishedCallback);
}
