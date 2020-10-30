using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimations : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    public string txt;

    Coroutine textAnimator = null;


    private void Start() {
        txt = text.text;
        resetTextAnimation();
    }


    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && textAnimator == null) {
            textAnimator = StartCoroutine(animateText());
        }
        else if(Input.GetKeyDown(KeyCode.Space) && textAnimator != null) {
            resetTextAnimation();
            textAnimator = StartCoroutine(animateText());
        }
    }


    public void setText(string newText) {
        txt = newText;
    }

    void resetTextAnimation() {
        if(textAnimator != null)
            StopCoroutine(textAnimator);
        text.text = "<alpha=#00>" + txt;
        textAnimator = null;
    }

    public bool spaceThing = true;
    IEnumerator animateText(int index = 0) {
        yield return new WaitForSeconds(0.5f);

        var arr = txt.ToCharArray();

        double vOffset = -0.1;
        text.text = "<alpha=#FF><size=100%><voffset=" + (-vOffset).ToString() + "em>";


        for(int i = 0; i < arr.Length; i++) {
            if(i == index)
                text.text += "<alpha=#00>";

            else if(i == index - 1) {
                text.text += "</voffset><voffset=" + vOffset.ToString() + "em><u>" + arr[i] + "</u></voffset>";
                continue;
            }

            text.text += arr[i];

            //  code here that skips spaces in text so only letters get animated.
        }

        if(index < arr.Length + 1)
            textAnimator = StartCoroutine(animateText(++index));

        else 
            textAnimator = null;
    }
}
