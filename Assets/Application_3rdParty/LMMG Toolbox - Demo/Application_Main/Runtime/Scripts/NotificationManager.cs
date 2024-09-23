using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Scripts/Hiscom Engine/Patterns/MMVCC/Managers/Notification Manager")]
public class NotificationManager : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    protected readonly Dictionary<GameObject, Dictionary<string, Action<GameObject, object>>> observers = new();

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void OnDisable()
    {
        observers.Clear();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Posts a notification to all observers.
    /// </summary>
    /// <param name="notificationSender"> The notification sender </param>
    /// <param name="notificationValue"> The notification to send </param>
    /// <param name="content"> The content to send </param>
    public void PostNotification(string notificationValue, GameObject notificationSender = null, object content = null)
    {
        foreach (var observer in observers.ToList())
        {
            if (observer.Key == null)
            {
                observers.Remove(observer.Key);
                return;
            }

            foreach (var notification in observer.Value)
            {
                if (notification.Key != notificationValue) continue;
                notification.Value?.Invoke(notificationSender, content);
            }
        }
    }

    /// <summary>
    /// Adds an observer to the list of observers.
    /// </summary>
    /// <param name="notificationValue"> The notification to send </param>
    /// <param name="notificationSender"> The sender of the notification </param>
    /// <param name="eventToExecute"> The event to execute when the notification is received </param>
    public void AddObserver(string notificationValue, GameObject notificationSender, Action<GameObject, object> eventToExecute)
    {
        if (observers.Keys.Contains(notificationSender))
        {
            if (observers[notificationSender].Keys.Contains(notificationValue))
            {
                Debug.LogWarning("Observer already exists for " + notificationValue);
                return;
            }

            observers[notificationSender].Add(notificationValue, eventToExecute);
            return;
        }

        observers.Add(notificationSender, new Dictionary<string, Action<GameObject, object>> {{notificationValue, eventToExecute}});
    }

    /// <summary>
    /// Removes a specific observer from the list of observers.
    /// </summary>
    /// <param name="notificationSender"> The sender of the notification </param>
    public void RemoveObservers(GameObject notificationSender)
    {
        if (!observers.Keys.Contains(notificationSender)) return;
        observers.Remove(notificationSender);
    }

    #endregion

    #endregion

    #region Singleton

    protected static readonly object Padlock = new();
    protected static NotificationManager instance;

    public static NotificationManager Instance
    {
        get
        {
            lock (Padlock)
            {
                if (instance != null) return instance;

                var go = new GameObject { name = "@NotificationManager" };
                instance = go.AddComponent<NotificationManager>();
                DontDestroyOnLoad(go);
                return instance;
            }
        }
    }

    #endregion
}