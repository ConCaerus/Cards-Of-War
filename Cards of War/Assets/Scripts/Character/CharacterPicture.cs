using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterPicture : MonoBehaviour {
    [SerializeField] GameObject characterPicture;
    Vector2 showPos, hidePos;
    float speed = 0.25f;
    float bobbleTime = 0.75f;
    float bobbleHeight = 0.15f;

    Coroutine bobbleAnim = null;

    private void Awake() {
        showPos = characterPicture.transform.position;
        hidePos = showPos;
        hidePos.x = -12.0f;

        forceHideCharacterPicture();
    }


    IEnumerator waitToBobble() {
        yield return new WaitForSeconds(speed + 0.1f);

        bobbleAnim = StartCoroutine(bobble());
    }


    IEnumerator bobble(bool tall = true) {
        yield return new WaitForSeconds(bobbleTime);

        Vector2 target = characterPicture.transform.position;
        if(tall)
            target = characterPicture.transform.position - new Vector3(0.0f, bobbleHeight, 0.0f);
        else 
            target = characterPicture.transform.position + new Vector3(0.0f, bobbleHeight, 0.0f);

        characterPicture.transform.DOMove(target, bobbleTime);

        bobbleAnim = StartCoroutine(bobble(!tall));
    }


    public void showCharacterPicture() {
        characterPicture.transform.DOComplete();
        characterPicture.transform.DOMove(showPos, speed);
        StartCoroutine(waitToBobble());
    }

    public void hideCharacterPicture() {
        if(bobbleAnim != null)
            StopCoroutine(bobbleAnim);
        characterPicture.transform.DOComplete();
        characterPicture.transform.DOMove(hidePos, speed);
    }

    public void forceHideCharacterPicture() {
        if(bobbleAnim != null)
            StopCoroutine(bobbleAnim);
        characterPicture.transform.DOComplete();
        characterPicture.transform.position = hidePos;
    }
}
