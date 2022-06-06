using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class EventManager
    {
        private Dictionary<string, UnityEvent<object>> Listeners { get; set; } = new Dictionary<string, UnityEvent<object>>();

        public void Subscribe(string eventName, UnityAction<object> listerner)
        {
            if (!Listeners.ContainsKey(eventName)) Listeners.Add(eventName, new UnityEvent<object>());
            Listeners[eventName].AddListener(listerner);
        }

        public void Unsubscribe(string eventName, UnityAction<object> listerner)
        {
            if (!Listeners.ContainsKey(eventName)) return;
            Listeners[eventName].RemoveListener(listerner);
        }

        public void Clear()
        {
            Listeners.Clear();
        }

        public void Trigger(string eventName, object context)
        {
            if (!Listeners.ContainsKey(eventName))
            {
                Debug.LogError(string.Format("Can not trigger evene. {0} Event does not exist", eventName));
                return;
            }
            
            Listeners[eventName].Invoke(context);
        }
    }
}