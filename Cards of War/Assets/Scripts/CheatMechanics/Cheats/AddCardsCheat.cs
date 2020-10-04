using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardsCheat : Cheat {
    const int cardsToAdd = 2;

    public override float getChargeAmount() {
        return 2.5f;
    }


    //  this cheat adds card to the user's deck
    public override void use() {
        
    }


    public override bool canBeUsed() {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<Deck>().getNumOfCardsInDeck() > 0 &&
                GameObject.FindGameObjectWithTag("Opponent").GetComponent<Deck>().getNumOfCardsInDeck() > 0;
    }

    public override void addToPlayer() {
        GameObject.FindGameObjectWithTag("Player").AddComponent<AddCardsCheat>();
    }
}
