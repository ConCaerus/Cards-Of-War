using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpponentAI : MonoBehaviour {
    public bool wantsToUseCheat = false;

    public void playCard() {
        GetComponentInChildren<Deck>().playCard();
    }

    public bool cardInPlay() {
        return !(FindObjectOfType<CardMovement>().getOpponentHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getOpponentPlayedCard() == null);
    }
}