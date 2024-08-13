namespace slmit.sellinghub.bs
{
    using UnityEngine;
    using UnityEngine.UI;
    
    public class SLM_CarouselCard : MonoBehaviour
    {
        #region Variables

        #region Protected Variables
    
        [SerializeField] protected Image thumbnail;

        #endregion

        #region Public Variables
        
        public SLM_ProjectData Data {get; private set;}

        #endregion

        #endregion

        #region Methods

        #region Protected Methods
        
        protected void LoadImage()
        {
            thumbnail.sprite = Data.Carousel;
        }

        #endregion

        #region Public Methods
        
        public void OnCardChanged(SLM_ProjectData data)
        {
            Data = data;
            LoadImage();
        }

        #endregion

        #endregion
    }
}