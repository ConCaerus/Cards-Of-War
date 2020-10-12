using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardsCheat : Cheat {
    const int cardsToAdd = 2;

    public override float getChargeWinAmount() {
        return 3.0f;
    }

    public override string getName() {
        return "Add Cards Cheat";
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

        if(gameObject.tag == "Opponent")
            GetComponent<DialogHandler>().startCheatDialog();

        setChargeAmount(0.0f);
    }


    public override bool canBeUsed() {
        return GetComponentInChildren<Deck>().getNumOfCardsInDeck() > 0;
    }

    public override bool useCondition() {
        if(chargeAmount >= filledChargeAmount) {
            if(gameObject.tag == "Player")
                return Input.GetKeyDown(KeyCode.Space);
            return true;
        }
        return false;
    }


    //  I know this function scares you Connor
    //  You'll be okay. I'm here for you

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
