using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour {
    //  list of card objects in deck
    List<GameObject> cardObjectsInDeck = new List<GameObject>();

    bool canStartPlaying = false;
    bool mouseOverCollider = false;


    private void OnMouseDrag() {
        //  pickup a card and move it with the mouse cursor.
        if(transform.parent.gameObject.tag == "Player" && canStartPlaying && getNumOfCardsInDeck() > 0) {
            //  moves the taken card over to the cardMovement script
            if(FindObjectOfType<CardMovement>().getPlayerHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getPlayerPlayedCard() == null) {
                var card = takeCardInDeck();
                card.GetComponent<CardObjectShadow>().startShowingShadow();
                FindObjectOfType<CardMovement>().setPlayerHeldCardObject(card);
            }
        }
    }

    private void OnMouseEnter() {
        mouseOverCollider = true;
    }
    private void OnMouseExit() {
        mouseOverCollider = false;
    }


    //      Adding cards to the deck
    public void addCardToDeck(GameObject cardObject) {
        if(cardObject.GetComponent<CardObject>().getShowingface())
            cardObject.GetComponent<CardObject>().hideCardFace();

        cardObjectsInDeck.Add(cardObject);
        cardObject.transform.SetParent(gameObject.transform);
    }


    //  opponent code

    public void opponentStartPlayingCard() {
        if(FindObjectOfType<CardMovement>().getOpponentHeldCardObject() == null && canStartPlaying) {
            //  plays a card from their deck
            var card = takeCardInDeck();
            card.GetComponent<CardObjectShadow>().startShowingShadow();
            FindObjectOfType<CardMovement>().setOpponentHeldCardObject(card);

            FindObjectOfType<CardMovement>().moveOpponentCardObject();
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

    public Vector2 getDeckPos() {
        return transform.position;
    }

    public bool getMouseOverCollider() {
        return mouseOverCollider;
    }


    //  waiters

    public IEnumerator waitToStartPlaying() {
        yield return new WaitForSeconds(1.0f);

        canStartPlaying = true;
        
        if(transform.parent.tag == "Opponent")
            opponentStartPlayingCard();
    }
}
