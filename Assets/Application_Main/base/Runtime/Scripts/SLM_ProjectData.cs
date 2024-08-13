namespace slmit.sellinghub.bs
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New SLMIT Project Data", menuName = "SLMIT/SLMIT Selling Hub/New SLMIT Project Data", order = 1)]
    public class SLM_ProjectData : ScriptableObject
    {
        #region Variables

        #region Protected Variables

        [SerializeField] private string title, description;
        [SerializeField] protected Sprite carousel, thumbnail;
        
        #endregion

        #region Public Variables
        
        public string Title => title;
        public string Description => description;
        
        public Sprite Carousel => carousel;
        public Sprite Thumbnail => thumbnail;

        #endregion

        #endregion
    }
}