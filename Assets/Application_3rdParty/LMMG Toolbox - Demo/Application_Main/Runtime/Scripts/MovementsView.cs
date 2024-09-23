using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class MovementsView : CallbackView
{
    // Todo:
    // - Fazer error de nulo no playerView.

    #region Variables

    #region Protected Variables

    [SerializeField] protected PlayerView playerView;
    [SerializeField] protected bool hasMovementConditions, hasMovementAnimation, hasMovementSounds;
    [SerializeField] protected ConditionsView[] movementConditions;

    [SerializeField]
    protected UnityEvent eventToExecuteBeforeMovement, eventToExecuteAfterMovement, canMoveEvent, cannotMoveEvent;

    [SerializeField] protected Animator movementAnimator;
    [SerializeField] protected AnimationClip defaultAnimationClip, movementAnimationClip;
    [SerializeField] protected AudioSource movementSound;

    #endregion

    #region Public Variables

    public AnimationClip DefaultAnimationClip
    {
        get => defaultAnimationClip;
        set => defaultAnimationClip = value;
    }

    public AnimationClip MovementAnimationClip
    {
        get => movementAnimationClip;
        set => movementAnimationClip = value;
    }

    public AudioSource MovementSound
    {
        get => movementSound;
        set => movementSound = value;
    }

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    /// <summary>
    /// Movement method that needs to be overridden with the desired movement.
    /// </summary>
    /// <param name="canMove"> If the rigidbody is allowed to move. </param>
    /// <exception cref="NotImplementedException"></exception>
    protected abstract void Move(bool canMove);

    #endregion

    #region Public Methods

    /// <summary>
    /// Start method to start the movement.
    /// </summary>
    public void Enable()
    {
        enabled = true;
    }

    /// <summary>
    /// Stop method to stop the movement.
    /// </summary>
    public void Disable()
    {
        enabled = false;
    }

    #endregion

    #endregion
}