using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
[Serializable]
struct LogEntry
{
    public DateTime currentTime;
    public object logObject;
    public LogEntry(object o)
    {
        logObject = o;
        currentTime = DateTime.Now;
    }
}

static public class BalanceLogging
{
    static private string currentPath = "";
    static public void CreateNewLog()
    {
        //make new file with current time
        currentPath = String.Format("{0}-log.json",DateTime.Now);
    }
    static public void Log(EventScriptable e, ChoiceScriptable c)
    {
        Debug.Log("Logged new choice");
        string stringToLog = String.Format("{0} chosen {1}", e.name, c.name);
        EnterLog(new LogEntry(stringToLog));
    }
    static public void Log(ResourceHolder r)
    {
        Debug.Log("Logged resources after new effect");
        EnterLog(new LogEntry(r));
    }
    static public void Log(BuildingScriptable b)
    {
        Debug.Log("Logged new building");
        EnterLog(new LogEntry(b));
    }
    static private void EnterLog(LogEntry log)
    {
        Write(JsonUtility.ToJson(log));
    }
    static private void Write(string data)
    {
        if (currentPath == "") CreateNewLog();
        StreamWriter sw = File.AppendText(Application.persistentDataPath + currentPath);
        sw.WriteLine(data);
    }
}
