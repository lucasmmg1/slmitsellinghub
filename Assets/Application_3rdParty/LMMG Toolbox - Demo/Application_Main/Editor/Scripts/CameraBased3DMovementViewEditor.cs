#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(CameraBased3DMovementView)), CanEditMultipleObjects]
public class CameraBased3DMovementViewEditor : Physical3DMovementsViewEditor
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