namespace HiscomEngine.Runtime.Scripts.Patterns.MMVCC.Views.Internal
{
    using System;
    using UnityEngine;

    [Serializable]
    public class ObjectView
    {
        #region Variables

        #region Protected Variables

        [SerializeField] protected string id;
        [SerializeField] protected bool isStackable;

        #endregion

        #region Public Variables

        public string Id => id;
        public bool IsStackable => isStackable;

        #endregion

        #endregion
    }
}