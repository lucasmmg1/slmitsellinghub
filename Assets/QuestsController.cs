using UnityEngine;

public class QuestsController : MonoBehaviour
{
    #region Methods

    #region Public Methods

    public void StartQuest(QuestView quest)
    {
        if (quest.Status is not QuestStatus.NotStarted) return;
        quest.Status = QuestStatus.InProgress;
        StartCoroutine(quest.Quest());
    }
    public void PauseQuest(QuestView quest)
    {
        if (quest.Status is not QuestStatus.InProgress) return;
        quest.Status = QuestStatus.Paused;
        StopCoroutine(quest.Quest());
    }
    public void UnpauseQuest(QuestView quest)
    {
        if (quest.Status is not QuestStatus.Paused) return;
        quest.Status = QuestStatus.Paused;
        StartCoroutine(quest.Quest());
    }
    
    public void CompleteQuest(QuestView quest)
    {
        if (quest.Status is not (QuestStatus.Paused or QuestStatus.InProgress)) return;
        quest.Status = QuestStatus.Completed;
        StopCoroutine(quest.Quest());
    }
    public void FailQuest(QuestView quest)
    {
        if (quest.Status is not (QuestStatus.Paused or QuestStatus.InProgress)) return;
        quest.Status = QuestStatus.Failed;
        StopCoroutine(quest.Quest());
    }
    public void CancelQuest(QuestView quest)
    {
        if (quest.Status is not (QuestStatus.Paused or QuestStatus.InProgress)) return;
        quest.Status = QuestStatus.Canceled;
        StopCoroutine(quest.Quest());
    }
    
    public QuestStatus GetQuestStatus(QuestView quest)
    {
        return quest.Status;
    }

    #endregion

    #endregion
}
