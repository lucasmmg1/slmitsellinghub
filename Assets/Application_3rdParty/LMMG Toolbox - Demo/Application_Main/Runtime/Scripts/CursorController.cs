namespace HiscomEngine.Runtime.Scripts.Patterns.MMVCC.Controllers
{
    using UnityEngine;
    
    [AddComponentMenu("Scripts/Hiscom Engine/Patterns/MMVCC/Controllers/Cursor Controller")]
    public class CursorController : MonoBehaviour
    {
        #region Variables

        #region Protected Variables
        
        [SerializeField] protected Texture2D cursorIcon;
        [SerializeField] protected CursorLockMode cursorLockMode;

        #endregion
        
        #endregion

        #region Methods

        #region Protected Methods

        protected void Start()
        {
            ChangeCursorVisibility(false);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the cursor visibility.
        /// </summary>
        /// <param name="isVisible"> The visibility boolean value to use </param>
        public void ChangeCursorVisibility(bool isVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = isVisible ? CursorLockMode.None : cursorLockMode;
            Cursor.SetCursor (isVisible ? null : cursorIcon, Vector2.zero, isVisible ? CursorMode.Auto : CursorMode.ForceSoftware);
        }

        /// <summary>
        /// Inverts the cursor visibility state.
        /// </summary>
        public void InvertCursorVisibilityState()
        {
            ChangeCursorVisibility(!Cursor.visible);
        }
        
        #endregion

        #endregion
    }
}