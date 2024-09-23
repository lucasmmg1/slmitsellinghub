using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class InputView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected string actionName;
    [SerializeField] protected InputAction inputAction;
    [SerializeField] protected UnityEvent<InputAction.CallbackContext> eventToExecute;

    #endregion

    #endregion

    #region Methods

    #region Public Methods

    public void CreateInputMap(InputActionMap actionMap)
    {
        actionMap.AddAction(actionName, inputAction.type, null, inputAction.interactions, inputAction.processors);
        actionMap[actionName].performed += ctx => eventToExecute?.Invoke(ctx);

        foreach (var binding in inputAction.bindings)
            actionMap[actionName].AddBinding(binding);
    }

    #endregion

    #endregion
}