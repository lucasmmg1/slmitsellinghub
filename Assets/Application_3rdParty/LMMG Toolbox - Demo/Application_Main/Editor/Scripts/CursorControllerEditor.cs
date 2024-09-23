#if UNITY_EDITOR

namespace HiscomEngine.Core.Editor.Scripts
{
    using UnityEditor;
    using UnityEngine;
    using Runtime.Scripts.Patterns.MMVCC.Controllers;

    [CustomEditor(typeof(CursorController)), CanEditMultipleObjects]
    public class CursorControllerEditor : Editor
    {
        #region Variables

        #region Protected Variables

        protected SerializedProperty cursorIcon, cursorLockMode;
        
        #endregion

        #endregion

        #region Methods

        #region Protected Methods

        protected void OnEnable()
        {
            Setup();
        }

        protected void Setup()
        {
            SetupCursor();
        }
        protected void Show()
        {
            ShowCursor();
        }

        protected void SetupCursor()
        {
            cursorIcon = serializedObject.FindProperty("cursorIcon");
            cursorLockMode = serializedObject.FindProperty("cursorLockMode");
        }
        protected void ShowCursor()
        {
            EditorGUILayout.PropertyField(cursorIcon, new GUIContent("Cursor icon: ", "The reference to the cursor icon sprite."));
            EditorGUILayout.PropertyField(cursorLockMode, new GUIContent("Cursor lock mode: ", "The cursor lock mode to use."));
        }

        #endregion

        #region Public Methods

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Show();
            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #endregion
    }
}

#endif