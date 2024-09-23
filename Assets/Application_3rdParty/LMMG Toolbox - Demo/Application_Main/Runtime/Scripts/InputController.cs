using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Scripts/Hiscom Engine/Patterns/MMVCC/Controllers/Input Controller")] 
public class InputController : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected InputView[] inputViews;
    protected InputActionMap actionMap;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void Awake()
    {
        actionMap = new InputActionMap(gameObject.name);

        foreach (var inputView in inputViews)
            inputView.CreateInputMap(actionMap);
    }

    protected void OnEnable()
    {
        actionMap.Enable();
    }

    protected void OnDisable()
    {
        actionMap.Disable();
    }

    #endregion

    #endregion
}