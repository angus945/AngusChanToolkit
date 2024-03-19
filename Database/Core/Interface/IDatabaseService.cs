using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDatabaseService
{
    void Initialize(string databasePath);
    void InsertAll<T>(T[] objs);
    void DropTable<T>();
    void CreateTable<T>();
}
