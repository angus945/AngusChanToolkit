using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public interface ISQLService
{
    void Initialize(string databasePath);

    void DropTable<T>();
    void CreateTable<T>();

    void InsertAll<T>(T[] objs);

    IEnumerable<T> GetItems<T>() where T : new();
    IEnumerable<T> GetItems<T>(Expression<Func<T, bool>> expression) where T : new();
}
