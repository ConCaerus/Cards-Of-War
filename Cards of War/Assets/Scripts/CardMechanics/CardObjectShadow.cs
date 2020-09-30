using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CardObjectShadow : MonoBehaviour {
    GameObject shadow;

    bool shadowCard = false;
    bool shown;

    float duration = 0.05f;

    Vector3 offset = new Vector3(-0.25f, -0.25f);
    Vector3 setPos;


    private void Awake() {
        createShadow();
        hideShadow();
    }


    private void Update() {
        moveShadow();
    }


    void createShadow() {
        shadow = new GameObject("Card Shadow");

        var sr = shadow.AddComponent<SpriteRenderer>();
        sr.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        sr.color = Color.black;
        sr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;

        shadow.transform.position = transform.position;
        shadow.transform.SetParent(transform);
    }

    void moveShadow() {
        if(shadowCard) {
            shadow.SetActive(true);
            if(shown) {
                setPos = transform.position + offset;
                shadow.transform.DOMove(setPos, duration);
            }

            else 
                moveToSetPos();
        } 
        else if(!shadowCard) {
            shadow.SetActive(false);
            setPos = transform.position;
        }
    }


    void moveToSetPos() {
        gameObject.transform.DOMove(setPos, duration);
        shadow.transform.DOMove(transform.position, duration);
    }


    public void hideShadow() {
        shown = false;
    }

    public void showShadow() {
        shown = true;
    }

    public void startShowingShadow() {
        shadowCard = true;
    }

    public void stopShowingShadow() {
        shadowCard = false;
    }


    public Vector2 getOffset() {
        return offset;
    }
}