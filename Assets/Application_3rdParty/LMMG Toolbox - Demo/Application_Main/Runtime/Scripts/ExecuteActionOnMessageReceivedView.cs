using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Scripts/Engine/Core/MMVCC/Views/Execute Action On Message Received View")]
public class ExecuteActionOnMessageReceivedView : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected string notificationToReceive;
    [SerializeField] protected UnityEvent<GameObject, object> eventToExecuteWhenReceiveNotification;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void Awake()
    {
        AddObservers();
    }

    protected void AddObservers()
    {
        NotificationManager.Instance.AddObserver(notificationToReceive, gameObject, eventToExecuteWhenReceiveNotification.Invoke);
    }

    #endregion

    #endregion
}