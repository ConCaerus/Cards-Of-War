using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour {
    Card card;
    
    [SerializeField] Sprite cardBack;

    bool faceShown = false;

    private void Awake() {
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        hideCardFace();
    }


    private void Start() {
        if(FindObjectOfType<TableCanvas>() != null) {
            transform.rotation = FindObjectOfType<TableCanvas>().getTableRotation();
        }
    }



    public void showCardFace() {
        if(card != null) {
            gameObject.GetComponent<SpriteRenderer>().sprite = card.sprite;
            faceShown = true;
        }
        else {
            Debug.Log("Trying to show a cardObject's face that doesn't have a card");
        }
    }

    public void hideCardFace() {
        gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
        faceShown = false;
    }


    public void setCard(Card c) {
        card = c;
    }

    public Card getCard() {
        return card;
    }

    public bool getShowingface() {
        return faceShown;
    }
}
