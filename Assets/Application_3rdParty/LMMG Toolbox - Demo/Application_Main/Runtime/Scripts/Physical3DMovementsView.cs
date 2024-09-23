using UnityEngine;

public abstract class Physical3DMovementsView : PhysicalMovementsView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected bool useIndividualForces;
    [SerializeField] protected float movementForce;
    [SerializeField] protected Vector3 movementForceVector;

    protected Rigidbody rigidbody3d;
    protected Vector3 movementVector;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected override void Start()
    {
        base.Start();
        rigidbody3d = ((Player3DView)playerView).GetRigidbody3d;
    }

    protected virtual void Update()
    {
        var input = CallbackContext.ReadValue<Vector3>().normalized;
        movementVector = useIndividualForces ? input.Multiply(movementForceVector) : input * movementForce;
    }

    #endregion

    #endregion
}