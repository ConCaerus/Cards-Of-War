using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBattleMechanics : MonoBehaviour {
    GameObject playerPlayedCard, opponentPlayedCard;

    int playerCardValueMod = 0, opponentCardValueMod = 0;
    int tempPlayerCardValueMod = 0, tempOpponentCardValueMod = 0;

    bool shown = false;

    Coroutine startedWaitToResolveBattle;


    private void Update() {
        managePlayedCardsResolveConditions();
        managePlayedCardsShowConditions();
    }


    //  long function names brother.

    //  shows the card faces if the player wants to advance the battle
    void managePlayedCardsShowConditions() {
        //  the player has played a card
        if(playerPlayedCard != null) {
            //  the player wants to show their card
            opponentPlayedCard.GetComponent<CardObjectShadow>().hideShadow();

            if(advanceBattle()) {
                //  stop card shadows
                playerPlayedCard.GetComponent<CardObjectShadow>().destroyShadow();
                opponentPlayedCard.GetComponent<CardObjectShadow>().destroyShadow();

                showPlayerPlayedCard();
                showOpponentPlayedCard();

                showCardResolveParticles();

                shown = true;
                startedWaitToResolveBattle = StartCoroutine(waitToResolveBattle());
            }
        }
    }

    //  if the player wants to resolve the battle before the coroutine is done
    void managePlayedCardsResolveConditions() {
        //  player is looking at the results of the battle
        if(shown == true) {
            //  player is ready to resolve the battle and have the cards go to the winner
            if(advanceBattle()) {
                StopCoroutine(startedWaitToResolveBattle);
                resolveBattle();
            }
        }
    }


    bool advanceBattle() {
        return Input.GetMouseButtonDown(0);
    }


    //                      battle mechanics

    void showPlayerPlayedCard() {
        playerPlayedCard.GetComponent<CardObject>().showCardFace();
    }

    void showOpponentPlayedCard() {
        opponentPlayedCard.GetComponent<CardObject>().showCardFace();
    }


    void showCardResolveParticles() {
        int playerVal = playerPlayedCard.GetComponent<CardObject>().getCard().value + playerCardValueMod + tempPlayerCardValueMod;
        int opponentVal = opponentPlayedCard.GetComponent<CardObject>().getCard().value + opponentCardValueMod + tempOpponentCardValueMod;

        FindObjectOfType<CardResolveParticles>().playParticles(playerVal, playerPlayedCard.transform.position,
                                                                opponentVal, opponentPlayedCard.transform.position);

        /*  Card win particle code
        if(playerVal > opponentVal)
            FindObjectOfType<CardWinParticles>().playParticles(playerPlayedCard.transform.position);
        else if(opponentVal > playerVal)
            FindObjectOfType<CardWinParticles>().playParticles(opponentPlayedCard.transform.position);
        */
    }



    void resolveBattle() {
        int playerVal = playerPlayedCard.GetComponent<CardObject>().getCard().value + playerCardValueMod + tempPlayerCardValueMod;
        int opponentVal = opponentPlayedCard.GetComponent<CardObject>().getCard().value + opponentCardValueMod + tempOpponentCardValueMod;

        //  manage if a player has ran out of cards
        outOfCardsHandler();

        //  player wins
        if(playerVal > opponentVal) {
            //  start moving the cards to the win piles
            FindObjectOfType<CardMovement>().moveResolvedCardObjectsPlayerWins(opponentPlayedCard, playerPlayedCard);

            //  modify the cheat values
            modifyCheatValsPlayerWins();
        }

        //  opponent wins
        else if(opponentVal > playerVal) {
            //  start moving the cards to the win piles
            FindObjectOfType<CardMovement>().moveResolvedCardObjectsOpponentWins(playerPlayedCard, opponentPlayedCard);

            //  modify the cheat values
            modifyCheatValsOpponentWins();
        }

        //  draw
        else {
            //  start moving the cards to the win piles
            FindObjectOfType<CardMovement>().moveResolvedCardObjectsDraw(playerPlayedCard, opponentPlayedCard);

            //  modify the cheat values
            modifyCheatValsDraw();
        }


        //  resets card variables and turns shown to false
        playerPlayedCard = null;
        opponentPlayedCard = null;
        shown = false;

        //  resets the temp card value mods
        tempPlayerCardValueMod = 0;
        tempOpponentCardValueMod = 0;


        //  tells the opponent to use their cheat if they want to
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<CheatHandler>().opponentCheatUseHandler();
    }



    void resolveGame() {
        int playerWinCount = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile();
        int opponentWinCount = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getNumOfCardsInPile();

        //  player won
        if(playerWinCount > opponentWinCount) {
            GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startLoseDialog();
        }

        //  opponent won
        if(opponentWinCount > playerWinCount) {
            GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startWinDialog();
        }
    }




    //  manage out of cards situations

    void outOfCardsHandler() {
        Deck plDeck = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>();
        Deck opDeck = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>();

        bool endGame = false;

        //  player ran out of cards
        if(plDeck.getNumOfCardsInDeck() == 0) {
            endGame = true;

            while(opDeck.getNumOfCardsInDeck() > 0)
                FindObjectOfType<CardMovement>().moveCardObjectToOpponentWinPile(opDeck.takeCardInDeck());
        }

        //  opponent ran out of cards
        else if(opDeck.getNumOfCardsInDeck() == 0) {
            endGame = true;

            while(plDeck.getNumOfCardsInDeck() > 0)
                FindObjectOfType<CardMovement>().moveCardObjectToPlayerWinPile(plDeck.takeCardInDeck());
        }


        if(endGame) {
            resolveGame();
        }
    }


    //  modify cheat charges based on the outcome of the battle

    void modifyCheatValsPlayerWins() {
        CheatHandler playerCH = GameObject.FindGameObjectWithTag("Player").GetComponent<CheatHandler>();
        if(playerCH != null)
            playerCH.addWinChargeAmount();

        CheatHandler opponentCH = GameObject.FindGameObjectWithTag("Opponent").GetComponent<CheatHandler>();
        if(opponentCH != null)
            opponentCH.addLoseChargeAmount();
    }

    void modifyCheatValsOpponentWins() {
        CheatHandler playerCH = GameObject.FindGameObjectWithTag("Player").GetComponent<CheatHandler>();
        if(playerCH != null)
            playerCH.addLoseChargeAmount();

        CheatHandler opponentCH = GameObject.FindGameObjectWithTag("Opponent").GetComponent<CheatHandler>();
        if(opponentCH != null)
            opponentCH.addWinChargeAmount();
    }

    void modifyCheatValsDraw() {
        CheatHandler playerCH = GameObject.FindGameObjectWithTag("Player").GetComponent<CheatHandler>();
        if(playerCH != null)
            playerCH.addLoseChargeAmount();

        CheatHandler opponentCH = GameObject.FindGameObjectWithTag("Opponent").GetComponent<CheatHandler>();
        if(opponentCH != null)
            opponentCH.addLoseChargeAmount();
    }


    //                      setters

    //  Played Cards
    public void setPlayerPlayedCard(GameObject card) {
        playerPlayedCard = card;
        playerPlayedCard.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void setOpponentPlayedCard(GameObject card) {
        opponentPlayedCard = card;
        opponentPlayedCard.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }


    //  Card value mods
    public void setPlayerCardValueMod(int f) {
        playerCardValueMod = f;
    }

    public void setOpponentCardValueMod(int f) {
        opponentCardValueMod = f;
    }


    //  Temp card value mods
    public void setTempPlayerCardValueMod(int f) {
        tempPlayerCardValueMod = f;
    }

    public void setTempOpponentCardValueMod(int f) {
        tempOpponentCardValueMod = f;
    }


    //                      getters

    //  Played Card objects
    public GameObject getPlayerPlayedCard() {
        return playerPlayedCard;
    }

    public GameObject getOpponentPlayedCard() {
        return opponentPlayedCard;
    }


    //  Card value mods
    public int getPlayerCardValueMod() {
        return playerCardValueMod;
    }

    public int getOpponentCardValueMod() {
        return opponentCardValueMod;
    }


    //  temp Card value mods
    public int getTempPlayerCardValueMod() {
        return tempPlayerCardValueMod;
    }

    public int getTempOpponentCardValueMod() {
        return tempOpponentCardValueMod;
    }

    public bool getShown() {
        return shown;
    }


    //                      waiters

    IEnumerator waitToResolveBattle() {
        yield return new WaitForSeconds(1.0f);

        resolveBattle();
    }
}
