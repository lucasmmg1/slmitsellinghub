public abstract class PhysicalMovementsView : MovementsView
{
    #region Methods

    #region Protected Methods

    protected virtual void Start()
    {
        playerView.GetPlayerMovements.Add(this);
    }

    protected virtual void FixedUpdate()
    {
        Move(!hasMovementConditions || ConditionsView.CheckConditions(movementConditions));
    }

    #endregion

    #endregion
}