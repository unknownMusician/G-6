using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger 
{
    public static void LogW(Exception ex, string message)
    {
        string warning = "";

        warning += "Time: " + DateTime.Now + "\n";
        warning += message + " | " + ex.Message; 
        warning += "StackTrace: " + ex.StackTrace;

        Debug.LogWarning(warning);
    }
}
