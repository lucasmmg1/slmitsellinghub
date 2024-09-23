#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExecuteActionOnConditionView)), CanEditMultipleObjects]
public class ExecuteActionOnConditionViewEditor : Editor
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty timerToWaitBeforeExecutingAction,
        repeatEvent,
        executeOnEnable,
        executeOnStart,
        eventConditions,
        eventToExecuteIfConditionIsTrue,
        eventToExecuteIfConditionIsFalse;

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
        SetupExecutionTime();
        SetupEvents();
    }

    protected virtual void Show()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField(new GUIContent("Execution Settings", "The settings related to execution"),
            EditorStyles.boldLabel);
        ShowExecutionTime();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField(new GUIContent("Events Settings", "Settings related to event executioning"),
            EditorStyles.boldLabel);
        ShowEvents();
    }

    protected virtual void SetupExecutionTime()
    {
        timerToWaitBeforeExecutingAction = serializedObject.FindProperty("timerToWaitBeforeExecutingAction");
        repeatEvent = serializedObject.FindProperty("repeatEvent");
        executeOnEnable = serializedObject.FindProperty("executeOnEnable");
        executeOnStart = serializedObject.FindProperty("executeOnStart");
    }

    protected virtual void ShowExecutionTime()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(timerToWaitBeforeExecutingAction,
            new GUIContent("Timer to wait before executing action: ", "The time to wait before executing the action."),
            true);
        EditorGUILayout.PropertyField(repeatEvent, new GUIContent("Repeat event? ", "Is the event repeatable?"), true);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(new GUIContent("Execute on: ", "When the event will be executed."));
        EditorGUI.indentLevel++;
        if (!executeOnStart.boolValue)
            EditorGUILayout.PropertyField(executeOnEnable,
                new GUIContent("Enable?", "Will the event be executed when the script be enabled?."), true);

        if (!executeOnEnable.boolValue)
            EditorGUILayout.PropertyField(executeOnStart,
                new GUIContent("Start?", "Will the event be executed when the script be started?."), true);

        if (executeOnStart.boolValue && executeOnEnable.boolValue)
            executeOnStart.boolValue = executeOnEnable.boolValue = false;
        EditorGUI.indentLevel--;
        EditorGUI.indentLevel--;
    }

    protected virtual void SetupEvents()
    {
        eventToExecuteIfConditionIsTrue = serializedObject.FindProperty("eventToExecuteIfConditionTrue");
        eventToExecuteIfConditionIsFalse = serializedObject.FindProperty("eventToExecuteIfConditionFalse");
        eventConditions = serializedObject.FindProperty("eventConditions");
    }

    protected virtual void ShowEvents()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(eventConditions,
            new GUIContent("Event conditions: ", "The conditions for the event to be executed."), true);
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(eventToExecuteIfConditionIsTrue,
                new GUIContent("Event to execute if condition is true: ",
                    "The event to execute if condition(s) is(are) true."));
            EditorGUILayout.PropertyField(eventToExecuteIfConditionIsFalse,
                new GUIContent("Event to execute if condition is false: ",
                    "The event to execute if condition(s) is(are) false."));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel--;
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