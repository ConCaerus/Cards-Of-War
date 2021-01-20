using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardsCheat : Cheat {
    const int cardsToAdd = 2;

    public GameObject hand;
    GameObject handInstance;

    
    private void Start() {
        if(gameObject.tag == "Opponent" && GetComponent<OpponentAI>() == null)
            gameObject.AddComponent<DefaultOpponentAI>();

        if(hand == null) 
            hand = FindObjectOfType<CheatIndex>().gameObject.GetComponent<AddCardsCheat>().hand;

        if(getInUse())
            createHand();
    }

    public override float getChargeWinAmount() {
        return 3.0f;
    }

    public override string getName() {
        return "Add Cards Cheat";
    }


    //  this cheat adds card to the user's deck
    public override void use() {
        if(gameObject.tag == "Player" && Input.GetMouseButtonDown(0) && handInstance.GetComponent<AddCardsHand>().isMouseOver()) {
            StartCoroutine(animateCardsWithDelay(handInstance.GetComponent<AddCardsHand>().takeCards()));
            setChargeAmount(0.0f);
        }

        else if(gameObject.tag == "Opponent") {
            StartCoroutine(waitToAddCards());
        }
    }


    public override void showCanUse() {
        if(handInstance.GetComponent<AddCardsHand>().getCardCount() == 0)
            handInstance.GetComponent<AddCardsHand>().populateHand(cardsToAdd);

        handInstance.GetComponent<AddCardsHand>().show();
    }

    public override void hideCanUse() {
    }


    void createHand() {
        handInstance = Instantiate(hand);
        handInstance.transform.SetParent(transform);
        handInstance.GetComponent<AddCardsHand>().forceHide();
    }


    public override bool useCondition() {
        return getCharged();
    }



    IEnumerator waitToAddCards() {
        yield return new WaitForSeconds(0.25f);

        StartCoroutine(animateCardsWithDelay(handInstance.GetComponent<AddCardsHand>().takeCards()));
        setChargeAmount(0.0f);
    }

    //  I know this function scares you Connor
    //  You'll be okay. I'm here for you

    IEnumerator animateCardsWithDelay(List<GameObject> cards) {
        if(cards.Count > 0) {
            int lastIndex = cards.Count - 1;


            if(gameObject.tag == "Player")
                FindObjectOfType<CardMovement>().moveCardObjectToPlayerDeckPos(cards[lastIndex]);
            else if(gameObject.tag == "Opponent")
                FindObjectOfType<CardMovement>().moveCardObjectToOpponentDeckPos(cards[lastIndex]);

            GetComponentInChildren<Deck>().addCardToDeck(cards[lastIndex]);
            cards.RemoveAt(lastIndex);
        }
        yield return new WaitForSeconds(0.1f);

        if(cards.Count > 0)
            StartCoroutine(animateCardsWithDelay(cards));
        else 
            handInstance.GetComponent<AddCardsHand>().hide();
    }
}
