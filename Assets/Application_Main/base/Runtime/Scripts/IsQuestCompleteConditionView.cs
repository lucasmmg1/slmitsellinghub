using System.Linq;
using UnityEngine;

public class IsQuestCompleteConditionView : ConditionsView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected QuestsController questsController;
    [SerializeField] protected QuestView[] questsToCheck;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected override bool Condition()
    {
        return questsToCheck.All(quest => questsController.GetQuestStatus(quest) == QuestStatus.Completed);
    }

    #endregion

    #endregion
}