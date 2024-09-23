#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IsKeyHoldConditionView)), CanEditMultipleObjects]
public class IsKeyHoldConditionViewEditor : Editor
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty timeToCheckHold, keyToCheckIfHold;

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
        SetupRequiredPresses();
        SetupKeyToCheck();
    }

    protected void Show()
    {
        ShowRequiredPresses();
        ShowKeyToCheck();
    }

    protected void SetupRequiredPresses()
    {
        timeToCheckHold = serializedObject.FindProperty("timeToCheckHold");
    }

    protected void ShowRequiredPresses()
    {
        EditorGUILayout.PropertyField(timeToCheckHold,
            new GUIContent("Time to check required presses: ",
                "The required amount of time to check if the key is hold."));
    }

    protected void SetupKeyToCheck()
    {
        keyToCheckIfHold = serializedObject.FindProperty("keyToCheckIfHold");
    }

    protected void ShowKeyToCheck()
    {
        EditorGUILayout.PropertyField(keyToCheckIfHold, new GUIContent("Key to check: ", "The key to check if hold."));
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

#endif