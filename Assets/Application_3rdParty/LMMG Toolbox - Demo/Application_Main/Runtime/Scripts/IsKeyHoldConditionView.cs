using UnityEngine;

[AddComponentMenu("Scripts/Engine/Core/MMVCC/Views/Is Key Hold Condition View")]
public class IsKeyHoldConditionView : ConditionsView
{
    // Todo:
    // Refactor igual ao isKeyPressedConditionView

    #region Variables

    #region Protected Variables

    [SerializeField] protected float timeToCheckHold;
    [SerializeField] protected KeyCode keyToCheckIfHold;

    protected float currentTime, initialTime;
    protected bool checkMaxTimeBetweenPresses, keyIsHold;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void Start()
    {
        checkMaxTimeBetweenPresses = !(timeToCheckHold <= 0);
    }

    protected void Update()
    {
        if (Input.GetKey(keyToCheckIfHold))
        {
            keyIsHold = true;
            return;
        }

        keyIsHold = false;
        initialTime = Time.time;
    }

    protected override bool Condition()
    {
        initialTime = Time.time;

        if (!keyIsHold) return false;

        if (!checkMaxTimeBetweenPresses) return true;

        if (!(Time.time >= initialTime + timeToCheckHold)) return false;
        keyIsHold = false;
        return true;
    }

    #endregion

    #endregion
}