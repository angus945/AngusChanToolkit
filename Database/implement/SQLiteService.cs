using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SQLite4Unity3d;
using UnityEngine;

public class SQLiteService : ISQLService
{
    string databasePath;
    SQLiteConnection connection;


    void ISQLService.Initialize(string databasePath)
    {
        this.databasePath = databasePath;

        connection = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    void ISQLService.CreateTable<T>()
    {
        connection.CreateTable<T>();
    }
    void ISQLService.DropTable<T>()
    {
        connection.DropTable<T>();
    }
    void ISQLService.InsertAll<T>(T[] objs)
    {
        connection.InsertAll(objs);
    }

    IEnumerable<T> ISQLService.GetItems<T>()
    {
        return connection.Table<T>();
    }
    IEnumerable<T> ISQLService.GetItems<T>(Expression<Func<T, bool>> expression)
    {
        return connection.Table<T>().Where(expression);
    }
}
