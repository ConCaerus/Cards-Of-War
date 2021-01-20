using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AdditionalValueButton : MonoBehaviour {
    [SerializeField] float yCoord, hiddenXCoord, shownXCoord;
    const float speed = 0.15f;

    private void Start() {
        if(transform.parent.gameObject.tag == "Player")
            yCoord -= transform.localScale.y / 3.0f;
        else if(transform.parent.gameObject.tag == "Opponent")
            yCoord += transform.localScale.y / 3.0f;
        forceHide();
    }



    public void show() {
        transform.DOMove(new Vector3(shownXCoord, yCoord, transform.position.z), speed);
    }

    public void hide() {
        transform.DOMove(new Vector3(hiddenXCoord, yCoord, transform.position.z), speed);
    }

    public void forceHide() {
        transform.DOComplete();
        transform.position = new Vector3(hiddenXCoord, yCoord, transform.position.z);
    }


    public bool isMouseOver() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }
}
