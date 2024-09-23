using UnityEngine;

public class NotificationConnector : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected string notification;
    [SerializeField] protected GameObject sender;
    [SerializeField] protected Object content;

    #endregion

    #region Public Variables

    public string Notification => notification;
    public GameObject Sender => sender;
    public Object Content => content;

    #endregion

    #endregion

    #region Methods

    #region Public Methods

    public void SetContent(NotificationContentConnector connector)
    {
        content = connector.GetContent();
    }

    #endregion

    #endregion
}