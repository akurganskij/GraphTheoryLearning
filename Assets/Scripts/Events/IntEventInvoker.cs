using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent<int>> unityEvents = 
        new Dictionary<EventName, UnityEvent<int>>();


    public void AddListener(EventName eventName, UnityAction<int> listener)
    {
        if(unityEvents.ContainsKey(eventName))
        {
            unityEvents[eventName].AddListener(listener);
        }
    }
}
