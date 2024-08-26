using System;
using UnityEngine;

public class Blink : MonoBehaviour
{
    #region Variables

    #region Protected Variables
    
    protected CanvasGroup cg;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    protected void OnEnable()
    {
        cg.alpha = 0;
        LeanTween.alphaCanvas(cg, 1, 0.5f).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(cg, 0, 0.5f).setDelay(0.5f);
        }).setLoopPingPong();
    }
    protected void OnDisable()
    {
        LeanTween.cancelAll();
        cg.alpha = 0;
    }

    #endregion

    #endregion
}
