using System.Linq;
using UnityEngine;

[AddComponentMenu("Scripts/Engine/Patterns/MMVCC/Views/Execute Action on Trigger 3D View")]
public class ExecuteActionOnTrigger3DView : ExecuteActionOnPhysicsView
{
    #region Methods

    #region Protected Methods

    protected void OnTriggerEnter(Collider collider3d)
    {
        if (!tagsToCollide.Any(collider3d.CompareTag)) return;
        eventToExecuteOnEnter?.Invoke();
        if (!executeOnEnter) return;
        ExecuteAction();
    }

    protected void OnTriggerStay(Collider collider3d)
    {
        if (!tagsToCollide.Any(collider3d.CompareTag)) return;
        eventToExecuteOnStay?.Invoke();
        if (!executeOnStay) return;
        ExecuteAction();
    }

    protected void OnTriggerExit(Collider collider3d)
    {
        if (!tagsToCollide.Any(collider3d.CompareTag)) return;
        eventToExecuteOnExit?.Invoke();
        if (!executeOnExit) return;
        ExecuteAction();
    }

    #endregion

    #endregion
}