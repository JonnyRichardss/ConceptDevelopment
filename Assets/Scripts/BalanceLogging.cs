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
