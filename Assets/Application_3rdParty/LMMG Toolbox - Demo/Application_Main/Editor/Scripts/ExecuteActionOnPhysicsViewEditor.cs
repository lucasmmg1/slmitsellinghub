#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExecuteActionOnPhysicsView)), CanEditMultipleObjects]
public class ExecuteActionOnPhysicsViewEditor : ExecuteActionOnConditionViewEditor
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty tagsToCollide,
        executeOnEnter,
        executeOnStay,
        executeOnExit,
        eventToExecuteOnEnter,
        eventToExecuteOnStay,
        eventToExecuteOnExit;

    protected bool eventsFoldout;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected override void SetupEvents()
    {
        base.SetupEvents();
        eventToExecuteOnEnter = serializedObject.FindProperty("eventToExecuteOnEnter");
        eventToExecuteOnStay = serializedObject.FindProperty("eventToExecuteOnStay");
        eventToExecuteOnExit = serializedObject.FindProperty("eventToExecuteOnExit");
        tagsToCollide = serializedObject.FindProperty("tagsToCollide");
    }

    protected override void ShowEvents()
    {
        EditorGUI.indentLevel++;
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(tagsToCollide,
            new GUIContent("Tag to collide with: ", "The tag of the object to collide with."));
        eventsFoldout = EditorGUILayout.Foldout(eventsFoldout, new GUIContent("Events: "));
        if (!eventsFoldout) return;
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(eventToExecuteOnEnter,
                new GUIContent("Event to execute on enter: ", "The event to execute on enter."),
                GUILayout.Width(Screen.width / 3f - 15));
            EditorGUILayout.PropertyField(eventToExecuteOnStay,
                new GUIContent("Event to execute on stay: ", "The event to execute on stay."),
                GUILayout.Width(Screen.width / 3f - 15));
            EditorGUILayout.PropertyField(eventToExecuteOnExit,
                new GUIContent("Event to execute on exit: ", "The event to execute on exit."),
                GUILayout.Width(Screen.width / 3f - 15));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUI.indentLevel--;
        EditorGUI.indentLevel--;
        base.ShowEvents();
    }

    protected override void SetupExecutionTime()
    {
        base.SetupExecutionTime();
        executeOnEnter = serializedObject.FindProperty("executeOnEnter");
        executeOnStay = serializedObject.FindProperty("executeOnStay");
        executeOnExit = serializedObject.FindProperty("executeOnExit");
    }

    protected override void ShowExecutionTime()
    {
        base.ShowExecutionTime();
        EditorGUILayout.Space();

        EditorGUI.indentLevel++;
        EditorGUI.indentLevel++;
        if (!executeOnStay.boolValue && !executeOnExit.boolValue)
            EditorGUILayout.PropertyField(executeOnEnter,
                new GUIContent("Execute on enter? ", "When will the event be executed."));

        if (!executeOnEnter.boolValue && !executeOnExit.boolValue)
            EditorGUILayout.PropertyField(executeOnStay,
                new GUIContent("Execute on stay? ", "When will the event be executed."));

        if (!executeOnEnter.boolValue && !executeOnStay.boolValue)
            EditorGUILayout.PropertyField(executeOnExit,
                new GUIContent("Execute on exit? ", "When will the event be executed."));

        if (executeOnEnter.boolValue && executeOnStay.boolValue && executeOnExit.boolValue)
            executeOnEnter.boolValue = executeOnStay.boolValue = executeOnExit.boolValue = false;
        EditorGUI.indentLevel--;
        EditorGUI.indentLevel--;
    }

    #endregion

    #endregion
}

#endif