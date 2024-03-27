using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SQLite4Unity3d;
using UnityEngine;

public class SQLiteDatabase<T> : IDatabaseReader<T> where T : new()
{
    IPathLocator pathLocator;
    SQLiteConnection connection;

    public SQLiteDatabase(string databaseName, IPathLocator pathLocator)
    {
        this.pathLocator = pathLocator;

        string dbPath = pathLocator.GetDatabasePath(databaseName);
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }
    public void RecreateDB()
    {
        connection.DropTable<T>();
        connection.CreateTable<T>();
    }
    public void InsertAll(T[] objs)
    {
        connection.InsertAll(objs);
    }

    T IDatabaseReader<T>.Get(Expression<Func<T, bool>> predicate)
    {
        return connection.Get(predicate);
    }
}
