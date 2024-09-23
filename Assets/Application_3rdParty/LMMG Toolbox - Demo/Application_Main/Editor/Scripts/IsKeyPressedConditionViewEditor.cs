#if UNITY_EDITOR

namespace HiscomEngine.Editor.Scripts
{
    using UnityEditor;
    using UnityEngine;
    using Runtime.Scripts.Patterns.MMVCC.Views;

    [CustomEditor(typeof(IsKeyPressedConditionView)), CanEditMultipleObjects]
    public class IsKeyPressedConditionViewEditor : Editor
    {
        #region Variables

        #region Protected Variables

        protected SerializedProperty numberOfRequiredPresses, timeToCheckRequiredPresses, useTime, compareType, keyToCheckIfPressed;

        #endregion

        #endregion

        #region Methods

        #region Protected Methods
        
        protected void OnEnable()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            SetupRequiredPresses();
            SetupUseTime();
            SetupKeyToCheck();
        }
        protected virtual void Show()
        {
            ShowRequiredPresses();
            ShowUseTime();
            ShowKeyToCheck();
        }
        
        protected virtual void SetupRequiredPresses()
        {
            compareType = serializedObject.FindProperty("compareType");
            numberOfRequiredPresses = serializedObject.FindProperty("numberOfRequiredPresses");
        }
        protected virtual void ShowRequiredPresses()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(new GUIContent("Number of required presses: ", "The number of times the key must be pressed to be considered pressed."));
                EditorGUILayout.PropertyField(compareType, GUIContent.none);
                EditorGUILayout.PropertyField(numberOfRequiredPresses, GUIContent.none);
            }
            EditorGUILayout.EndHorizontal();
        }
        protected virtual void SetupUseTime()
        {
            useTime = serializedObject.FindProperty("useTime");
            timeToCheckRequiredPresses = serializedObject.FindProperty("timeToCheckRequiredPresses");
        }

        protected virtual void ShowUseTime()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(useTime, new GUIContent("Use time: ", "If true, the time to check the required presses will be used."));
                if (useTime.boolValue)
                {
                    EditorGUILayout.PropertyField(timeToCheckRequiredPresses, new GUIContent("", "The maximum time to wait before checking if the key was pressed the required number of times in seconds."));
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        
        protected virtual void SetupKeyToCheck()
        {
            keyToCheckIfPressed = serializedObject.FindProperty("keyToCheckIfPressed");
        }
        protected virtual void ShowKeyToCheck()
        {
            EditorGUILayout.PropertyField(keyToCheckIfPressed, new GUIContent("Key to check: ", "The key to check if pressed."));
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