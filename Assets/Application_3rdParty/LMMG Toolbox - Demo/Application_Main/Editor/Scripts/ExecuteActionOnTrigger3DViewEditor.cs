#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(ExecuteActionOnTrigger3DView)), CanEditMultipleObjects]
public class ExecuteActionOnTrigger3DViewEditor : ExecuteActionOnPhysicsViewEditor
{
    #region Methods

    #region Public Methods

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }

    #endregion

    #endregion
}

#endif