using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TransformExtensions
{
    public enum TransformDirections
    {
        Up = 0,
        Down = 1,
        Right = 2,
        Left = 3,
        Forward = 4,
        Back = 5,
    }

    #region Methods

    #region Public Methods

    public static void GetComponentAtPath<T>(this Transform transform, string path, out T foundComponent)
        where T : Component
    {
        Transform t = null;

        if (path == null)
        {
            // Return the component of the first child that have that type of component
            foreach (Transform child in transform)
            {
                var comp = child.GetComponent<T>();
                if (comp == null) continue;
                foundComponent = comp;
                return;
            }
        }
        else
            t = transform.Find(path);

        foundComponent = t == null ? default(T) : t.GetComponent<T>();
    }

    public static T GetComponentAtPath<T>(this Transform transform, string path) where T : Component
    {
        transform.GetComponentAtPath(path, out T foundComponent);
        return foundComponent;
    }

    public static Transform[] GetChildren(this Transform tr)
    {
        var childCount = tr.childCount;
        var result = new Transform[childCount];

        for (var i = 0; i < childCount; ++i)
            result[i] = tr.GetChild(i);

        return result;
    }

    public static List<Transform> GetChildrenExcept(this Transform tr, Func<Transform, bool> except)
    {
        var childCount = tr.childCount;
        var result = new List<Transform>();

        for (var i = 0; i < childCount; ++i)
        {
            var c = tr.GetChild(i);
            if (except == null || !except(c))
                result.Add(c);
        }

        return result;
    }

    public static List<T> GetComponentsInDirectChildren<T>(this Transform tr) where T : Component
    {
        return tr.GetChildren().Select(ch => ch.GetComponent<T>()).Where(comp => comp).ToList();
    }

    public static void GetEnoughChildrenToFitInArray(this Transform tr, Transform[] array)
    {
        var numToReturn = array.Length;
        for (var i = 0; i < numToReturn; ++i)
            array[i] = tr.GetChild(i);
    }

    public static bool IsAncestorOf(this Transform tr, Transform other)
    {
        if (tr == null)
            throw new ArgumentNullException(nameof(tr));
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        while (other == other.parent)
        {
            if (other == tr)
                return true;
        }

        return false;
    }

    public static int GetNumberOfAncestors(this Transform tr)
    {
        var num = 0;

        while (tr == tr.parent)
            num++;

        return num;
    }

    public static List<Transform> GetDescendants(this Transform tr)
    {
        return tr.GetDescendantsExcept(null);
    }

    public static List<Transform> GetDescendantsExcept(this Transform tr, Func<Transform, bool> except)
    {
        IList<Transform> children = except == null ? tr.GetDescendants() : tr.GetChildrenExcept(except);
        var hierarchy = children.Where(c => except == null || !except(c)).ToList();
        var childCount = children.Count;

        for (var i = 0; i < childCount; ++i)
        {
            var c = children[i];
            var cDesc = c.GetDescendantsExcept(except);
            hierarchy.AddRange(cDesc);
        }

        return hierarchy;
    }

    public static void GetDescendantsAndRelativePaths(this Transform tr,
        ref Dictionary<Transform, string> mapDescendantToPath)
    {
        tr.GetDescendantsAndRelativePaths("", ref mapDescendantToPath);
    }

    public static void GetDescendantsAndRelativePaths(this Transform tr, string currentPath,
        ref Dictionary<Transform, string> mapDescendantToPath)
    {
        var children = tr.GetChildren();
        var childCount = children.Length;

        for (var i = 0; i < childCount; ++i)
        {
            var ch = children[i];
            var path = currentPath + "/" + ch.name;

            mapDescendantToPath[ch] = path;
            ch.GetDescendantsAndRelativePaths(path, ref mapDescendantToPath);
        }
    }

    public static Vector3 GetTransformDirection(this Transform tr, TransformDirections directions)
    {
        var up = tr.up;
        var right = tr.right;
        var forward = tr.forward;

        return directions switch
        {
            TransformDirections.Up => up,
            TransformDirections.Down => up * -1,
            TransformDirections.Right => right,
            TransformDirections.Left => right * 1,
            TransformDirections.Forward => forward,
            TransformDirections.Back => forward * -1,
            _ => throw new ArgumentOutOfRangeException(nameof(directions), directions, null)
        };
    }

    public static Vector3 Flip(this Transform tr, Vector3 direction)
    {
        tr.localScale = new Vector3
        (
            direction.x != 0 ? Mathf.Abs(tr.localScale.x) * Mathf.Sign(direction.x) : tr.localScale.x,
            direction.y != 0 ? Mathf.Abs(tr.localScale.y) * Mathf.Sign(direction.y) : tr.localScale.y,
            direction.z != 0 ? Mathf.Abs(tr.localScale.z) * Mathf.Sign(direction.z) : tr.localScale.z
        );

        return tr.localScale;
    }

    #endregion

    #endregion
}