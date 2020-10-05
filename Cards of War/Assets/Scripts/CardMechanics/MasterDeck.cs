using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDeck : MonoBehaviour {

    /*

    Clubs       [0, 12]
    Diamonds    [13, 25]
    Hearts      [26, 38]
    Spades      [39, 51]

    */

    //  this is the list that does not get touched. It is only here so I have a place where I can get a list of all the cards
    [SerializeField] Card[] filledDeckPreset;
    //  this deck is so I know what cards are have been used
    List<Card> filledDeck = new List<Card>();

    //  default card object preset
    public GameObject cardObject;

    Deck playerDeck, opponentDeck;

    const float deckPopDelay = 0.01f;


    private void Awake() {
        //  destroys any cards already in the scene
        destroyAllCardsInScene();

        //  resets the filled deck so that it is filled with cards
        resetFilledDeck();

        //  Automatically sets the player and opponent deck in the script.
        playerDeck = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>();
        opponentDeck = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>();
    }


    void resetFilledDeck() {
        filledDeck.Clear();
        foreach(var i in filledDeckPreset)
            filledDeck.Add(i);
    }

    void destroyAllCardsInScene() {
        foreach(var i in FindObjectsOfType<CardObject>()) {
            Destroy(i.gameObject);
        }
    }
    

    public void populateDecks() {
        List<GameObject> playerCards = new List<GameObject>();
        List<GameObject> opponentCards = new List<GameObject>();

        playerCards.Clear();
        opponentCards.Clear();
        for(int i = 0; i < filledDeckPreset.Length; i++) {
            if(i % 2 == 0) {    //  sort the card into the player's deck
                //  create a card object and send it to the player's deck
                var temp = createCardObject(takeRandomCard());
                playerDeck.addCardToDeck(temp);

                playerCards.Add(temp);
            } 
            else {  //  sort the card into the opponent's deck
                //  create a card object and send it to the opponent's deck
                var temp = createCardObject(takeRandomCard());
                opponentDeck.addCardToDeck(temp);

                opponentCards.Add(temp);
            }
        }

        StartCoroutine(delayMovingCardsToDecks("Player", playerCards));
        StartCoroutine(delayMovingCardsToDecks("Opponent", opponentCards));
    }

    //  creates a card object from a regular card
    public GameObject createCardObject(Card card) {
        var ob = Instantiate(cardObject, transform.position, Quaternion.identity);
        ob.GetComponent<CardObject>().setCard(card);

        return ob;
    }

    //  takes random card from the filledDeck
    public Card takeRandomCard() {
        if(filledDeck.Count > 0) {
            int rand = Random.Range(0, filledDeck.Count);

            Card temp = filledDeck[rand];

            filledDeck.Remove(temp);

            return temp;
        }

        Debug.Log("Out of cards");
        return null;
    }

    //  takes random card from the deck preset
    public Card takeRandomCardFromPreset() {
        int rand = Random.Range(0, filledDeckPreset.Length - 1);
        return filledDeckPreset[rand];
    }


    IEnumerator delayMovingCardsToDecks(string type, List<GameObject> cards) {
        int lastIndex = cards.Count - 1;

        yield return new WaitForSeconds(deckPopDelay);

        if(type == "Player")
            FindObjectOfType<CardMovement>().moveCardObjectToPlayerDeckPos(cards[lastIndex]);
        else if(type == "Opponent")
            FindObjectOfType<CardMovement>().moveCardObjectToOpponentDeckPos(cards[lastIndex]);

        cards.RemoveAt(lastIndex);

        //  not done sorting cards
        if(cards.Count > 0)
            StartCoroutine(delayMovingCardsToDecks(type, cards));

        //  done sorting cards
        else {
            foreach(var i in FindObjectsOfType<Deck>())
                i.setCanStartPlaying(true);
        }
    }
}