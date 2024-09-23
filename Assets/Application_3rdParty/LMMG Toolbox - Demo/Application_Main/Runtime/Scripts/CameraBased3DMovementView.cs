using UnityEngine;

[AddComponentMenu("Scripts/Hiscom Engine/Patterns/MMVCC/Views/Camera Based 3D Movement View")]
public class CameraBased3DMovementView : Physical3DMovementsView
{
    #region Methods

    #region Protected Methods

    protected override void Move(bool canMove)
    {
        eventToExecuteBeforeMovement?.Invoke();
        var selectedMovementAnimationClip = defaultAnimationClip;
            
        if (canMove)
        {
            canMoveEvent?.Invoke();

            var rigidbodyVelocity = rigidbody3d.velocity;
            var rigidbodyTransform = rigidbody3d.transform;
            var localVelocity = rigidbodyTransform.forward * movementVector.z + rigidbodyTransform.right * movementVector.x;
            var velocity = new Vector3(localVelocity.x != 0 ? localVelocity.x : rigidbodyVelocity.x, localVelocity.y != 0 ? localVelocity.y : rigidbodyVelocity.y, localVelocity.z != 0 ? localVelocity.z : rigidbodyVelocity.z);
            var input = movementVector / movementForce;
            
            rigidbody3d.velocity = velocity;
            playerView.SetLastMovementDirection = input != Vector3.zero ? input : playerView.GetLastMovementDirection;
            selectedMovementAnimationClip = movementVector != Vector3.zero ? movementAnimationClip : selectedMovementAnimationClip;
        }
        else
        {
            cannotMoveEvent?.Invoke();
        }
        eventToExecuteAfterMovement?.Invoke();
    }

    #endregion

    #endregion
}