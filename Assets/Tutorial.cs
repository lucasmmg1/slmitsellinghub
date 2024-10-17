using System.Collections;
using HiscomEngine.Runtime.Scripts.Patterns.MMVCC.Controllers;
using HPlayer;
using UnityEngine;

public class Tutorial : QuestView
{
    #region Variables

    #region Protected Variables

    [SerializeField] protected CanvasGroup overlayPanel;
    [SerializeField] protected RectTransform tutorialPanel;
    [SerializeField] protected CursorController cursorController;
    [SerializeField] protected PlayerController playerController;
    protected bool isTutorialCompleted;

    #endregion

    #endregion
    
    #region Methods

    #region Protected Methods

    protected void Start()
    {
        playerController.enabled = false;
        
        overlayPanel.alpha = 0;
        LeanTween.alphaCanvas(overlayPanel, 1, 0.5f).setIgnoreTimeScale(true).setDelay(.5f);
        
        tutorialPanel.localScale = Vector3.zero;
        LeanTween.scale(tutorialPanel, Vector3.one, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeOutBack).setDelay(1f).setOnComplete(() =>
        {
            cursorController.ChangeCursorVisibility(true);
        });
    }

    #endregion
    
    #region Public Methods

    public override IEnumerator Quest()
    {
        do
        {
            Status = QuestStatus.InProgress;
            yield return new WaitForEndOfFrame();
        } while (!isTutorialCompleted);
        
        Status = QuestStatus.Completed;
    }

    public void OnComecarButtonPressed()
    {
        LeanTween.alphaCanvas(overlayPanel, 0, 0.5f).setIgnoreTimeScale(true).setOnComplete(() =>
        {
            overlayPanel.gameObject.SetActive(false);
        });
        LeanTween.scale(tutorialPanel, Vector3.zero, 0.5f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            tutorialPanel.gameObject.SetActive(false);
            playerController.enabled = true;
            cursorController.ChangeCursorVisibility();
            isTutorialCompleted = true;
        });
    }

    #endregion

    #endregion
}
