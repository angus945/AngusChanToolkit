using UnityEngine;

namespace AngusChanToolkit.DataDriven.Unity
{
    public class UnityLogger : ILogger
    {
        public void Print(string message)
        {
            Debug.Log(message);
        }
        public void PrintError(string message)
        {
            Debug.LogError(message);
        }

        void ILogger.Print(object message)
        {
            Debug.Log(message);
        }
    }
}
