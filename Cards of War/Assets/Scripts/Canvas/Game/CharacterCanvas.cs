using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CharacterCanvas : MonoBehaviour {
    [SerializeField] GameObject dialogAnchor;
    [SerializeField] TextMeshProUGUI dialog;

    string dialogText = "Hello World!";

    Coroutine showTimeInstance, delayBetweenCharsInstance;

    private void Awake() {
        DOTween.Init();
        dialog.text = "";

        stopDialog();
    }


    public void startDialog(string st) {
        dialogText = st;
        dialog.text = "";
        dialogAnchor.transform.DOMoveX(5.25f, 0.25f);

        if(showTimeInstance != null)
            StopCoroutine(showTimeInstance);
        if(delayBetweenCharsInstance != null)
            StopCoroutine(delayBetweenCharsInstance);

        delayBetweenCharsInstance = StartCoroutine(delayBetweenChars());
    }

    void stopDialog() {
        dialogAnchor.transform.DOMoveX(15.0f, 0.75f);

        if(showTimeInstance != null)
            StopCoroutine(showTimeInstance);
        if(delayBetweenCharsInstance != null)
            StopCoroutine(delayBetweenCharsInstance);
    }


    IEnumerator delayBetweenChars() {
        yield return new WaitForSeconds(0.05f);
        dialogAnchor.transform.DORotate(new Vector3(0.0f, 0.0f, -10.0f), 0.1f);

        //  Some characters still need to be shown
        if(dialogText.Length > dialog.text.Length) {
            dialog.text = dialog.text + " ";
            char[] d = dialog.text.ToCharArray();
            char[] dt = dialogText.ToCharArray();

            for(int i = 0; i < dialog.text.Length; i++) {
                d[i] = dt[i];
            }

            dialog.text = d.ArrayToString();

            StartCoroutine(delayBetweenChars());
        }

        //  Characters are all shown
        else if(dialogText.Length == dialog.text.Length) {
            if(showTimeInstance != null)
                StopCoroutine(showTimeInstance);

            showTimeInstance = StartCoroutine(showTime());
        }


        dialogAnchor.transform.DORotate(new Vector3(0.0f, 0.0f, 0.0f), 0.5f);
    }

    IEnumerator showTime() {
        yield return new WaitForSeconds(2.0f);

        stopDialog();
    }
}
