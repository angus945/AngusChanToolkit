using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database<T>
{
    IPathLocator pathLocator;
    IDatabaseService database;

    public Database(string databaseName, IPathLocator pathLocator, IDatabaseService database)
    {
        this.pathLocator = pathLocator;
        // this.syncer = syncer;
        this.database = database;

        string dbPath = pathLocator.GetDatabasePath(databaseName);
        database.Initialize(dbPath);
    }

    // public void ReDownloadDB()
    // {
    //     syncer.SyncDatas<T>((T[] datas) =>
    //     {
    //         Debug.Log(datas);
    //         Debug.Log(datas.Length);
    //         Debug.Log(datas[0].ToString());

    //         RecreateDB();
    //         InsertAll(datas);
    //     });
    // }
    public void RecreateDB()
    {
        database.DropTable<T>();
        database.CreateTable<T>();
    }
    public void InsertAll(T[] objs)
    {
        database.InsertAll(objs);
    }
}
