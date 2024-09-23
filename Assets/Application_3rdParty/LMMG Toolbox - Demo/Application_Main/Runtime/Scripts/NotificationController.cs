using UnityEngine;

[AddComponentMenu("Scripts/Hiscom Engine/Patterns/MMVCC/Controllers/Notification Controller")]
public class NotificationController : MonoBehaviour
{
    // Todo:
    // Colocar códigos de erro e warning.

    #region Methods

    #region Public Methods

    /// <summary>
    /// Posts a notification to all observers.
    /// </summary>
    /// <param name="notificationValue"> The notification to send </param>
    /// <param name="notificationSender"> The notification sender </param>
    /// <param name="content"> The content to send </param>
    public void PostNotification(string notificationValue, GameObject notificationSender, object content)
    {
        NotificationManager.Instance.PostNotification(notificationValue, notificationSender, content);
    }

    /// <summary>
    /// Posts a notification to all observers.
    /// </summary>
    /// <param name="notificationConnector"> The notification connector </param>
    public void PostNotification(NotificationConnector notificationConnector)
    {
        PostNotification(notificationConnector.Notification, notificationConnector.Sender,
            notificationConnector.Content);
    }

    #endregion

    #endregion
}