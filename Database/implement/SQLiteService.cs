using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

public class SQLiteService : IDatabaseService
{
    string databasePath;
    SQLiteConnection connection;


    void IDatabaseService.Initialize(string databasePath)
    {
        this.databasePath = databasePath;

        connection = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    void IDatabaseService.InsertAll<T>(T[] objs)
    {
        connection.InsertAll(objs);
    }
    void IDatabaseService.CreateTable<T>()
    {
        connection.CreateTable<T>();
    }
    void IDatabaseService.DropTable<T>()
    {
        connection.DropTable<T>();
    }

}
