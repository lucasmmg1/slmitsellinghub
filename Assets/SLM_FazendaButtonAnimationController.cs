namespace slmit.sellinghub.features.fazenda
{
    using UnityEngine;

    public class SLM_FazendaButtonAnimationController : MonoBehaviour
    {
        #region Variables

        #region Protected Variables

        [SerializeField] protected GameObject idle;
        [SerializeField] protected Animator animator;
        [SerializeField] protected AnimationClip clipToPlay;

        #endregion

        #endregion

        #region Methods

        #region Protected Methods

        protected void OnEnable()
        {
            idle.SetActive(false);
            animator.Play(clipToPlay.name);
        }
        protected void OnDisable()
        {
            idle.SetActive(true);
        }

        #endregion

        #endregion
    }
}