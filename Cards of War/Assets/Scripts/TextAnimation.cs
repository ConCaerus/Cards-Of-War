using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour {
    TextMeshProUGUI text;
    string txt;

    Coroutine textAnimator = null;


    private void Start() {
        init();
    }

    public void init() {
        if(text == null) {
            text = GetComponent<TextMeshProUGUI>();
            txt = text.text;
            resetTextAnimation();
        }
    }


    public void startAnimation() {
        resetTextAnimation();
        textAnimator = StartCoroutine(animateText());
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

    IEnumerator animateText(int index = 0) {
        yield return new WaitForSeconds(0.025f);

        var arr = txt.ToCharArray();
        while(index < arr.Length && arr[index] == ' ')
            index++;

        double vOffset = -0.1;
        text.text = "<alpha=#FF><size=100%><voffset=" + (-vOffset).ToString() + "em>";


        for(int i = 0; i < arr.Length; i++) {

            if(i == index + 1)
                text.text += "<alpha=#00>";

            else if(i == index) {
                text.text += "</voffset><voffset=" + vOffset.ToString() + "em><u>" + arr[i] + "</u></voffset>";
                continue;
            }

            text.text += arr[i];
        }

        if(index < arr.Length + 1) {
            textAnimator = StartCoroutine(animateText(++index));
        }

        else 
            textAnimator = null;
    }
}
