using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpponentAI : MonoBehaviour {

    public void playCard() {
        GetComponentInChildren<Deck>().playCard();
    }

    public void useCheat() {
        GetComponent<Cheat>().use();
    }

    public bool cardInPlay() {
        return !(FindObjectOfType<CardMovement>().getOpponentHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getOpponentPlayedCard() == null);
    }
}