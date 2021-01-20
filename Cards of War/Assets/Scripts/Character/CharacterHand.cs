using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterHand : MonoBehaviour {
    [SerializeField] Vector2 centerOffset;
    Vector2 hiddenPos = new Vector2(0.0f, 1.0f);
    const float speed = 0.25f;


    private void Awake() {
        forceHide();
    }


    private void Update() {
        setSortingOrder();
        if(FindObjectOfType<CardMovement>().getOpponentHeldCardObject() != null) {
            move(FindObjectOfType<CardMovement>().getOpponentHeldCardObject().transform.position);
        }
        else
            hide();
    }


    void setSortingOrder() {
        var cm = FindObjectOfType<CardMovement>();
        if(cm.getOpponentHeldCardObject() != null) {
            GetComponent<SpriteRenderer>().sortingOrder = cm.getOpponentHeldCardObject().GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    void move(Vector2 target) {
        transform.DOComplete();
        transform.position = target + centerOffset;
    }

    void hide() {
        transform.DOMove(hiddenPos, speed);
    }

    void forceHide() {
        transform.DOComplete();
        transform.position = hiddenPos;
    }
}
