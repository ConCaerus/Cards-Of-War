using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealCheat : Cheat {
    public override float getChargeWinAmount() {
        return 2.0f;
    }

    public override string getName() {
        return "Steal Cheat";
    }

    //  This cheat takes a card from the other player and adds it to the owner's deck
    public override void use() {
        //  player used cheat
        if(gameObject.tag == "Player") {
            var stolenCard = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().takeCardFromPile();

            if(stolenCard != null) {
                gameObject.GetComponentInChildren<Deck>().addCardToDeck(stolenCard);

                FindObjectOfType<CardMovement>().stopMovingPlayerHeldCardObject();
                FindObjectOfType<CardMovement>().moveCardObjectToPlayerDeckPos(stolenCard);
            }
        }

        //  opponent used cheat
        else if(gameObject.tag == "Opponent") {
            var stolenCard = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().takeCardFromPile();

            if(stolenCard != null) {
                gameObject.GetComponentInChildren<Deck>().addCardToDeck(stolenCard);

                FindObjectOfType<CardMovement>().stopMovingOpponentHeldCardObject();
            }

            GetComponent<DialogHandler>().startCheatDialog();
        }

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
    //  oh mate, you're ganna need a thicc cup of joe for this fucking doosey
    public override bool useCondition() {
        return false;
    }
}
