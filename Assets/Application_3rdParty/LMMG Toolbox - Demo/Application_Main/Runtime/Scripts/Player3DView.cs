using UnityEngine;

public class Player3DView : PlayerView
{
    #region Variables

    #region Protected Variables

    [SerializeField] private Rigidbody rigidbody3d;

    #endregion

    #region Public Variables

    public Rigidbody GetRigidbody3d => rigidbody3d;

    #endregion

    #endregion
}