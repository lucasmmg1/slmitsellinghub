using System.Collections;
using UnityEngine;

public abstract class QuestView : MonoBehaviour
{
    #region Variables

    #region Public Variables

    public QuestStatus Status { get; set; } = QuestStatus.NotStarted;

    #endregion

    #endregion

    #region Methods

    #region Public Methods

    public abstract IEnumerator Quest();

    #endregion

    #endregion
}