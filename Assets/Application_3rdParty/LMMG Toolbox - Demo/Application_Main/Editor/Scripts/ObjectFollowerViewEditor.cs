#if UNITY_EDITOR

using Engine.Resources.Scripts.MVC.Views;
using UnityEngine;
    using UnityEditor;
    
    [CustomEditor(typeof(ObjectFollowerView))]
    public class ObjectFollowerViewEditor : Editor
    {
        #region Variables

        #region Private Variables

        private SerializedProperty _mouseSensibility, _hasHorizontalLimits, _hasVerticalLimits, _horizontalLimits, _verticalLimits, _objectToServeAsEyes;
        
        #endregion

        #endregion

        #region Methods

        #region Private Methods

        private void OnEnable()
        {
            _mouseSensibility = serializedObject.FindProperty("mouseSensibility");
            _hasHorizontalLimits = serializedObject.FindProperty("hasHorizontalLimits");
            _hasVerticalLimits = serializedObject.FindProperty("hasVerticalLimits");
            _horizontalLimits = serializedObject.FindProperty("horizontalLimits");
            _verticalLimits = serializedObject.FindProperty("verticalLimits");
            _objectToServeAsEyes = serializedObject.FindProperty("objectToServeAsEyes");
        }

        #endregion

        #region Public Methods

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_mouseSensibility, new GUIContent("Sensibility: ", "The mouse sensibility, in %."));
            EditorGUILayout.PropertyField(_objectToServeAsEyes, new GUIContent("Parent object: ", "The reference of the object that this camera will serve as eyes."));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Limits", EditorStyles.boldLabel);

            _hasHorizontalLimits.boolValue = EditorGUILayout.Toggle(new GUIContent("Has horizontal limits?", "Does the camera has horizontal limits?"), _hasHorizontalLimits.boolValue);
            if (_hasHorizontalLimits.boolValue)
            {
                EditorGUILayout.PropertyField(_horizontalLimits, new GUIContent("Limits: ", "A vector for the horizontal limits of the camera."));
                EditorGUILayout.Space();
            }

            _hasVerticalLimits.boolValue = EditorGUILayout.Toggle(new GUIContent("Has vertical limits?", "Does the camera has vertical limits?"), _hasVerticalLimits.boolValue);
            if (_hasVerticalLimits.boolValue)
            {
                EditorGUILayout.PropertyField(_verticalLimits, new GUIContent("Limits: ", "A vector for the vertical limits of the camera."));
                EditorGUILayout.Space();
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #endregion
    } 
#endif