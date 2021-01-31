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
    namespace Events
    {
        // If you want to trigger events or add/remove listeners, use/derive from the Event base class.
        public class EventManager : MonoBehaviour
        {
            private Dictionary<Type, Action<object>> eventListeners;
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
                if (eventListeners == null)
                {
                    eventListeners = new Dictionary<Type, Action<object>>();
                }
            }

            public static void AddListener(Type eventType, Action<object> listener)
            {
                Action<object> existingListener;
                instance.eventListeners.TryGetValue(eventType, out existingListener);
                existingListener += listener;
                instance.eventListeners[eventType] = existingListener;
            }

            public static void RemoveListener(Type eventType, Action<object> listener)
            {
                // If during tear down the eventManager instance has already been removed before all the listeners were removed, just return.
                if (eventManager == null)
                {
                    return;
                }

                Action<object> existingListener;
                instance.eventListeners.TryGetValue(eventType, out existingListener);
                existingListener -= listener;
                if (existingListener == null)
                {
                    instance.eventListeners.Remove(eventType);
                }
                else
                {
                    instance.eventListeners[eventType] = existingListener;
                }

            }

            public static void TriggerEvent(Type eventType, object payload)
            {
                Action<object> listener;
                if (instance.eventListeners.TryGetValue(eventType, out listener))
                {
                    listener.Invoke(payload);
                }
            }
        }

        // Interface for using the EventManager. Derive from the Event base class specifying a type for the generic event payload.
        public class EventBase<Payload>
        {
            // Map from listeners for derived events to their object payload listeners the event manager uses.
            Dictionary<Action<Payload>, Action<object>> listenerWrappers;

            public EventBase()
            {
                this.listenerWrappers = new Dictionary<Action<Payload>, Action<object>>();
            }

            public void AddListener(Action<Payload> listener)
            {
                if (this.listenerWrappers.ContainsKey(listener))
                {
                    return;
                }
                // This is the listener the event manager will use. It casts the object payload to the derived event's generic payload type and calls our listener.
                Action<object> newListenerWrapper = (object payload) => { listener((Payload)payload); };
                this.listenerWrappers[listener] = newListenerWrapper;
                EventManager.AddListener(this.GetType(), newListenerWrapper);
            }

            public void RemoveListener(Action<Payload> listener)
            {
                if (!this.listenerWrappers.ContainsKey(listener))
                {
                    return;
                }
                Action<object> listenerWrapper;
                this.listenerWrappers.TryGetValue(listener, out listenerWrapper);
                this.listenerWrappers.Remove(listener);
                EventManager.RemoveListener(this.GetType(), listenerWrapper);
            }

            public void Trigger(Payload payload)
            {
                EventManager.TriggerEvent(this.GetType(), payload);
            }
        }

        // Payload is the dragged inventory item.
        public class InventoryItemDragBegin : EventBase<GameObject> { }

        // Payload is the dragged inventory item.
        public class InventoryItemDragEnd : EventBase<GameObject> { }

    }

}
