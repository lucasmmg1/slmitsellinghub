using UnityEngine;

public abstract class NotificationContentConnector : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    protected Object content;

    #endregion

    #endregion

    #region Methods

    #region Public Methods

    public Object GetContent() => content;

    #endregion

    #endregion
}