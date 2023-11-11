using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SSActionEventType : int { Started, Competeted }
public interface ISSActionCallback
{
    /// <summary>
    /// when an action has been excuted, call the callback function
    /// </summary>
    /// <param name="source">action has been excuted</param>
    /// <param name="events"></param>
    /// <param name="intParam"></param>
    /// <param name="strParam"></param>
    /// <param name="objectParam"></param>
    public void SSActionEvent(SSAction source,
                              SSActionEventType events = SSActionEventType.Competeted,
                              int intParam = 0,
                              string strParam = null,
                              GameObject objectParam = null);
    
}
