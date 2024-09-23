#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovementsView)), CanEditMultipleObjects]
public class MovementsViewEditor : Editor
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty playerView,
        hasMovementConditions,
        hasMovementAnimation,
        hasMovementSounds,
        movementConditions,
        canMoveEvent,
        cannotMoveEvent,
        movementAnimator,
        defaultAnimationClip,
        movementAnimationClip,
        movementSound;

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
        SetupPlayer();
        SetupConditions();
        SetupAnimation();
        SetupSounds();
        SetupEvents();
    }

    protected virtual void Show()
    {
        ShowPlayer();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField(new GUIContent("General Movement Settings"), EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUI.indentLevel++;
        ShowConditions();
        ShowAnimation();
        ShowSounds();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        ShowEvents();
        EditorGUI.indentLevel--;
    }

    protected virtual void SetupPlayer()
    {
        playerView = serializedObject.FindProperty("playerView");
    }

    protected virtual void ShowPlayer()
    {
        EditorGUILayout.PropertyField(playerView, new GUIContent("Player", "The reference for the player."));
    }

    protected virtual void SetupConditions()
    {
        hasMovementConditions = serializedObject.FindProperty("hasMovementConditions");
        movementConditions = serializedObject.FindProperty("movementConditions");
    }

    protected virtual void ShowConditions()
    {
        EditorGUILayout.PropertyField(hasMovementConditions,
            new GUIContent("Has movement conditions?", "Does the rigidbody has movement conditions?"));

        EditorGUI.indentLevel++;
        if (hasMovementConditions.boolValue)
        {
            EditorGUILayout.PropertyField(movementConditions,
                new GUIContent("Conditions", "The references for the movement conditions."));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUI.indentLevel--;
    }

    protected virtual void SetupAnimation()
    {
        hasMovementAnimation = serializedObject.FindProperty("hasMovementAnimation");
        movementAnimator = serializedObject.FindProperty("movementAnimator");
        defaultAnimationClip = serializedObject.FindProperty("defaultAnimationClip");
        movementAnimationClip = serializedObject.FindProperty("movementAnimationClip");
    }

    protected virtual void ShowAnimation()
    {
        EditorGUILayout.PropertyField(hasMovementAnimation,
            new GUIContent("Has movement animation?", "Does the rigidbody has movement animation?"));

        EditorGUI.indentLevel++;
        if (hasMovementAnimation.boolValue)
        {
            EditorGUILayout.PropertyField(movementAnimator,
                new GUIContent("Animator", "The reference for the movement animator."));
            EditorGUILayout.PropertyField(defaultAnimationClip,
                new GUIContent("Default animation clip", "The reference for the default animation clip."));
            EditorGUILayout.PropertyField(movementAnimationClip,
                new GUIContent("Movement animation clip", "The reference for the movement animation clip."));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUI.indentLevel--;
    }

    protected virtual void SetupSounds()
    {
        hasMovementSounds = serializedObject.FindProperty("hasMovementSounds");
        movementSound = serializedObject.FindProperty("movementSound");
    }

    protected virtual void ShowSounds()
    {
        EditorGUILayout.PropertyField(hasMovementSounds,
            new GUIContent("Has movement sounds?", "Does the rigidbody has movement sounds?"));

        EditorGUI.indentLevel++;
        if (hasMovementSounds.boolValue)
        {
            EditorGUILayout.PropertyField(movementSound,
                new GUIContent("Sound", "The reference for the movement sound."));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUI.indentLevel--;
    }

    protected virtual void SetupEvents()
    {
        canMoveEvent = serializedObject.FindProperty("canMoveEvent");
        cannotMoveEvent = serializedObject.FindProperty("cannotMoveEvent");
    }

    protected virtual void ShowEvents()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.PropertyField(canMoveEvent,
                new GUIContent("Can move post-action: ",
                    "The event to be called when the movement conditions are met."));
            EditorGUILayout.PropertyField(cannotMoveEvent,
                new GUIContent("Cannot move post-action: ",
                    "The event to be called when the movement conditions are not met."));
        }
        EditorGUILayout.EndHorizontal();
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