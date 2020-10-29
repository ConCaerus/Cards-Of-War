using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimations : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    string txt;

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


    void resetTextAnimation() {
        if(textAnimator != null)
            StopCoroutine(textAnimator);
        text.text = "<alpha=#00>" + txt;
        textAnimator = null;
    }


    IEnumerator animateText(int index = 0) {
        yield return new WaitForSeconds(0.025f);

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
        }

        if(index < arr.Length + 1)
            textAnimator = StartCoroutine(animateText(++index));

        else 
            textAnimator = null;
    }

    float getRand() {
        return Random.Range(0.0f, 1.0f);
    }
}
