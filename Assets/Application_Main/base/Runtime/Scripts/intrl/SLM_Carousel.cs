namespace slmit.sellinghub.bs.intrl
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    
    [Serializable]
    public class SLM_Carousel
    {
        #region Variables

        #region Protected Variables

        [SerializeField] protected RectTransform rt;
        [SerializeField] protected HorizontalLayoutGroup hlg;
        [SerializeField] protected CanvasGroup cg;
        [SerializeField] protected GameObject center, placeholder;
        [SerializeField] protected SLM_CarouselCard cardPrefab;
        
        #endregion

        #region Public Variables

        public RectTransform RT => rt;
        public GameObject Center => center;
        public SLM_CarouselCard CardPrefab => cardPrefab;
        public float card_width => cardPrefab.GetComponent<RectTransform>().rect.width;
        public float spacing => hlg.spacing;
        
        #endregion

        #endregion

        #region Methods

        #region Public Methods

        public void Setup() {}

        public void OnPreSetup()
        {
            cg.alpha = 0;
            placeholder.SetActive(true);
        }
        public void OnPostSetup()
        {
            placeholder.SetActive(false);
            cg.alpha = 1;
            rt.anchoredPosition += Vector2.right * ((card_width / 2 + (card_width + spacing) * 2) * -1);
        }

        #endregion

        #endregion
    }
}