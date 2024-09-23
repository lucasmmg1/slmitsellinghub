#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InputView))]
public class InputViewPropertyDrawer : PropertyDrawer
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty actionName, inputAction, eventToExecute;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void Setup(SerializedProperty property)
    {
        SetupActionName(property);
        SetupInputAction(property);
        SetupEventToExecute(property);
    }

    protected void Show(Rect rect)
    {
        ShowActionName(rect);
        ShowInputAction(rect);
        ShowEventToExecute(rect);
    }

    protected void SetupActionName(SerializedProperty property)
    {
        actionName = property.FindPropertyRelative("actionName");
    }

    protected void ShowActionName(Rect rect)
    {
        var actionNameRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(actionNameRect, actionName, new GUIContent("Action name", "Name of the action"));
    }

    protected void SetupInputAction(SerializedProperty property)
    {
        inputAction = property.FindPropertyRelative("inputAction");
    }

    protected void ShowInputAction(Rect rect)
    {
        var inputActionRect =
            new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2, rect.width / 2 - 2.5f, 75);
        EditorGUI.PropertyField(inputActionRect, inputAction,
            new GUIContent("Input action", "Input action to listen to"));
    }

    protected void SetupEventToExecute(SerializedProperty property)
    {
        eventToExecute = property.FindPropertyRelative("eventToExecute");
    }

    protected void ShowEventToExecute(Rect rect)
    {
        var eventToExecuteRect = new Rect(rect.x + rect.width / 2 + 5, rect.y + EditorGUIUtility.singleLineHeight * 2,
            rect.width / 2 - 2.5f, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(eventToExecuteRect, eventToExecute,
            new GUIContent("Event to execute", "Event to execute when the input is triggered"));
    }

    #endregion

    #region Public Methods

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Setup(property);
        return EditorGUI.GetPropertyHeight(eventToExecute) + (EditorGUIUtility.singleLineHeight + 5) * 2;
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);
        Setup(property);
        Show(rect);
        EditorGUI.EndProperty();
    }

    #endregion

    #endregion
}

#endif