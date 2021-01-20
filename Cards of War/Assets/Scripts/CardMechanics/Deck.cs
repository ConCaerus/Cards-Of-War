using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour {
    //  list of card objects in deck
    List<GameObject> cardObjectsInDeck = new List<GameObject>();

    bool canStartPlaying = false;
    bool mouseOverCollider = false;

    private void Update() {
        setPosition();
    }


    private void OnMouseDrag() {
        //  pickup a card and move it with the mouse cursor.
        if(transform.parent.gameObject.tag == "Player" && canStartPlaying && getNumOfCardsInDeck() > 0) {
            playCard();
        }
    }

    private void OnMouseEnter() {
        mouseOverCollider = true;
    }
    private void OnMouseExit() {
        mouseOverCollider = false;
    }



    void setPosition() {
        if(transform.parent.gameObject.tag == "Player")
            transform.position = FindObjectOfType<Table>().getPlayerDeckPos();
        else if(transform.parent.gameObject.tag == "Opponent")
            transform.position = FindObjectOfType<Table>().getOpponentDeckPos();
    }


    //      Adding cards to the deck
    public void addCardToDeck(GameObject cardObject) {
        if(cardObject.GetComponent<CardObject>().getShowingface())
            cardObject.GetComponent<CardObject>().hideCardFace();

        cardObject.GetComponent<Collider2D>().enabled = false;
        cardObjectsInDeck.Add(cardObject);
        cardObject.transform.SetParent(gameObject.transform);
        setCardSortingOrder();
    }


    //  play card
    public void playCard() {
        //  player's deck
        if(transform.parent.tag == "Player") {
            //  can play card
            if(canStartPlaying && getNumOfCardsInDeck() > 0) {
                //  checks if card is already in play
                if(FindObjectOfType<CardMovement>().getPlayerHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getPlayerPlayedCard() == null) {
                    var card = takeCardInDeck();
                    card.GetComponent<ObjectShadow>().showShadow();
                    FindObjectOfType<CardMovement>().setPlayerHeldCardObject(card);
                    FindObjectOfType<CardMovement>().setPlayerHeldCardObjectOrigin(gameObject);
                }
            }
        }


        //  opponent's deck
        else if(transform.parent.tag == "Opponent") {
            //  can play card
            if(canStartPlaying && getNumOfCardsInDeck() > 0) {
                //  checks if card is already in play
                if(FindObjectOfType<CardMovement>().getOpponentHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getOpponentPlayedCard() == null) {
                    var card = takeCardInDeck();
                    card.GetComponent<ObjectShadow>().showShadow();
                    FindObjectOfType<CardMovement>().setOpponentHeldCardObject(card);
                    FindObjectOfType<CardMovement>().moveOpponentHeldCardToPlayPos();
                }
            }
        }
    }


    void setCardSortingOrder() {
        for(int i = 0; i < cardObjectsInDeck.Count; i++) {
            cardObjectsInDeck[i].GetComponent<SpriteRenderer>().sortingOrder = i - cardObjectsInDeck.Count;
        }
    }


    //      getters 
    public int getNumOfCardsInDeck() {
        return cardObjectsInDeck.Count;
    }

    public GameObject takeCardInDeck() {
        var temp = cardObjectsInDeck[cardObjectsInDeck.Count - 1];
        cardObjectsInDeck.RemoveAt(cardObjectsInDeck.Count - 1);
        temp.transform.SetParent(null);
        return temp;
    }


    //  fucking watch this little cunt
    public GameObject getNextCardInDeck() {
        return cardObjectsInDeck[cardObjectsInDeck.Count - 1];
    }

    public Vector2 getDeckPos() {
        return transform.position;
    }

    public bool getMouseOverCollider() {
        return mouseOverCollider;
    }

    //  setters
    public void setCanStartPlaying(bool b) {
        canStartPlaying = b;
    }
}
