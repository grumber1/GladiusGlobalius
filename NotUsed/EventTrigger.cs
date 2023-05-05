using UnityEngine;
using System;

public static class EventTrigger
{
    public static event EventHandler<bool> Event; 

    public static void StartEvent()
    {
        try
        {
            Debug.Log("works");
            Event?.Invoke(null, true);            
        }
        catch(Exception ex)
        {
            Debug.Log("works not");
            Debug.Log(ex.Data);
            Debug.Log(ex);
        }
    }
}