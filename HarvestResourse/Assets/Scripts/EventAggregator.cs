using System;
using UnityEngine;

public class EventAggregator : MonoBehaviour
{
    public static void Subscribe<T>(Action<object, T> @event)
    {
        EventInner<T>.Event += @event;
    }

    public static void Unsubscribe<T>(Action<object, T> @event)
    {
        EventInner<T>.Event -= @event;
    }

    public static void Post<T>(object sender, T eventHendler)
    {
        EventInner<T>.Post(sender, eventHendler);
    }


    private static class EventInner<T>
    {
        public static Action<object, T> Event;

        public static void Post(object sender, T eventHendler)
        {
            Event?.Invoke(sender, eventHendler);
        }
    }
}
