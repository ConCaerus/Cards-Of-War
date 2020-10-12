using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheat : Cheat {
    public override float getChargeWinAmount() {
        return 2.5f;
    }

    public override string getName() {
        return "Win Cheat";
    }

    //  this cheat makes the active card's value increase by 100, automatically winning
    public override void use() {
        //  the player played the cheat
        if(gameObject.tag == "Player") {
            FindObjectOfType<CardBattleMechanics>().setTempPlayerCardValueMod(FindObjectOfType<CardBattleMechanics>().getTempPlayerCardValueMod() + 100);
        }

        //  the opponent played the cheat
        else if(gameObject.tag == "Opponent") {
            FindObjectOfType<CardBattleMechanics>().setTempOpponentCardValueMod(FindObjectOfType<CardBattleMechanics>().getTempOpponentCardValueMod() + 100);
        }

        setChargeAmount(0.0f);
    }


    public override bool canBeUsed() {
        if(GameObject.FindGameObjectWithTag("Player") == null) return false;
        bool temp = (GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getNumOfCardsInDeck() > 0 ||
                        FindObjectOfType<CardMovement>().getOpponentHeldCardObject() != null ||
                        (FindObjectOfType<CardBattleMechanics>().getOpponentPlayedCard() != null && !FindObjectOfType<CardBattleMechanics>().getShown()))
                        &&
                        (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getNumOfCardsInDeck() > 0 ||
                        FindObjectOfType<CardMovement>().getPlayerHeldCardObject() != null ||
                        (FindObjectOfType<CardBattleMechanics>().getPlayerPlayedCard() != null && !FindObjectOfType<CardBattleMechanics>().getShown()));

        return temp;
    }

    public override bool useCondition() {
        if(chargeAmount >= filledChargeAmount) {
            if(gameObject.tag == "Player")
                return Input.GetKeyDown(KeyCode.Space);
            return true;
        }
        return false;
    }
}
