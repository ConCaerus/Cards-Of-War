using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardsCheat : Cheat {
    const int cardsToAdd = 3;

    public override float getChargeAmount() {
        return 5.0f;
    }


    //  this cheat adds card to the user's deck
    public override void use() {
        List<GameObject> cards = new List<GameObject>();
        cards.Clear();
        for(int i = 0; i < cardsToAdd; i++) {
            //  creates random card object
            var temp = FindObjectOfType<MasterDeck>().createCardObject(FindObjectOfType<MasterDeck>().takeRandomCardFromPreset());
            cards.Add(temp);
        }

        //  animates and adds the cards
        StartCoroutine(animateCardsWithDelay(cards));
    }


    public override bool canBeUsed() {
        return GetComponentInChildren<Deck>().getNumOfCardsInDeck() > 0;
    }

    public override void addToPlayer() {
        GameObject.FindGameObjectWithTag("Player").AddComponent<AddCardsCheat>();
    }


    IEnumerator animateCardsWithDelay(List<GameObject> cards) {
        int lastIndex = cards.Count - 1;
        if(gameObject.tag == "Player")
            FindObjectOfType<CardMovement>().moveCardObjectToPlayerDeckPos(cards[lastIndex]);
        else if(gameObject.tag == "Opponent")
            FindObjectOfType<CardMovement>().moveCardObjectToOpponentDeckPos(cards[lastIndex]);

        GetComponentInChildren<Deck>().addCardToDeck(cards[lastIndex]);
        cards.RemoveAt(lastIndex);
        yield return new WaitForSeconds(0.1f);

        if(cards.Count > 0)
            StartCoroutine(animateCardsWithDelay(cards));
    }
}
