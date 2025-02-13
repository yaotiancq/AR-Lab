using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DebugDisplay : MonoBehaviour
{
    public TMP_Text debugText; 
    public int maxLogs = 10;  

    private Queue<string> logQueue = new Queue<string>(); 

    void Start()
    {
        if (debugText != null)
        {
            debugText.text = "Started!\n";
        }

        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log)
        {
            logQueue.Enqueue($"{type}: {logString}");

            if (logQueue.Count > maxLogs)
            {
                logQueue.Dequeue();
            }

            UpdateDebugText();
        }


    }


    void UpdateDebugText()
    {
        if (debugText != null)
        {
            debugText.text = string.Join("\n", logQueue.ToArray());
        }
    }

    public void Log(string message)
    {
        logQueue.Enqueue($"Custom: {message}");
        if (logQueue.Count > maxLogs)
        {
            logQueue.Dequeue();
        }
        UpdateDebugText();
    }
}
