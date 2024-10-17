using System.Collections;
using HPlayer;
using UnityEngine;

public class RealizarCorte : QuestView
{
    [SerializeField] protected CanvasGroup canvasGC;
    [SerializeField] protected PlayerController playerController;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animationClip;
    [SerializeField] private GameObject macarico;

    protected Vector3 initialPosition, initialRotation;
    protected bool hasCut;
    
    protected void Start()
    {
        canvasGC.alpha = 0;
    }
    
    public override IEnumerator Quest()
    {
        LeanTween.alphaCanvas(canvasGC, 1, 0.5f).setIgnoreTimeScale(true);
        
        do
        {
            Status = QuestStatus.InProgress;
            yield return new WaitForEndOfFrame();
        } while (!hasCut);
        
        Status = QuestStatus.Completed;
    }

    public void Cut()
    {
        StartCoroutine(CutCoroutine());
    }

    protected IEnumerator CutCoroutine()
    {
        initialPosition = macarico.transform.position;
        initialRotation = macarico.transform.eulerAngles;
        
        LeanTween.alphaCanvas(canvasGC, 0, 0.5f).setIgnoreTimeScale(true);
        
        LeanTween.rotate(macarico, new Vector3(20.919f, -74.557f, 184.083f), 0.5f).setIgnoreTimeScale(true);
        LeanTween.move(macarico, new Vector3(-1.751549f, 0.995f, -3.444333f), 0.5f).setIgnoreTimeScale(true).setOnComplete
        (
            () =>
            {
                macarico.SetActive(false);
                macarico.transform.position = new Vector3(-1.2f, 0.649f, -2.886f);
                macarico.transform.eulerAngles = new Vector3(19.533f, -75.912f, 132.31f);
                playerController.enabled = false;
                animator.Play(animationClip.name);

            }
        );
        yield return new WaitForSeconds(animationClip.length);
        LeanTween.rotate(macarico, initialRotation, 0.5f).setIgnoreTimeScale(true);
        LeanTween.move(macarico, initialPosition, 0.5f).setIgnoreTimeScale(true).setOnComplete
        (
            () =>
            {
                macarico.SetActive(true);
                hasCut = true;
                playerController.enabled = true;
            }
        );
    }
}
