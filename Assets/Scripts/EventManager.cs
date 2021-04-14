using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// https://stackoverflow.com/questions/42034245/unity-eventmanager-with-delegate-instead-of-unityevent
// https://www.reddit.com/r/Unity2D/comments/78hamm/better_solution_for_event_manager/
// https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#
public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<object>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                } else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, Action<object>>();
        }
    }

    public static void StartListening<T>(string eventName, Action<T> listener) {
        Action<object> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            //Add more event to the existing one
            thisEvent += Convert(listener);
            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        } else {
            //Add event to the Dictionary for the first time
            thisEvent += Convert(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening<T>(string eventName, Action<T> listener) {
        if (eventManager == null) return;
        Action<object> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            //Remove event from the existing one
            thisEvent -= Convert(listener);
            //Update the Dictionary
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent<T>(string eventName, T param) {
        Action<object> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(param);
            // OR USE  instance.eventDictionary[eventName](object);
        }
    }

    public static Action<object> Convert<T>(Action<T> myActionT) {
        if (myActionT == null) return null;
        else return new Action<object>(o => myActionT((T)o));
    }

}
