
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

    [SerializeField] [TagSelector] protected string[] targetsTags;
    [SerializeField] protected float hybridUpdateRate = 0.1f;
    [SerializeField] protected bool debugRay;
    [SerializeField] protected Transform raycastOrigin;
    [SerializeField] protected Color debugRayColor;
    [SerializeField] protected LayerMask layerMaskToCheckForRaycastTarget;
    [SerializeField] protected UnityEvent<RaycastHit> eventToExecuteWhenRaycastTargetFound, eventToExecuteWhenRaycastTargetLost;
    [SerializeField] protected UnityEvent eventToExecuteIfRaycastTargetNotFound;
    [SerializeField] protected TransformDirections raycastDirection;
    [SerializeField] protected QueryTriggerInteraction queryTriggerInteraction;
    [SerializeField] protected RaycastType raycastType;
    [SerializeField] protected float maxRaycastDistance;
    [SerializeField] protected Vector3 boxcastSize;
    protected List<RaycastHit> previousHits, currentHits;
    
    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void OnEnable()
    {
        previousHits = new List<RaycastHit>();
        currentHits = new List<RaycastHit>();
        
        StartCoroutine(HybridUpdate());
    }
    protected void OnDisable()
    {
        previousHits.Clear();
        currentHits.Clear();
        
        StopAllCoroutines();
    }

/*#if UNITY_EDITOR
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
#endif*/

    protected IEnumerator HybridUpdate()
    {
        yield return new WaitForSeconds(hybridUpdateRate + 0.01f);
        
        previousHits = currentHits.Except(GetHits()).ToList();
        currentHits = GetHits().ToList();

        if (previousHits.Count > 0)
        {
            foreach (var previousHit in previousHits)
                eventToExecuteWhenRaycastTargetLost?.Invoke(previousHit);
        }
        else
        {
            
        }
        if (currentHits.Count > 0)
        {
            foreach (var currentHit in currentHits)
                eventToExecuteWhenRaycastTargetFound?.Invoke(currentHit);
        }
        else
        {
            eventToExecuteIfRaycastTargetNotFound?.Invoke();
        }
        
        StartCoroutine(HybridUpdate());
    }

    protected IEnumerable<RaycastHit> GetHits()
    {
        return raycastType switch
        {
            RaycastType.Line => GetLineCastHits(),
            RaycastType.Box => GetBoxCastHits(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    protected IEnumerable<RaycastHit> GetLineCastHits()
    {
        return Physics.RaycastAll
        (
            raycastOrigin.position,
            transform.GetTransformDirection(raycastDirection),
            maxRaycastDistance,
            layerMaskToCheckForRaycastTarget,
            queryTriggerInteraction
        ).Where(hit => targetsTags.Contains(hit.collider.tag));
    }
    protected IEnumerable<RaycastHit> GetBoxCastHits()
    {
        return Physics.BoxCastAll
        (
            new Vector3(raycastOrigin.position.x + boxcastSize.x / 2 * transform.GetTransformDirection(raycastDirection).x, raycastOrigin.position.y, raycastOrigin.position.z + boxcastSize.z / 2 * transform.GetTransformDirection(raycastDirection).z),
            boxcastSize / 2f, 
            transform.GetTransformDirection(raycastDirection),
            transform.rotation, 
            1, 
            layerMaskToCheckForRaycastTarget, 
            queryTriggerInteraction
        ).Where(hit => targetsTags.Contains(hit.collider.tag));
    }

    #endregion

    #endregion
}
