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
    }



    void resolveGame() {
        GameInformation.playerEndScore = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile();
        GameInformation.opponentEndScore = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getNumOfCardsInPile();
        int gameState = GameInformation.setGameResult();

        //  load the end game screen.
        StartCoroutine(waitToLoadEndGameScreen());
    }




    //  manage out of cards situations

    void outOfCardsHandler() {
        Deck plDeck = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>();
        Deck opDeck = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>();

        WinPile plWinPile = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>();
        WinPile opWinPile = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>();

        bool endGame = false;

        //  player ran out of cards;    opponent still has cards left
        if(plDeck.getNumOfCardsInDeck() == 0) {
            endGame = true;

            while(opDeck.getNumOfCardsInDeck() > 0) {
                var temp = opDeck.takeCardInDeck();

                opWinPile.addCardToPile(temp);
                FindObjectOfType<CardMovement>().moveCardObjectToOpponentWinPile(temp);
            }
        }

        //  opponent ran out of cards;  player still has cards left
        else if(opDeck.getNumOfCardsInDeck() == 0) {
            endGame = true;

            while(plDeck.getNumOfCardsInDeck() > 0) {
                var temp = plDeck.takeCardInDeck();
                plWinPile.addCardToPile(temp);
                FindObjectOfType<CardMovement>().moveCardObjectToPlayerWinPile(temp);
            }
        }


        if(endGame) {
            resolveGame();
        }
    }


    //  modify cheat charges based on the outcome of the battle

    void modifyCheatValsPlayerWins() {
        var playerCH = GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>();
        if(playerCH != null)
            playerCH.addWinChargeAmount();

        var opponentCH = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>();
        if(opponentCH != null)
            opponentCH.addLoseChargeAmount();
    }

    void modifyCheatValsOpponentWins() {
        var playerCH = GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>();
        if(playerCH != null)
            playerCH.addLoseChargeAmount();

        var opponentCH = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>();
        if(opponentCH != null)
            opponentCH.addWinChargeAmount();
    }

    void modifyCheatValsDraw() {
        var playerCH = GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>();
        if(playerCH != null)
            playerCH.addLoseChargeAmount();

        var opponentCH = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>();
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

    IEnumerator waitToLoadEndGameScreen() {
        yield return new WaitForSeconds(0.5f);

        FindObjectOfType<GameStateHandler>().loadEndGameScreen();
    }
}
