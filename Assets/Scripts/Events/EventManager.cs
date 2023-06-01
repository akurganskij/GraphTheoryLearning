using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static Dictionary<EventName, List<IntEventInvoker>> invokers =
        new Dictionary<EventName, List<IntEventInvoker>>();
    static Dictionary<EventName, List<UnityAction<int>>> listeners =
        new Dictionary<EventName, List<UnityAction<int>>>();

    public static void Initialize()
    {
        foreach(EventName eventName in Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(eventName))
            {
                invokers.Add(eventName, new List<IntEventInvoker>());
                listeners.Add(eventName, new List<UnityAction<int>>());
            }
            else
            {
                invokers[eventName].Clear();
                listeners[eventName].Clear();
            }
        }
    }

    public static void AddInvoker(EventName eventName, IntEventInvoker invoker)
    {
        foreach(UnityAction<int> listener in listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        invokers[eventName].Add(invoker);
    }

    public static void AddListener(EventName eventName, UnityAction<int> listener)
    {
        foreach (IntEventInvoker invoker in invokers[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        listeners[eventName].Add(listener);
    }

    public static void RemoveInvoker(EventName eventName, IntEventInvoker invoker)
    {
        invokers[eventName].Remove(invoker);
    }

}
