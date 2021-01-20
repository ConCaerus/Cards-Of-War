using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransitionCanvas : MonoBehaviour {
    [SerializeField] GameObject transitionMask;
    [SerializeField] bool animate = true, showOnStart = true;

    Coroutine animation = null;

    float speed = 1.0f;
    Vector2 showPos, hidePos;


    private void Awake() {
        var xMid = Camera.main.pixelWidth / 2;
        var yMid = Camera.main.pixelHeight / 2;

        showPos = new Vector2(xMid, yMid);
        hidePos = showPos + new Vector2(-2000.0f, 0.0f);

        StartCoroutine(setUpWaiter());
    }


    public void resetAnimation() {
        forceShowMask();
        hideMask();
    }

    void showMask() {
        if(animate) {
            transitionMask.GetComponent<Image>().DOComplete();
            transitionMask.GetComponent<Image>().DOFade(1.0f, speed);
        }
    }

    void forceShowMask() {
        if(animate) {
            Color c = transitionMask.GetComponent<Image>().color;
            transitionMask.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1.0f);
        }
    }

    void hideMask() {
        if(animate) {
            transitionMask.GetComponent<Image>().DOComplete();
            transitionMask.GetComponent<Image>().DOFade(0.0f, speed);
        }
    }

    void forceHideMask() {
        if(animate) {
            Color c = transitionMask.GetComponent<Image>().color;
            transitionMask.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.0f);
        }
    }

    public void show() {
        forceHideMask();
        transitionMask.GetComponent<Image>().DOComplete();
        transitionMask.GetComponent<Image>().DOFade(1.0f, speed);
        showMask();
    }

    public void hide() {
        forceShowMask();
        hide();
    }

    IEnumerator setUpWaiter() {
        if(showOnStart)
            transitionMask.SetActive(true);

        yield return new WaitForEndOfFrame();

        if(showOnStart) {
            forceShowMask();
            hideMask();
        }
        else {
            forceHideMask();
            transitionMask.SetActive(true);
        }
    }


    public bool isShowing() {
        return transitionMask.GetComponent<Image>().color.a == 1;
    }
}
