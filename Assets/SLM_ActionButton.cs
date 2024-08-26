namespace slmit.sellinghub.bs
{
    using UnityEngine;
    using UnityEngine.Events;
    using MixedReality.Toolkit.UX;
    
    public class SLM_ActionButton : MonoBehaviour
    {
        #region Variables

        #region Protected Variables
    
        [SerializeField] protected float animationDuration;
        [SerializeField] protected Animator animator;
        [SerializeField] protected GameObject idle, hover, click;
        [SerializeField] protected AnimationClip idleAnimationClip, hoverAnimationClip, clickAnimationClip;
        [SerializeField] private PressableButton button;
        [SerializeField] protected UnityEvent eventToExecute;
    
        #endregion

        #endregion
    
        #region Methods

        #region Public Methods

        /// <summary>
        /// Executes when the gaze hover starts over object.
        /// </summary>
        public void OnHoverEnter()
        {
            if (!button.enabled) return;
            
            hover.SetActive(true);
            idle.SetActive(false);
            animator.Play(hoverAnimationClip.name);
        }

        /// <summary>
        /// Executes when the gaze hover ends over object.
        /// </summary>
        public void OnHoverExit()
        {
            if (!button.enabled) return;
            
            idle.SetActive(true);
            hover.SetActive(false);
            animator.Play(idleAnimationClip.name);
        }

        /// <summary>
        /// Executes when the object is clicked.
        /// </summary>
        public void OnClick()
        {
            if (!button.enabled) return;
            
            button.enabled = false;
            //click.SetActive(true);
            //hover.SetActive(false);
            //animator.Play(clickAnimationClip.name);
            //eventToExecute?.Invoke();
            Invoke(nameof(Reset), animationDuration);
        }

        #endregion

        #region Protected Methods

        protected void Reset()
        {
            //idle.SetActive(true);
            //click.SetActive(false);
            //animator.Play(idleAnimationClip.name);
            button.enabled = true;
        }

        #endregion
    
        #endregion
    }
}