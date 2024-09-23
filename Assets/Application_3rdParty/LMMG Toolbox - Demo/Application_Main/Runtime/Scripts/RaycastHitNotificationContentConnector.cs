using UnityEngine;

[AddComponentMenu("Scripts/Hiscom Engine/Patterns/MMVCC/Connectors/Raycast Hit Notification Content Connector")]
public class RaycastHitNotificationContentConnector : NotificationContentConnector
{
    #region Methods

    #region Public Methods

    public void SetContent(RaycastHit hit)
    {
        content = hit.transform.gameObject;
    }

    #endregion

    #endregion
}