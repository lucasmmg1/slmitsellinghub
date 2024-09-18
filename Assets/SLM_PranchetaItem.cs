namespace slmit.sellinghub.features.seguranca
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    
    [RequireComponent(typeof(EventTrigger))]
    public class SLM_PranchetaItem : MonoBehaviour
    {
        #region Variables

        #region Protected Variables

        [SerializeField] protected UnityEvent<BaseEventData> onItemWasClickedEvent;

        #endregion

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Executes when the object is clicked.
        /// </summary>
        public void OnItemWasClickedEvent(BaseEventData eventData) => onItemWasClickedEvent?.Invoke(eventData);

        #endregion

        #endregion
    }
}