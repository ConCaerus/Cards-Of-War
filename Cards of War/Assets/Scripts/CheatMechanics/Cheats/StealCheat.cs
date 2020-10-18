using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealCheat : Cheat {
    public enum OpponentStealOption {
        addToWinPile    = 0, 
        playFromOther   = 1, 
        playFromOwn     = 2
    }


    private void Start() {
        if(gameObject.tag == "Opponent" && GetComponent<StealCheatOpponentAI>() == null)
            gameObject.AddComponent<StealCheatOpponentAI>();
    }

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


        //  opponent used cheat
        else if(gameObject.tag == "Opponent") {
            GameObject stolenCard = null;

            switch(GetComponent<StealCheatOpponentAI>().chooseWhatToSteal()) {

                //  add a card from the player's win pile to their own win pile
                case OpponentStealOption.addToWinPile:
                    stolenCard = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().takeCardFromPile();

                    if(stolenCard != null) {
                        gameObject.GetComponentInChildren<WinPile>().addCardToPile(stolenCard);
                        FindObjectOfType<CardMovement>().moveCardObjectToOpponentWinPile(stolenCard);
                    }
                    break;

                //  plays a card from the player's win pile
                case OpponentStealOption.playFromOther:
                    stolenCard = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().takeCardFromPile();

                    if(stolenCard != null) {
                        if(FindObjectOfType<CardMovement>().getOpponentHeldCardObject() == null) {
                            stolenCard.GetComponent<ObjectShadow>().showShadow();
                            FindObjectOfType<CardMovement>().setOpponentHeldCardObject(stolenCard);
                            FindObjectOfType<CardMovement>().moveOpponentHeldCardToPlayPos();
                        }
                        else
                            GetComponentInChildren<Deck>().addCardToDeck(stolenCard);
                    }
                    break;

                //  plays a card the their own win pile
                case OpponentStealOption.playFromOwn:
                    stolenCard = GetComponentInChildren<WinPile>().takeCardFromPile();

                    if(stolenCard != null) {
                        if(FindObjectOfType<CardMovement>().getOpponentHeldCardObject() == null) {
                            stolenCard.GetComponent<ObjectShadow>().showShadow();
                            FindObjectOfType<CardMovement>().setOpponentHeldCardObject(stolenCard);
                            FindObjectOfType<CardMovement>().moveOpponentHeldCardToPlayPos();
                        }
                        else
                            GetComponentInChildren<Deck>().addCardToDeck(stolenCard);
                    }
                    break;
            }

        }

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
            if(gameObject.GetComponentInChildren<Deck>().getNumOfCardsInDeck() <= 0)
                return false;
        }
        else if(gameObject.tag == "Opponent") {
            if(gameObject.GetComponentInChildren<Deck>().getNumOfCardsInDeck() <= 0)
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
