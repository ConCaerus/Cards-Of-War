using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Phone : MonoBehaviour {
    [SerializeField] Sprite bigShineSprite, smallShineSprite;
    [SerializeField] Vector2 bigShineOffset, smallShineOffset;
    GameObject bigShine, smallShine;

    Canvas shownCanvas;


    private void Start() {
        bigShine = new GameObject("Big Shine");
        var sr = bigShine.AddComponent<SpriteRenderer>();
        sr.sprite = bigShineSprite;
        bigShine.transform.position = transform.position;
        bigShine.transform.SetParent(transform);

        smallShine = new GameObject("Small Shine");
        sr = smallShine.AddComponent<SpriteRenderer>();
        sr.sprite = smallShineSprite;
        smallShine.transform.position = transform.position;
        smallShine.transform.SetParent(transform);
    }

    private void LateUpdate() {
        if(Input.GetKeyDown(KeyCode.W))
            show();
        else if(Input.GetKeyDown(KeyCode.S))
            hide();
        positionShines();
    }


    void positionShines() {
        bigShine.transform.DOMove((Vector2)transform.position + bigShineOffset, 0.05f);
        smallShine.transform.DOMove((Vector2)transform.position + smallShineOffset, 0.05f);
    }


    public void show() {
        transform.DOMove((Vector2)Camera.main.transform.position + new Vector2(0.0f, -3.0f), 0.25f);
    }

    public void hide() {
        transform.DOMove((Vector2)Camera.main.transform.position + new Vector2(0.0f, -8.0f), 0.25f);
    }


    public void setShownCanvas(Canvas canvas) {
        shownCanvas = canvas;
    }
}
