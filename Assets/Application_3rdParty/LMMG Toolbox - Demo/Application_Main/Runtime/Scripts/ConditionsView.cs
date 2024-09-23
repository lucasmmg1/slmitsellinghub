using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ConditionsView : MonoBehaviour
{
    #region Methods

    #region Protected Methods

    /// <summary>
    /// The condition method. Inside, it must return a boolean value.
    /// </summary>
    /// <returns></returns>
    protected abstract bool Condition();

    #endregion

    #region Public Methods

    /// <summary>
    /// Check if the given conditions return true or false. 
    /// </summary>
    /// <param name="conditions"> The conditions to check for. </param>
    /// <returns></returns>
    public static bool CheckConditions(IEnumerable<ConditionsView> conditions)
    {
        return conditions.All(condition => condition.Condition());
    }

    #endregion

    #endregion
}