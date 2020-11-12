using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPile : MonoBehaviour {
    List<GameObject> cardsInPile = new List<GameObject>();

    bool mouseDown = false, mouseOver = false;


    //  mouse is trying to drag a card from this win pile
    private void OnMouseDown() {
        mouseDown = true;
    }
    private void OnMouseUp() {
        mouseDown = false;
    }

    private void OnMouseEnter() {
        mouseOver = true;
    }
    private void OnMouseExit() {
        mouseOver = false;
    }


    private void Update() {
        setPosition();
    }


    void setPosition() {
        if(transform.parent.gameObject.tag == "Player")
            transform.position = FindObjectOfType<Table>().getPlayerWinPilePos();
        else if(transform.parent.gameObject.tag == "Opponent")
            transform.position = FindObjectOfType<Table>().getOpponentWinPilePos();
    }


    void setSortingOrderForCardsInPile() {
        for(int i = 0; i < cardsInPile.Count - 1; i++) {
            cardsInPile[i].GetComponent<SpriteRenderer>().sortingOrder = -(cardsInPile.Count - 1 - i);
            if(cardsInPile[i].GetComponentInChildren<SpriteRenderer>() != null) {
                foreach(var sr in cardsInPile[i].GetComponentsInChildren<SpriteRenderer>())
                    sr.sortingOrder = cardsInPile[i].GetComponent<SpriteRenderer>().sortingOrder + 1;
            }
        }
    }


    public void addCardToPile(GameObject card, GameObject other = null) {
        card.GetComponent<ObjectShadow>().hideShadow();
        card.GetComponent<Collider2D>().enabled = false;
        cardsInPile.Add(card);
        card.transform.SetParent(transform);


        setSortingOrderForCardsInPile();

        if(other != null)
            addCardToPile(other);
    }

    public GameObject takeCardFromPile() {
        GameObject temp = cardsInPile[cardsInPile.Count - 1];
        cardsInPile.RemoveAt(cardsInPile.Count - 1);
        temp.transform.SetParent(null);
        return temp;
    }


    //  does not remove the card from the win pile
    //  need to be careful not to call this function instead of takeCardFromPile
    public GameObject getTopCardInPile() {
        return cardsInPile[cardsInPile.Count - 1];
    }

    public int getNumOfCardsInPile() {
        return cardsInPile.Count;
    }

    public Vector2 getWinPilePos() {
        return transform.position;
    }

    public bool getMouseDown() {
        return mouseDown;
    }
    public bool getMouseOver() {
        return mouseOver;
    }
}
