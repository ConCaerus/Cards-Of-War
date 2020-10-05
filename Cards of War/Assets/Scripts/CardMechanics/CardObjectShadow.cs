using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CardObjectShadow : MonoBehaviour {
    GameObject shadow = null;

    bool shown = false;

    float duration = 0.05f;

    Vector3 offset = new Vector3(-0.25f, -0.25f);
    Vector3 setPos;

    private void Awake() {
        hideShadow();
        shadow = null;
    }


    private void Update() {
        if(shadow != null)
            moveShadow();
    }


    void createShadow() {
        shadow = new GameObject("Card Shadow");

        var sr = shadow.AddComponent<SpriteRenderer>();
        sr.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        sr.color = Color.black;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.g, 0.5f);
        sr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;

        shadow.transform.position = transform.position;
        shadow.transform.SetParent(transform);
    }

    void moveShadow() {
        shadow.SetActive(true);
        if(shown) {
            setPos = transform.position + offset;
            shadow.transform.DOMove(setPos, duration);
        }

        else
            moveToSetPos();
    }


    void moveToSetPos() {
        gameObject.transform.DOMove(setPos, duration);
        shadow.transform.DOMove(transform.position, duration);
    }


    public void hideShadow() {
        shown = false;
    }

    public void showShadow() {
        if(shadow == null)
            createShadow();

        shown = true;
    }

    public void destroyShadow() {
        Destroy(shadow);
        shadow = null;
    }


    public Vector2 getOffset() {
        return offset;
    }
}