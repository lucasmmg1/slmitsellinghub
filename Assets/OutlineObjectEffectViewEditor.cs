using HiscomEngine.Runtime.Scripts.Patterns.MMVCC.Views;

#if UNITY_EDITOR

namespace HiscomEngine.Addons.Effects.Editor.Scripts
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(OutlineObjectEffectView)), CanEditMultipleObjects]
    public class OutlineObjectEffectViewEditor : Editor
    {
        #region Variables

        #region Private Variables

        private SerializedProperty _outlineWidth, _blinkSpeed, _outlineColor, _outlineMode;

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        private void OnEnable()
        {
            _outlineWidth = serializedObject.FindProperty("outlineWidth");
            _blinkSpeed = serializedObject.FindProperty("blinkSpeed");
            _outlineColor = serializedObject.FindProperty("outlineColor");
            _outlineMode = serializedObject.FindProperty("outlineMode");
        }

        #endregion

        #region Public Methods

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_blinkSpeed, new GUIContent("Blink speed:", "The speed of the blinking."));
            EditorGUILayout.PropertyField(_outlineMode, new GUIContent("Outline mode:", "The mode of the outline."));
            EditorGUILayout.PropertyField(_outlineWidth, new GUIContent("Outline width:", "The width of the outline."));
            EditorGUILayout.PropertyField(_outlineColor, new GUIContent("Outline color:", "The color of the outline."));
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
        
        #endregion
    }
}

#endif