using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealCheat : Cheat {
    public override float getChargeWinAmount() {
        return 2.5f;
    }

    public override string getName() {
        return "Steal Cheat";
    }

    //  This cheat takes a card from the other player and adds it to the owner's deck
    public override void use() {
        //  player used cheat 
        if(gameObject.tag == "Player") {
            GameObject stolenCard = null;
            foreach(var i in FindObjectsOfType<WinPile>()) {
                if(i.getMouseDown()) {
                    stolenCard = i.takeCardFromPile();
                    break;
                }
            }

            if(stolenCard != null && FindObjectOfType<CardMovement>().getPlayerHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getPlayerPlayedCard() == null) {
                stolenCard.GetComponent<CardObjectShadow>().showShadow();
                FindObjectOfType<CardMovement>().setPlayerHeldCardObject(stolenCard);
            }
        }

        /*
        //  opponent used cheat
        else if(gameObject.tag == "Opponent") {
            var stolenCard = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().takeCardFromPile();

            if(stolenCard != null) {
                gameObject.GetComponentInChildren<Deck>().addCardToDeck(stolenCard);

                FindObjectOfType<CardMovement>().stopMovingOpponentHeldCardObject();
            }

            GetComponent<DialogHandler>().startCheatDialog();
        } */

        setChargeAmount(0.0f);
    }


    //  cannot be used if the other player does not have any cards in their win pile
    public override bool canBeUsed() {
        if(gameObject.tag == "Player")
            return GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getNumOfCardsInPile() > 0 &&
                    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getNumOfCardsInDeck() > 0;
        else if(gameObject.tag == "Opponent")
            return GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile() > 0 &&
                    GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getNumOfCardsInDeck() > 0;
        
        return false;
    }

    //  used when the player takes a card from the opponent deck
    public override bool useCondition() {
        if(chargeAmount >= filledChargeAmount) {
            foreach(var i in FindObjectsOfType<WinPile>()) {
                if(i.getMouseDown())
                    return true;
            }
        }
        return false;
    }
}
