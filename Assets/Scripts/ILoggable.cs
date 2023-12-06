using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoggable
{
    public string LogDebugMessage();
    public string ToLogString();
}
