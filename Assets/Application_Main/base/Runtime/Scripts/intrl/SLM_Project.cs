namespace slmit.sellinghub.bs.intrl
{
    using UnityEngine;
    using UnityEngine.UI;

    public class SLM_Project : MonoBehaviour
    {
        #region Variables

        #region Protected Variables

        [SerializeField] protected CanvasGroup cg;
        [SerializeField] protected Image thumbnail;

        #endregion

        #region Public Variables
        
        public SLM_ProjectData Data {get; private set;}
        public CanvasGroup CG => cg;
        
        #endregion

        #endregion

        #region Methods
        
        #region Protected Methods
        
        protected void LoadImage()
        {
            thumbnail.sprite = Data.Thumbnail;
        }

        #endregion

        #region Public Methods
        
        public void OnProjectChanged(SLM_ProjectData data)
        {
            Data = data;
            LoadImage();
        }

        #endregion

        #endregion
    }
}
