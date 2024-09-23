using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerView : MonoBehaviour
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected bool overrideDirection, directionalX, directionalY, directionalZ, flipX, flipY, flipZ;

    protected List<MovementsView> playerMovements = new();
    protected Vector3 direction, lastMovementDirection;

    #endregion

    #region Public Variables

    public bool GetOverrideDirection => overrideDirection;
    public bool GetDirectionalX => directionalX;
    public bool GetDirectionalY => directionalY;
    public bool GetDirectionalZ => directionalZ;
    public bool GetFlipX => flipX;
    public bool GetFlipY => flipY;
    public bool GetFlipZ => flipZ;

    public List<MovementsView> GetPlayerMovements => playerMovements;

    public List<MovementsView> SetPlayerMovements
    {
        set => playerMovements = value;
    }

    public Vector3 GetDirection => direction;

    public Vector3 SetDirection
    {
        set => direction = value;
    }

    public Vector3 GetLastMovementDirection => lastMovementDirection;

    public Vector3 SetLastMovementDirection
    {
        set => lastMovementDirection = value;
    }

    #endregion

    #endregion
}