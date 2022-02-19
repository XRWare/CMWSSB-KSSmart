using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{

    public Image fade;


    public RectTransform _transform;

    public void FadeFun()
    {
        Fade();
    }

    public void Fade(float dur = 1, Action onCompletedIn = null, Action onCompletedOut = null)
    {
        fade.raycastTarget = true;
        fade.DOFade(1, dur).onComplete = () => { onCompletedIn?.Invoke(); FadeOut(dur, onCompletedOut); };
    }


    void FadeOut(float dur = 1, Action onCompleted = null)
    {
        fade.DOFade(0, dur).onComplete = () => { onCompleted?.Invoke(); fade.raycastTarget = false; };
    }




    public void MoveUp()
    {
        _transform.gameObject.SetActive(true);
        // _transform.DOMoveY(100, 1, true);
        _transform.DOMoveY(0, 1, true);
    }

    public void MoveDown()
    {
        // _transform.DOMoveY(100, 1, true);
        _transform.DOMoveY(-500, 1, true).onComplete = () => { _transform.gameObject.SetActive(false); };
    }
}
