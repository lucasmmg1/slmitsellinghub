#if UNITY_EDITOR

using UnityEditorInternal;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputController)), CanEditMultipleObjects]
public class InputControllerEditor : Editor
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty inputViews;
    protected ReorderableList inputViewsList;

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
        SetupInputConnectors();
    }

    protected void Show()
    {
        ShowInputConnectors();
    }

    protected void SetupInputConnectors()
    {
        inputViews = serializedObject.FindProperty("inputViews");
        inputViewsList = new ReorderableList(serializedObject, inputViews, true, true, true, true)
        {
            drawHeaderCallback = rect =>
                EditorGUI.LabelField(rect, new GUIContent("Inputs", "The inputs that the object has.")),
            drawElementCallback = (rect, index, _, _) =>
            {
                var element = inputViewsList.serializedProperty.GetArrayElementAtIndex(index);
                var actionName = element.FindPropertyRelative("actionName");
                var foldoutRect = new Rect(rect.x + 10, rect.y, rect.width - 10, EditorGUIUtility.singleLineHeight);
                var foldoutGUIContent =
                    new GUIContent(actionName.stringValue == string.Empty ? "New Input" : actionName.stringValue,
                        "The input object");

                element.isExpanded = EditorGUI.Foldout(foldoutRect, element.isExpanded, foldoutGUIContent);
                if (!element.isExpanded) return;

                var elementRect = new Rect(rect.x + 10, rect.y + EditorGUIUtility.singleLineHeight, rect.width - 10,
                    EditorGUI.GetPropertyHeight(element));
                EditorGUI.PropertyField(elementRect, element, GUIContent.none);
            },
            elementHeightCallback = index =>
            {
                var element = inputViewsList.serializedProperty.GetArrayElementAtIndex(index);
                if (!element.isExpanded) return EditorGUIUtility.singleLineHeight;
                return EditorGUI.GetPropertyHeight(element) + 25;
            }
        };
    }

    protected void ShowInputConnectors()
    {
        inputViewsList.DoLayoutList();
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