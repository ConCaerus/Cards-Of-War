using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AddCardsHand : MonoBehaviour {
    [SerializeField] float yCoord, shownXCoord, hiddenXCoord;
    const float speed = 0.35f;

    List<GameObject> heldCards = new List<GameObject>();

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

    public void populateHand(int numOfCards) {
        for(int i = 0; i < numOfCards; i++) {
            Card card = FindObjectOfType<MasterDeck>().takeRandomCardFromPreset();
            GameObject co = FindObjectOfType<MasterDeck>().createCardObject(card);
            co.transform.position = transform.position + new Vector3(-0.35f, 0.0f, 0.0f);
            co.transform.SetParent(transform);
            co.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
            heldCards.Add(co);
        }
    }




    public bool isMouseOver() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    public List<GameObject> takeCards() {
        List<GameObject> temp = new List<GameObject>();
        temp.Clear();
        foreach(var i in heldCards)
            temp.Add(i);
        heldCards.Clear();
        return temp;
    }

    public int getCardCount() {
        return heldCards.Count;
    }
}
