using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TransformDirections = TransformExtensions.TransformDirections;

public class ExecuteActionOnRaycastTargetSeen3DView : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    [SerializeField] [TagSelector] protected string[] tagsToCollide;
    [SerializeField] protected float hybridUpdateRate = 0.1f;
    [SerializeField] protected bool debugRay, lookBehindObjects;
    [SerializeField] protected Transform raycastOrigin;
    [SerializeField] protected Color debugRayColor;
    [SerializeField] protected LayerMask layerMaskToCheckForRaycastTarget;
    [SerializeField] protected UnityEvent<RaycastHit> eventToExecuteWhenRaycastTargetFound;
    [SerializeField] protected UnityEvent<RaycastHit> eventToExecuteWhenRaycastTargetLost;
    [SerializeField] protected TransformDirections raycastDirection;
    [SerializeField] protected QueryTriggerInteraction queryTriggerInteraction;
    [SerializeField] protected RaycastType raycastType;
    [SerializeField] protected float maxRaycastDistance;
    [SerializeField] protected Vector3 boxcastSize;
    protected Vector3 raycastOriginPosition;
    protected List<RaycastHit> raycastHits;
    protected bool isSeeingTarget;
    
    #endregion

    #region Public Variables

    public bool IsSeeingTarget => isSeeingTarget;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void OnEnable()
    {
        raycastHits = new List<RaycastHit>();
        StartCoroutine(HybridUpdate());
    }

    protected void OnDisable()
    {
        raycastHits.Clear();
        StopAllCoroutines();
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        if (!debugRay) return;
        Gizmos.color = debugRayColor;
        raycastOriginPosition = raycastOrigin.position;

        switch (raycastType)
        {
            case RaycastType.Line:
                Gizmos.DrawRay(raycastOrigin.position,
                    transform.GetTransformDirection(raycastDirection) * maxRaycastDistance);
                break;

            case RaycastType.Box:
                Gizmos.DrawWireCube(
                    new Vector3(
                        raycastOriginPosition.x +
                        boxcastSize.x / 2 * transform.GetTransformDirection(raycastDirection).x,
                        raycastOriginPosition.y,
                        raycastOriginPosition.z +
                        boxcastSize.z / 2 * transform.GetTransformDirection(raycastDirection).z), boxcastSize);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
#endif

    protected IEnumerator HybridUpdate()
    {
        yield return new WaitForSeconds(hybridUpdateRate + 0.01f);

        raycastHits.Clear();
        raycastOriginPosition = raycastOrigin.position;
        raycastHits = raycastType switch
        {
            RaycastType.Line => Physics.RaycastAll(raycastOriginPosition, transform.GetTransformDirection(raycastDirection), maxRaycastDistance, layerMaskToCheckForRaycastTarget, queryTriggerInteraction).ToList(),
            RaycastType.Box => Physics.BoxCastAll(new Vector3(raycastOriginPosition.x + boxcastSize.x / 2 * transform.GetTransformDirection(raycastDirection).x, raycastOriginPosition.y, raycastOriginPosition.z + boxcastSize.z / 2 * transform.GetTransformDirection(raycastDirection).z), boxcastSize / 2f, transform.GetTransformDirection(raycastDirection), transform.rotation, 1, layerMaskToCheckForRaycastTarget, queryTriggerInteraction).ToList(),
            _ => throw new ArgumentOutOfRangeException()
        };

        foreach (var raycastHit in raycastHits)
        {
            if (raycastHit.collider == null) continue;

            foreach (var tagToCollide in tagsToCollide)
            {
                var objectFoundInFront = Physics.Raycast(raycastOriginPosition, transform.GetTransformDirection(raycastDirection), Vector3.Distance(raycastOriginPosition, raycastHit.collider.transform.position), layerMaskToCheckForRaycastTarget, queryTriggerInteraction);

                switch (lookBehindObjects)
                {
                    case true when raycastHit.collider.CompareTag(tagToCollide):
                        eventToExecuteWhenRaycastTargetFound?.Invoke(raycastHit);
                        isSeeingTarget = true;
                        goto endOfMethod;
                    case false when raycastHit.collider.CompareTag(tagToCollide) && !objectFoundInFront:
                        eventToExecuteWhenRaycastTargetFound?.Invoke(raycastHit);
                        isSeeingTarget = true;
                        goto endOfMethod;
                }
            }
        }
        
        eventToExecuteWhenRaycastTargetLost?.Invoke();
        isSeeingTarget = false;
        
        endOfMethod:
        StartCoroutine(HybridUpdate());
    }

    #endregion

    #endregion
}