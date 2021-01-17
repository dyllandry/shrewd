/**
 * This is a singleton for a decoupled message broker system.
 * 
 * Tutorial: https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa
 * Related question that turned this from using UnityEvents to Actions: https://stackoverflow.com/questions/42034245/unity-eventmanager-with-delegate-instead-of-unityevent
 */

using UnityEngine;
using System.Collections.Generic;
using System;

namespace Shrewd
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<EventName, Action<GameObject>> eventDictionary;
        private static EventManager eventManager;
        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType<EventManager>();
                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<EventName, Action<GameObject>>();
            }
        }

        public static void StartListener(EventName eventName, Action<GameObject> listener)
        {
            Action<GameObject> eventToListenTo;
            if (instance.eventDictionary.TryGetValue(eventName, out eventToListenTo))
            {
                eventToListenTo += listener;
                // Update the dictionary (eventToListenTo is a value type).
                instance.eventDictionary[eventName] = eventToListenTo;
            }
            else
            {
                eventToListenTo += listener;
                // Add event to the dictionary for the first time.
                instance.eventDictionary.Add(eventName, eventToListenTo);
            }
            
        }

        public static void StopListener(EventName eventName, Action<GameObject> listener)
        {
            // In case during cleanup we've already removed the event manager, prevent null reference exceptions.
            if (eventManager == null) return;
            Action<GameObject> eventToStopListeningTo;
            if (instance.eventDictionary.TryGetValue(eventName, out eventToStopListeningTo))
            {
                eventToStopListeningTo -= listener;
                // Update the dictionary.
                instance.eventDictionary[eventName] = eventToStopListeningTo;
            }
        }

        public static void TriggerEvent(EventName eventName, GameObject gameObject)
        {
            Action<GameObject> eventToTrigger;
            if (instance.eventDictionary.TryGetValue(eventName, out eventToTrigger))
            {
                eventToTrigger.Invoke(gameObject);
            }
        }
    }

    public enum EventName
    {
        INVENTORY_ITEM_DRAG_BEGIN,
        INVENTORY_ITEM_DRAG_END
    }
}
