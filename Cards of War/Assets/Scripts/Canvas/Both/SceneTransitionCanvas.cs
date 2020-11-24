using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransitionCanvas : MonoBehaviour {
    [SerializeField] GameObject transitionMask;
    [SerializeField] bool animate = true, showOnStart = true;
    bool shown = false;

    Coroutine animation = null;

    float speed = 0.25f;
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
            animation = StartCoroutine(incMaskAlpha());
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
            animation = StartCoroutine(decMaskAlpha());
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


    float changeAmount = 1.0f / 100.0f;

    IEnumerator decMaskAlpha() {
        Color c = transitionMask.GetComponent<Image>().color;

        yield return new WaitForEndOfFrame();

        if(c.a > 0.0f + changeAmount) {
            transitionMask.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a - changeAmount);
            animation = StartCoroutine(decMaskAlpha());
        }

        else {
            transitionMask.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.0f);
            shown = false;
            animation = null;
        }
    }

    IEnumerator incMaskAlpha() {
        Color c = transitionMask.GetComponent<Image>().color;

        yield return new WaitForEndOfFrame();

        if(c.a < 1.0f - changeAmount) {
            transitionMask.GetComponent<Image>().color = new Color(c.r, c.g, c.b, c.a + changeAmount);
            animation = StartCoroutine(incMaskAlpha());
        }

        else {
            transitionMask.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1.0f);
            shown = true;
            animation = null;
        }
    }


    public bool getShowing() {
        return shown;
    }
}
