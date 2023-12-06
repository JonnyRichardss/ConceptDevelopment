using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
static public class BalanceLogging
{
    static private string currentPath = "";
    static public void CreateNewLog()
    {
        //make new file with current time
        currentPath = String.Format("{0}-log.txt",DateTime.Now.ToString("yyyy-mm-dd_hh.mm.ss"));
        FileStream fs = File.Open(Application.persistentDataPath + "/" + currentPath, FileMode.CreateNew);
        fs.Close();
    }
    /*
    static public void Log(EventScriptable e, ChoiceScriptable c)
    {
        Debug.Log("Logged new choice");
        string stringToLog = String.Format("{0} chosen {1}", e.name, c.name);
       // EnterLog(new );
    }
    static public void Log(ResourceHolder r)
    {
        Debug.Log("Logged resources after new effect");
       WriteLog(r.ToLogString());
    }
    static public void Log(BuildingScriptable b)
    {
        Debug.Log("Logged new building");
       // EnterLog();
    }
    */
    static public void Log(ILoggable obj)
    {
        Debug.Log(obj.LogDebugMessage());
        WriteLog(obj.ToLogString());
    }
    static public void WriteLog(string data)
    {
        data = "{\n" + DateTime.Now + "\n" + data + "\n}";
        if (currentPath == "") CreateNewLog();

        StreamWriter sw = File.AppendText(Application.persistentDataPath + "/"+ currentPath);
        sw.WriteLine(data);
        sw.Close();
    }
}
