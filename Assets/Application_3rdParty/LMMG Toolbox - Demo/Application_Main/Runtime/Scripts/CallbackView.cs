using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CallbackView : MonoBehaviour
{
    #region Variables

    #region Public Variables

    public InputAction.CallbackContext CallbackContext { get; set; }

    #endregion

    #endregion
}