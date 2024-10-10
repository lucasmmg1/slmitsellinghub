#if UNITY_EDITOR

using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExecuteActionOnRaycastTargetSeen3DView))]
public class ExecuteActionOnRaycastTargetSeen3DViewEditor : Editor
{
    #region Variables

    #region Protected Variables

    protected SerializedProperty targetsTags, eventConditions, hybridUpdateRate, debugRay, raycastOrigin, debugRayColor, layerMaskToCheckForRaycastTarget, eventToExecuteWhenRaycastTargetFoundAndConditionsTrue, eventToExecuteWhenRaycastTargetFoundAndConditionsFalse, eventToExecuteWhenRaycastTargetLost, eventToExecuteIfRaycastTargetNotFoundAndConditionsTrue, eventToExecuteIfRaycastTargetNotFoundAndConditionsFalse, raycastDirection, queryTriggerInteraction, raycastType, maxRaycastDistance, boxcastSize;

    protected bool advancedSettingsFoldout, showRaycastTargetFoundEvents, showRaycastTargetNotFoundEvents;

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
        SetupTagsToCollide();
        SetupConditions();
        SetupRaycastSettings();
        SetupEvents();
        SetupAdvancedSettings();
    }

    protected virtual void Show()
    {
        EditorGUILayout.LabelField(new GUIContent("Collision Settings", "Settings related to collision detection."), EditorStyles.boldLabel);
        ShowTagsToCollide();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.LabelField(new GUIContent("Conditions Settings", "Settings related to conditions."), EditorStyles.boldLabel);
        ShowConditions();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField(new GUIContent("Raycast Settings", "Settings related to raycasting detection."), EditorStyles.boldLabel);
        ShowRaycastSettings();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.LabelField(new GUIContent("Events Settings", "Settings related to events executioning."), EditorStyles.boldLabel);
        ShowEvents();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        ShowAdvancedSettings();
    }

    protected virtual void SetupTagsToCollide()
    {
        targetsTags = serializedObject.FindProperty("targetsTags");
    }

    protected virtual void ShowTagsToCollide()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(targetsTags, new GUIContent("Tag to collide with: ", "The tag of the object to collide with."));
        EditorGUI.indentLevel--;
    }
    protected void SetupConditions()
    {
        eventConditions = serializedObject.FindProperty("eventConditions");
    }
    protected void ShowConditions()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(eventConditions, new GUIContent("Conditions", "The conditions to check."));
        EditorGUI.indentLevel--;
    }

    protected virtual void SetupRaycastSettings()
    {
        debugRay = serializedObject.FindProperty("debugRay");
        raycastOrigin = serializedObject.FindProperty("raycastOrigin");
        debugRayColor = serializedObject.FindProperty("debugRayColor");
        layerMaskToCheckForRaycastTarget = serializedObject.FindProperty("layerMaskToCheckForRaycastTarget");
        raycastDirection = serializedObject.FindProperty("raycastDirection");
        queryTriggerInteraction = serializedObject.FindProperty("queryTriggerInteraction");
        raycastType = serializedObject.FindProperty("raycastType");
        maxRaycastDistance = serializedObject.FindProperty("maxRaycastDistance");
        boxcastSize = serializedObject.FindProperty("boxcastSize");
    }

    protected virtual void ShowRaycastSettings()
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(debugRay, new GUIContent("Debug ray?", "Check this option to debug the ray emitted by the object."));
        if (debugRay.boolValue) 
            EditorGUILayout.PropertyField(debugRayColor, new GUIContent("Debug ray color", "The color of the debug ray."));
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(raycastOrigin, new GUIContent("Raycast origin", "The origin of the ray."));
        EditorGUILayout.PropertyField(raycastDirection, new GUIContent("Raycast direction", "The direction of the ray."));
        EditorGUILayout.PropertyField(queryTriggerInteraction, new GUIContent("Query trigger interaction", "The query trigger interaction."));
        EditorGUILayout.PropertyField(layerMaskToCheckForRaycastTarget, new GUIContent("Layer mask to check for raycast target", "The layer mask to check for raycast target."));
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(raycastType, new GUIContent("Raycast type", "The raycast type."));
        switch ((RaycastType)raycastType.enumValueIndex)
        {
            case RaycastType.Line:
                EditorGUILayout.PropertyField(maxRaycastDistance, new GUIContent("Max distance", "The maximum (in meters) distance the ray can travel."));
                break;
            case RaycastType.Box:
                EditorGUILayout.PropertyField(boxcastSize, new GUIContent("Boxcast size", "The size of the boxcast."));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        EditorGUILayout.Space();
        EditorGUI.indentLevel--;
    }

    protected virtual void SetupEvents()
    {
        eventToExecuteWhenRaycastTargetFoundAndConditionsTrue = serializedObject.FindProperty("eventToExecuteWhenRaycastTargetFoundAndConditionsTrue");
        eventToExecuteWhenRaycastTargetFoundAndConditionsFalse = serializedObject.FindProperty("eventToExecuteWhenRaycastTargetFoundAndConditionsFalse");
        eventToExecuteWhenRaycastTargetLost = serializedObject.FindProperty("eventToExecuteWhenRaycastTargetLost");
        eventToExecuteIfRaycastTargetNotFoundAndConditionsTrue = serializedObject.FindProperty("eventToExecuteIfRaycastTargetNotFoundAndConditionsTrue");
        eventToExecuteIfRaycastTargetNotFoundAndConditionsFalse = serializedObject.FindProperty("eventToExecuteIfRaycastTargetNotFoundAndConditionsFalse");
    }

    protected virtual void ShowEvents()
    {
        EditorGUI.indentLevel++;
        {
            showRaycastTargetFoundEvents = EditorGUILayout.Foldout(showRaycastTargetFoundEvents, new GUIContent("Events to execute if raycast target found...", "Events to execute when raycast target found."));
            if (showRaycastTargetFoundEvents)
            {
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.PropertyField(eventToExecuteWhenRaycastTargetFoundAndConditionsTrue, new GUIContent("...and conditions true", "The event to execute if raycast target found is true and conditions are true."));
                    EditorGUILayout.PropertyField(eventToExecuteWhenRaycastTargetFoundAndConditionsFalse, new GUIContent("...and conditions false", "The event to execute if raycast target found is true and conditions are false."));
                }
                EditorGUI.indentLevel--;
            }
            showRaycastTargetNotFoundEvents = EditorGUILayout.Foldout(showRaycastTargetNotFoundEvents, new GUIContent("Events to execute if raycast target not found...", "Events to execute if raycast target not found."));
            if (showRaycastTargetNotFoundEvents)
            {
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.PropertyField(eventToExecuteIfRaycastTargetNotFoundAndConditionsTrue, new GUIContent("...and conditions true", "The event to execute if raycast target not found and conditions are true."));
                    EditorGUILayout.PropertyField(eventToExecuteIfRaycastTargetNotFoundAndConditionsFalse, new GUIContent("...and conditions false", "The event to execute if raycast target not found and conditions are false."));
                }
                EditorGUI.indentLevel--;
            } 
        }

        EditorGUI.indentLevel--;
    }

    protected virtual void SetupAdvancedSettings()
    {
        hybridUpdateRate = serializedObject.FindProperty("hybridUpdateRate");
        advancedSettingsFoldout = false;
    }

    protected virtual void ShowAdvancedSettings()
    {
        advancedSettingsFoldout = EditorGUILayout.Foldout(advancedSettingsFoldout, new GUIContent("Advanced settings", "Advanced settings for the inventory controller."));
        if (!advancedSettingsFoldout) return;
        EditorGUILayout.LabelField("ONLY CHANGE THIS SETTINGS IF YOU KNOW WHAT YOU ARE DOING!", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(hybridUpdateRate, new GUIContent($"Hybrid update rate", $"The rate fro the hybrid update."));
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