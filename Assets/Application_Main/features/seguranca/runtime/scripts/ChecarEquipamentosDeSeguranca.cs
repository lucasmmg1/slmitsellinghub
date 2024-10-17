using System.Collections;
using System.Linq;
using UnityEngine;

public class ChecarEquipamentosDeSeguranca : QuestView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected OutlineObjectEffectView[] vestuarios;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void Start()
    {
        foreach (var vestuario in vestuarios)
            vestuario.StartFeedback();
    }

    #endregion
    
    #region Public Methods
    
    public override IEnumerator Quest()
    {
        do
        {
            Status = QuestStatus.InProgress;
            yield return new WaitForEndOfFrame();
        } while (vestuarios.Any(vestuario => vestuario.gameObject.activeSelf));
        
        Status = QuestStatus.Completed;
        Debug.Log("complete");
    }

    #endregion

    #endregion
}