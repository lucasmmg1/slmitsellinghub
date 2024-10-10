using System.Collections;
using System.Linq;
using UnityEngine;

public class ChecarEquipamentosDeSeguranca : QuestView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected GameObject[] vestuarios;

    #endregion

    #endregion

    #region Methods

    #region Public Methods

    public override IEnumerator Quest()
    {
        do
        {
            Status = QuestStatus.InProgress;
            yield return new WaitForEndOfFrame();
        } while (vestuarios.Any(vestuario => vestuario.activeSelf));
        
        Status = QuestStatus.Completed;
    }

    #endregion

    #endregion
}