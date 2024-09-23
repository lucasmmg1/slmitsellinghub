using UnityEngine;
using UnityEngine.Events;

public abstract class ExecuteActionOnPhysicsView : ExecuteActionOnConditionView
{
    #region Variables

    #region Protected Variables

    [SerializeField] [TagSelector] protected string[] tagsToCollide;
    [SerializeField] protected bool executeOnEnter, executeOnStay, executeOnExit;
    [SerializeField] protected UnityEvent eventToExecuteOnEnter, eventToExecuteOnStay, eventToExecuteOnExit;

    #endregion

    #endregion
}