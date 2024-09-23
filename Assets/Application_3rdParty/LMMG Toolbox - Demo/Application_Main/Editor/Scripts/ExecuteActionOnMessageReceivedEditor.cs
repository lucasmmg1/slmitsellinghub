#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExecuteActionOnMessageReceivedView))]
public class ExecuteActionOnMessageReceivedEditor : Editor
{
    #region Variables

    #region Private Variables

    protected SerializedProperty notificationToReceive, eventToExecuteWhenReceiveNotification;

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
        SetupNotification();
        SetupEvent();
    }

    protected virtual void Show()
    {
        ShowNotification();
        ShowEvent();
    }

    protected virtual void SetupNotification()
    {
        notificationToReceive = serializedObject.FindProperty("notificationToReceive");
    }

    protected virtual void ShowNotification()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(notificationToReceive,
            new GUIContent("Notification to receive: ",
                "The notification this object has to receive in order to execute it's event"));
        EditorGUI.indentLevel--;
    }

    protected virtual void SetupEvent()
    {
        eventToExecuteWhenReceiveNotification = serializedObject.FindProperty("eventToExecuteWhenReceiveNotification");
    }

    protected virtual void ShowEvent()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(eventToExecuteWhenReceiveNotification,
            new GUIContent("Event to execute when receive notification: ",
                "The event that's gonna be executed when receive the notification"));
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