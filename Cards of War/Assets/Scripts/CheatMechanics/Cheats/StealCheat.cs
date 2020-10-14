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

    private void LateUpdate() {
    }

    //  This cheat takes a card from the other player and adds it to the owner's deck
    public override void use() {
        //  player used cheat 
        if(gameObject.tag == "Player") {
            GameObject stolenCard = null;
            WinPile pileStolenFrom = null;
            foreach(var i in FindObjectsOfType<WinPile>()) {
                if(i.getMouseDown()) {
                    stolenCard = i.takeCardFromPile();
                    pileStolenFrom = i;
                    break;
                }
            }

            if(stolenCard != null && FindObjectOfType<CardMovement>().getPlayerHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getPlayerPlayedCard() == null) {
                stolenCard.GetComponent<ObjectShadow>().showShadow();
                FindObjectOfType<CardMovement>().setPlayerHeldCardObject(stolenCard);
                FindObjectOfType<CardMovement>().setPlayerHeldCardObjectOrigin(pileStolenFrom.gameObject);
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

    public override void showCanUse() {
        //  does nothing
    }

    public override void hideCanUse() {
        //  does nothing
    }



    //  used when the player takes a card from the opponent deck
    public override bool useCondition() {
        //  returns false if not charges
        if(!getCharged()) 
            return false;

        //  returns false if not enough cards for cheat to be used
        if(gameObject.tag == "Player") {
            if(GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getNumOfCardsInPile() <= 0 || gameObject.GetComponentInChildren<Deck>().getNumOfCardsInDeck() <= 0)
                return false;
        }
        else if(gameObject.tag == "Opponent") {
            if(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile() <= 0 || gameObject.GetComponentInChildren<Deck>().getNumOfCardsInDeck() <= 0)
                return false;
        }

        //  player is taking a card from a win pile
        if(gameObject.tag == "Player") {
            foreach(var i in FindObjectsOfType<WinPile>()) {
                if(i.getMouseDown())
                    return true;
            }
        }
        else if(gameObject.tag == "Opponent") 
            return gameObject.GetComponent<OpponentAI>().wantsToUseCheat;
        return false;
    }
}
