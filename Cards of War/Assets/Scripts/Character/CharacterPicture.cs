using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterPicture : MonoBehaviour {
    [SerializeField] GameObject characterPicture;
    Vector2 showPos, hidePos;
    float speed = 0.25f;

    private void Awake() {
        showPos = characterPicture.transform.position;
        hidePos = showPos;
        hidePos.x = -12.0f;

        forceHideCharacterPicture();
    }


    public void showCharacterPicture() {
        characterPicture.transform.DOComplete();
        characterPicture.transform.DOMove(showPos, speed);
    }

    public void hideCharacterPicture() {
        characterPicture.transform.DOComplete();
        characterPicture.transform.DOMove(hidePos, speed);
    }

    public void forceHideCharacterPicture() {
        characterPicture.transform.DOComplete();
        characterPicture.transform.position = hidePos;
    }
}
