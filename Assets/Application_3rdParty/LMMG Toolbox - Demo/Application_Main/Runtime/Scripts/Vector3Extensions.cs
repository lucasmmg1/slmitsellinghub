using UnityEngine;

public static class Vector3Extensions
{
    #region Methods

    #region Public Methods

    /// <summary>
    /// Returns the absolute value of a vector.
    /// </summary>
    /// <param name="source"> Current vector </param>
    /// <returns></returns>
    public static Vector3 Abs(this Vector3 source)
    {
        return new Vector3(Mathf.Abs(source.x), Mathf.Abs(source.y), Mathf.Abs(source.z));
    }

    /// <summary>
    /// Divides a vector by another vector.
    /// </summary>
    /// <param name="source"> Current vector </param>
    /// <param name="target"> The division vector </param>
    /// <returns></returns>
    public static Vector3 Divide(this Vector3 source, Vector3 target)
    {
        return new Vector3(source.x / target.x, source.y / target.y, source.z / target.z);
    }

    public static Vector3 Multiply(this Vector3 source, Vector3 target)
    {
        return new Vector3(source.x * target.x, source.y * target.y, source.z * target.z);
    }

    #endregion

    #endregion
}