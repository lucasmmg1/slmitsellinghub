namespace slmit.sellinghub
{
    using UnityEngine;
    
    [RequireComponent(typeof(Animator))]
    public class AnimatorClipChanger : MonoBehaviour
    {
        #region Variables

        #region Protected Variables

        [SerializeField] protected AnimationClip clip;
        protected Animator animator;

        #endregion

        #endregion

        #region Methods

        #region Protected Methods

        protected void Awake()
        {
            animator = GetComponent<Animator>();
        }
        protected void Start()
        {
            ChangeAnimationClip();
        }

        protected void ChangeAnimationClip()
        {
            animator.Play(clip.name);
        }

        #endregion

        #endregion
    }
}
