using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpponentCharacterCanvas : MonoBehaviour {

    [SerializeField] GameObject character;
    const float characterSpeed = 0.25f;
    Vector2 characterShowPos, characterHidePos;


    private void Awake() {
        characterShowPos = character.GetComponent<RectTransform>().anchoredPosition;
        characterHidePos = characterShowPos - new Vector2(100.0f, 0.0f);

        forceHideCharacter();
    }


    public void showCharacter() {
        character.GetComponent<RectTransform>().DOComplete();
        character.GetComponent<RectTransform>().DOMove(characterShowPos, characterSpeed);
    }

    public void hideCharacter() {
        character.GetComponent<RectTransform>().DOComplete();
        character.GetComponent<RectTransform>().DOMove(characterHidePos, characterSpeed);
    }

    public void forceHideCharacter() {
        character.GetComponent<RectTransform>().DOComplete();
        character.GetComponent<RectTransform>().anchoredPosition = characterHidePos;
    }
}

