using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Scripts/Engine/Patterns/MMVCC/Views/Execute Action on Condition View")]
public class ExecuteActionOnConditionView : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected float timerToWaitBeforeExecutingAction;
    [SerializeField] protected bool repeatEvent, executeOnEnable, executeOnStart;
    [SerializeField] protected ConditionsView[] eventConditions;
    [SerializeField] protected UnityEvent eventToExecuteIfConditionTrue, eventToExecuteIfConditionFalse;

    #endregion

    #endregion

    #region Methods

    #region Private Methods

    private IEnumerator ExecuteActionCoroutine()
    {
        var eventToExecute = ConditionsView.CheckConditions(eventConditions)
            ? eventToExecuteIfConditionTrue
            : eventToExecuteIfConditionFalse;
        yield return new WaitForSeconds(timerToWaitBeforeExecutingAction);
        eventToExecute?.Invoke();

        if (repeatEvent && gameObject.activeSelf)
            StartCoroutine(ExecuteActionCoroutine());
    }

    #endregion

    #region Protected Methods

    protected virtual void OnEnable()
    {
        if (executeOnEnable)
            StartCoroutine(ExecuteActionCoroutine());
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual void Start()
    {
        if (executeOnStart)
            StartCoroutine(ExecuteActionCoroutine());
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Executes the action
    /// </summary>
    public virtual void ExecuteAction()
    {
        StartCoroutine(ExecuteActionCoroutine());
    }

    #endregion

    #endregion
}