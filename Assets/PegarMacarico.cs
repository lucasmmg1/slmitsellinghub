using System.Collections;
using UnityEngine;

public class PegarMacarico : QuestView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected OutlineObjectEffectView macarico;
    [SerializeField] protected GameObject macaricoHand;
    [SerializeField] protected CanvasGroup canvasGC;

    #endregion

    #endregion

    #region Methods

    protected void Start()
    {
        canvasGC.alpha = 0;
        macaricoHand.SetActive(false);
    }

    #region Public Methods
    
    public override IEnumerator Quest()
    {
        LeanTween.alphaCanvas(canvasGC, 1, 0.5f).setIgnoreTimeScale(true);
        macarico.StartFeedback();
        
        do
        {
            Status = QuestStatus.InProgress;
            yield return new WaitForEndOfFrame();
        } while (macarico.gameObject.activeSelf);
        
        Status = QuestStatus.Completed;
        LeanTween.alphaCanvas(canvasGC, 0, 0.5f).setIgnoreTimeScale(true);
        macaricoHand.SetActive(true);
    }

    #endregion

    #endregion
}