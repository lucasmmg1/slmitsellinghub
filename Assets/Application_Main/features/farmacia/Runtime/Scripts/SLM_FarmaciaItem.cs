namespace slmit.sellinghub.features.farmacia
{
    using UnityEngine;
    
    public class SLM_FarmaciaItem : MonoBehaviour
    {
        #region Variables

        #region Protected Variables

        [SerializeField] protected GameObject anchor;
        [SerializeField] protected CanvasGroup infoCG;
        
        #endregion
        
        #endregion
        
        #region Methods

        #region Protected Methods

        protected void Start()
        {
            anchor.transform.localScale = Vector3.zero;
            infoCG.alpha = 0;
        }
        
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Method to be executed when the object is grabbed
        /// </summary>
        public void OnObjectWasGrabbed()
        {
            anchor.transform.localScale = new Vector3(1, 0, 1);
            
            LeanTween.scaleY(anchor, 1, 0.5f).setEaseOutBack();
            LeanTween.alphaCanvas(infoCG, 1, 0.5f).setDelay(0.35f);
        }
        
        /// <summary>
        /// Method to be executed when the object is released
        /// </summary>
        public void OnObjectWasReleased()
        {
            LeanTween.alphaCanvas(infoCG, 0, 0.5f);
            LeanTween.scaleY(anchor, 0, 0.5f).setEaseInBack().setDelay(0.15f).setOnComplete(() =>
            {
                anchor.transform.localScale = Vector3.zero;
                infoCG.alpha = 0;
            });
        }
        #endregion
        
        #endregion
    }
}
