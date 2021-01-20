using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ObjectShadow : MonoBehaviour {
    GameObject shadow = null;

    bool shown = false;

    float duration = 0.05f;

    Vector3 offset = new Vector3(-0.15f, -0.25f);
    Vector3 setPos;


    private void LateUpdate() {
        if(shadow != null && shadow.transform.parent != null) {
            moveShadow();
        }
    }


    void createShadow() {
        shadow = new GameObject("Shadow");

        var sr = shadow.AddComponent<SpriteRenderer>();
        sr.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        sr.color = Color.black;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.g, 0.65f);
        sr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
        sr.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        shadow.transform.position = transform.position;
        shadow.transform.rotation = transform.rotation;
        shadow.transform.localScale = transform.localScale;
        shadow.transform.SetParent(transform);
    }

    void moveShadow() {
        shadow.SetActive(true);
        shadow.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
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

        shadow.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;

        if(gameObject.transform.position == setPos)
            destroyShadow();
    }


    public void hideShadow() {
        shown = false;

        if(shadow != null)
            shadow.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
    }

    public void showShadow() {
        if(shadow == null)
            createShadow();

        shown = true;
    }

    public void destroyShadow() {
        Destroy(shadow);
        shadow = null;
        shown = false;
    }


    public Vector2 getOffset() {
        return offset;
    }

    public bool getShown() {
        return shown;
    }
}