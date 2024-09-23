#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Physical3DMovementsView)), CanEditMultipleObjects]
public class Physical3DMovementsViewEditor : MovementsViewEditor
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty useIndividualForces, movementForce, movementForceVector;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected override void Setup()
    {
        base.Setup();
        SetupMovementForce();
    }

    protected override void Show()
    {
        base.Show();
        EditorGUILayout.LabelField(new GUIContent("Physical Movement Settings"), EditorStyles.boldLabel);
        ShowMovementForce();
    }

    protected virtual void SetupMovementForce()
    {
        useIndividualForces = serializedObject.FindProperty("useIndividualForces");
        movementForce = serializedObject.FindProperty("movementForce");
        movementForceVector = serializedObject.FindProperty("movementForceVector");
    }

    protected virtual void ShowMovementForce()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(useIndividualForces,
            new GUIContent("Use individual forces?",
                "Does individual forces have to be applied for each movement axis?"));

        switch (useIndividualForces.boolValue)
        {
            case true:
                EditorGUILayout.PropertyField(movementForceVector,
                    new GUIContent("Movement force",
                        "The force vector that will be applied at the referenced rigidbody."));
                break;

            case false:
                EditorGUILayout.PropertyField(movementForce,
                    new GUIContent("Movement force", "The force that will be applied at the referenced rigidbody."));
                break;
        }

        EditorGUI.indentLevel--;
    }

    #endregion

    #endregion
}

#endif