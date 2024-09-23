using UnityEngine;

[AddComponentMenu("Scripts/Engine/Core/MMVCC/Views/Is Key Pressed Condition View")]
public class IsKeyPressedConditionView : ConditionsView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected int numberOfRequiredPresses;
    [SerializeField] protected float timeToCheckRequiredPresses;
    [SerializeField] protected bool useTime;
    [SerializeField] protected IntegerCompareType compareType;
    [SerializeField] protected KeyCode keyToCheckIfPressed;

    protected int numberOfTimesPressed;
    protected float initialConditionTime, currentTime;
    protected bool checkMaxTimeBetweenPresses, keyWasPressedOnLastFrame;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected void Update()
    {
        if (Time.time > currentTime + 0.1f)
            keyWasPressedOnLastFrame = false;

        if (!Input.GetKeyDown(keyToCheckIfPressed)) return;
        currentTime = Time.time;
        keyWasPressedOnLastFrame = true;
    }

    protected virtual bool CompareIntegers()
    {
        return compareType switch
        {
            IntegerCompareType.Equals => numberOfTimesPressed == numberOfRequiredPresses,
            IntegerCompareType.GreaterThan => numberOfTimesPressed > numberOfRequiredPresses,
            IntegerCompareType.GreaterOrEqualThan => numberOfTimesPressed >= numberOfRequiredPresses,
            IntegerCompareType.LessThan => numberOfTimesPressed < numberOfRequiredPresses,
            IntegerCompareType.LessOrEqualThan => numberOfTimesPressed <= numberOfRequiredPresses,
            IntegerCompareType.NotEquals => numberOfTimesPressed != numberOfRequiredPresses,
            _ => false
        };
    }

    protected override bool Condition()
    {
        switch (useTime)
        {
            case true:
                if (keyWasPressedOnLastFrame)
                {
                    if (numberOfTimesPressed == 0)
                        initialConditionTime = Time.time;

                    numberOfTimesPressed++;
                }

                if (Time.time >= initialConditionTime + timeToCheckRequiredPresses)
                {
                    var returnValueIfTimeEnded = CompareIntegers();
                    numberOfTimesPressed = returnValueIfTimeEnded ? 0 : numberOfTimesPressed;
                    return returnValueIfTimeEnded;
                }

                var returnValueCheckingCondition = CompareIntegers();
                numberOfTimesPressed = returnValueCheckingCondition ? 0 : numberOfTimesPressed;
                return returnValueCheckingCondition;

            case false:
                if (keyWasPressedOnLastFrame)
                    numberOfTimesPressed++;

                var returnValueOnUnlimitedTime = CompareIntegers();
                numberOfTimesPressed = returnValueOnUnlimitedTime ? 0 : numberOfTimesPressed;
                return returnValueOnUnlimitedTime;
        }
    }

    #endregion

    #endregion
}