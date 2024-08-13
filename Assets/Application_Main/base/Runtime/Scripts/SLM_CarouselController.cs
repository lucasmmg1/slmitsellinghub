namespace slmit.sellinghub.bs
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using intrl;
    
    public class SLM_CarouselController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Variables

        #region Protected Variables
        
        [SerializeField] protected List<SLM_ProjectData> projects;
        [SerializeField] protected SLM_Carousel carousel;
        [SerializeField] protected SLM_Project project;
        
        protected const int CARD_COUNT = 6;
        protected List<SLM_CarouselCard> ccs;
        protected SLM_CarouselCard previous, selected;
        protected Vector2 previousPosition, direction;

        #endregion

        #endregion

        #region Methods

        #region Protected Methods
        
        protected void Start()
        {
            SetupCarousel();
        }
        protected void SetupCarousel()
        {
            carousel.OnPreSetup();
            
            ccs = new List<SLM_CarouselCard>();
            for (var i = 0; i < CARD_COUNT; i++)
            {
                var card = AddCardAt(i);
                card.OnCardChanged(GetNextDataItem(i));
            }
            selected = ccs[2];
            previous = selected;
            
            carousel.OnPostSetup();
        }

        #endregion

        #region Public Methods

        public void OnBeginDrag(PointerEventData eventData)
        {
            direction = eventData.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            direction = eventData.position - previousPosition;
            previousPosition = eventData.position;
            
            var tmp = new List<SLM_CarouselCard>(ccs);
            foreach (var card in tmp)
            {
                switch (card.transform.position.x) 
                {
                    case var x when x + carousel.card_width / 2 < 0 && direction.x < 0:
                        card.transform.localPosition += Vector3.right * (carousel.card_width + carousel.spacing) * CARD_COUNT;
                        card.OnCardChanged(GetNextDataItem(projects.FindIndex(data => data == card.Data)));
                        ReplaceCardAt(card, 0, ccs.Count - 1);
                        selected = ccs[ccs.FindIndex(c => c == selected) + 1 % ccs.Count];
                        break;
                    case var x when x - carousel.card_width / 2 > Screen.width && direction.x > 1:
                        card.transform.localPosition -= Vector3.right * (carousel.card_width + carousel.spacing) * CARD_COUNT;
                        card.OnCardChanged(GetPreviousDataItem(projects.FindIndex(data => data == card.Data)));
                        ReplaceCardAt(card, ccs.Count - 1, 0);
                        selected = ccs[ccs.FindIndex(c => c == selected) - 1 % ccs.Count];
                        break;
                }
            }  
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (previous == selected)
            {
                selected = direction.x switch
                {
                    > 0 => ccs[ccs.FindIndex(c => c == selected) - 1 % ccs.Count],
                    < 0 => ccs[ccs.FindIndex(c => c == selected) + 1 % ccs.Count],
                    _ => selected
                };
            }
            selected.OnCardChanged(selected.Data);

            var current = carousel.RT.anchoredPosition.x;
            var target = current + (selected.transform.position.x - carousel.Center.transform.position.x) * -1;
            LeanTween.value(gameObject, value => carousel.RT.anchoredPosition = new Vector2(value, carousel.RT.anchoredPosition.y), current, target, 0.15f)
            .setOnComplete(() =>
            {
                LeanTween.alphaCanvas(project.CG, 0, 0.15f).setOnComplete(() =>
                {
                    project.OnProjectChanged(selected.Data);
                    LeanTween.alphaCanvas(project.CG, 1, 0.15f);
                });
            });
            
            previous = selected;
        }

        /// <summary>
        /// Add a card to the carousel at the specified index.
        /// </summary>
        /// <param name="index"> The carousel index to add the card at. 0-based. </param>
        public SLM_CarouselCard AddCardAt(int index = 0)
        {
            var card = Instantiate(carousel.CardPrefab, carousel.RT);
            card.name = $"Card {index}";
            ccs.Insert(index, card);
            return card;
        }
        
        /// <summary>
        /// Remove a card from the carousel at the specified index.
        /// </summary>
        /// <param name="index"> The carousel index to remove the card from. 0-based. </param>
        public void RemoveCardAt(int index = 0)
        {
            Destroy(ccs[index].gameObject);
            ccs.RemoveAt(index);
        }

        /// <summary>
        /// Replaces the given card index and position in the carousel list.
        /// </summary>
        /// <param name="card"> The card that will have it's index and position changed. </param>
        /// <param name="currentIndex"> The current card's index </param>
        /// <param name="newIndex"> The new card's index </param>
        public void ReplaceCardAt(SLM_CarouselCard card, int currentIndex, int newIndex)
        {
            var tmp = ccs;
            tmp.RemoveAt(currentIndex);
            tmp.Insert(newIndex, card);
            ccs = tmp;
        }

        /// <summary>
        /// Get the next data item index in the carousel.
        /// </summary>
        /// <param name="index"> The index for reference </param>
        /// <returns></returns>
        public SLM_ProjectData GetNextDataItem(int index) => projects[index + 1 >= projects.Count - 1 ? index + 1 - projects.Count * ((index + 1) / projects.Count) : index + 1];
        
        /// <summary>
        /// Get the previous data item index in the carousel.
        /// </summary>
        /// <param name="index"> The index for reference </param>
        /// <returns></returns>
        public SLM_ProjectData GetPreviousDataItem(int index) => projects[index - 1 < 0 ? projects.Count - 1 : index - 1];

        #endregion

        #endregion
    }
}