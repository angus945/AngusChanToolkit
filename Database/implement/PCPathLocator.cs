using UnityEngine;

public class PCPathLocator : IPathLocator
{
    public string GetDatabasePath(string databaseName)
    {
        return $"{Application.streamingAssetsPath}/{databaseName}";
    }
}
