using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class Database<T> : IDatabaseReader<T> where T : new()
{
    IPathLocator pathLocator;
    ISQLService database;

    public Database(string databaseName, IPathLocator pathLocator, ISQLService database)
    {
        this.pathLocator = pathLocator;
        this.database = database;

        string dbPath = pathLocator.GetDatabasePath(databaseName);
        database.Initialize(dbPath);
    }
    public void RecreateDB()
    {
        database.DropTable<T>();
        database.CreateTable<T>();
    }
    public void InsertAll(T[] objs)
    {
        database.InsertAll(objs);
    }

    //Interface
    public T GetByIndex(int index)
    {
        return database.GetItems<T>().ElementAt(index);
    }
    public IEnumerable<T> GetItems(Expression<Func<T, bool>> expression)
    {
        return database.GetItems<T>(expression);
    }

    public IEnumerable<T> GetAll()
    {
        return database.GetItems<T>();
    }

}
