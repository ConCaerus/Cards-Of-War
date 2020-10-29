using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardMovement : MonoBehaviour {
    Vector2 playerDeckPos, opponentDeckPos;
    Vector2 playerWinPilePos, opponentWinPilePos;

    Vector2 opponentPlayPos;
    Vector2 recentOpponentPlayPos;
    GameObject playerHeldCardObjectOrigin;
    GameObject playerHeldCardObject, opponentHeldCardObject;


    private void Awake() {
        DOTween.Init();
    }

    private void Start() {
        //  makes sure that everything is in the right spot before using their positions

        playerDeckPos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getDeckPos();
        opponentDeckPos = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getDeckPos();

        playerWinPilePos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getWinPilePos();
        opponentWinPilePos = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getWinPilePos();

        float middleX = (playerDeckPos.x + opponentDeckPos.x) / 2.0f;
        float middleY = (opponentDeckPos.y + playerDeckPos.y) / 2.0f;
        opponentPlayPos = new Vector2(middleX, middleY + 0.75f);
    }

    private void LateUpdate() {
        moveHeldCardObjects();


        //  stop showing opponent shadow
        if(opponentHeldCardObjectDoneMovingToPlayPos())
            sendOpponentCardObject();
    }



    //  Direct movers

    //  move to decks

    float allowedError = 0.05f, avgTime = 0.5f;
    public void moveCardObjectToPlayerDeckPos(GameObject ob) {
        if(ob != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            ob.transform.DOComplete();
            ob.transform.DOMove(playerDeckPos + randPos, randTime, false);
        }
    }

    public void moveCardObjectToOpponentDeckPos(GameObject ob) {
        if(ob != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            ob.transform.DOComplete();
            ob.transform.DOMove(opponentDeckPos + randPos, randTime, false);
        }
    }


    //  move to win piles

    public void moveCardObjectToPlayerWinPile(GameObject ob) {
        if(ob != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            ob.transform.DOComplete();
            ob.transform.DOMove(playerWinPilePos + randPos, randTime, false);

            if(!ob.GetComponent<CardObject>().getShowingface())
                ob.GetComponent<CardObject>().showCardFace();
        }
    }

    public void moveCardObjectToOpponentWinPile(GameObject ob) {
        if(ob != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            ob.transform.DOComplete();
            ob.transform.DOMove(opponentWinPilePos + randPos, randTime, false);

            if(!ob.GetComponent<CardObject>().getShowingface())
                ob.GetComponent<CardObject>().showCardFace();
        }
    }


    //  move to (0, 0)
    //  usefull when trying to move to a deck pos but the card is a child of the deck
    public void moveCardObjectToZero(GameObject ob) {
        if(ob != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            ob.transform.DOComplete();
            ob.transform.DOMove(Vector2.zero + randPos, randTime, false);
        }
    }

    //  move resolved cards

    public void moveResolvedCardObjectsPlayerWins(GameObject a, GameObject b = null) {
        if(a != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            a.transform.DOComplete();
            a.transform.DOMove(playerWinPilePos + randPos, randTime, false);
            //  moves the resolved cards over to the win pile script
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().addCardToPile(a);

            if(b != null)
                moveResolvedCardObjectsPlayerWins(b);
        }
    }

    public void moveResolvedCardObjectsOpponentWins(GameObject a, GameObject b = null) {
        if(a != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            a.transform.DOComplete();
            a.transform.DOMove(opponentWinPilePos + randPos, randTime, false);

            //  moves the resolved cards over to the win pile script
            GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().addCardToPile(a);

            if(b != null)
                moveResolvedCardObjectsOpponentWins(b);
        }
    }

    public void moveResolvedCardObjectsDraw(GameObject p, GameObject o = null) {
        if(p != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            p.transform.DOComplete();
            p.transform.DOMove(playerWinPilePos + randPos, randTime, false);

            //  moves the resolved cards over to the win pile script
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().addCardToPile(p);

            if(o != null)
                moveResolvedCardObjectsOpponentWins(o);
        }
    }


    //  move to play pos

    public void moveOpponentHeldCardToPlayPos() {
        if(opponentHeldCardObject != null) {
            float opponentHeldCardAllowedError = 0.5f;
            float randTime = 0.5f; //   set to the desired norm time;

            //  don't use opponent allowed error here, possibility for instant movement
            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-opponentHeldCardAllowedError, opponentHeldCardAllowedError);
            randPos.y = Random.Range(-opponentHeldCardAllowedError, opponentHeldCardAllowedError);

            opponentHeldCardObject.transform.DOComplete();
            opponentHeldCardObject.transform.DOMove(opponentPlayPos + randPos, randTime, false);

            recentOpponentPlayPos = opponentPlayPos + randPos;
        }
    }

    //  move held cards
    //  meat of this entire fucking 300 lines of pain

    void moveHeldCardObjects() {
        //  player card code
        if(playerHeldCardObject != null) {
            bool overDeck = false, overWinPile = false;
            foreach(var i in FindObjectsOfType<Deck>()) {
                if(i.getMouseOverCollider())
                    overDeck = true;
            }
            foreach(var i in FindObjectsOfType<WinPile>()) {
                if(i.getMouseOver())
                    overWinPile = true;
            }

            //  card is still being held
            if(Input.GetMouseButton(0)) {
                playerHeldCardObject.transform.DOMove((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
            }

            //  player let of of the card and it is in play
            //  send it over to the battle mechanics script
            else if(!Input.GetMouseButton(0) && !overDeck && !overWinPile) {
                FindObjectOfType<CardBattleMechanics>().setPlayerPlayedCard(playerHeldCardObject);
                playerHeldCardObject.GetComponent<ObjectShadow>().hideShadow();
                playerHeldCardObject = null;
                playerHeldCardObjectOrigin = null;
            }


            //  player wants to put their card back in the origin
            else if(!Input.GetMouseButton(0) && playerHeldCardObjectOrigin != null) {
                //  origin is a deck
                if(playerHeldCardObjectOrigin.GetComponent<Deck>() != null) {
                    if(playerHeldCardObjectOrigin.GetComponent<Deck>().getMouseOverCollider()) {
                        playerHeldCardObjectOrigin.GetComponent<Deck>().addCardToDeck(playerHeldCardObject);
                        playerHeldCardObject.GetComponent<ObjectShadow>().hideShadow();
                        playerHeldCardObject = null;
                        playerHeldCardObjectOrigin = null;
                    }
                }

                //  origin is a winpile
                else if(playerHeldCardObjectOrigin.GetComponent<WinPile>() != null) {
                    WinPile other = null;
                    foreach(var i in FindObjectsOfType<WinPile>()) {
                        if(i != playerHeldCardObjectOrigin.GetComponent<WinPile>())
                            other = i;
                    }

                    //  player wants to put card back into origin
                    if(playerHeldCardObjectOrigin.GetComponent<WinPile>().getMouseOver()) {
                        playerHeldCardObjectOrigin.GetComponent<WinPile>().addCardToPile(playerHeldCardObject);
                        playerHeldCardObject.GetComponent<ObjectShadow>().hideShadow();

                        //  this because no other thing leads to the origin being a win pile
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().setChargeAmount(GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().getFilledChargeAmount());
                        playerHeldCardObject = null;
                        playerHeldCardObjectOrigin = null;
                    }

                    //  player wants to add card into other winPile
                    else if(other.getMouseOver()) {
                        playerHeldCardObject.GetComponent<ObjectShadow>().hideShadow();
                        other.addCardToPile(playerHeldCardObject);
                        playerHeldCardObject = null;
                        playerHeldCardObjectOrigin = null;
                    }
                }
                
                //  player is trying to add their card to a spot they can't
                //  two options here: add card back to deck, or keep card on mouse
                //                  I went with the deck one
                if(playerHeldCardObject != null) {
                    moveCardObjectToPlayerDeckPos(playerHeldCardObject);
                    playerHeldCardObjectOrigin.GetComponent<Deck>().addCardToDeck(playerHeldCardObject);
                    playerHeldCardObject.GetComponent<ObjectShadow>().destroyShadow();
                    playerHeldCardObject = null;
                    playerHeldCardObjectOrigin = null;
                }
            }
        }
    }

    void sendOpponentCardObject() {
        if(opponentHeldCardObject != null) {
            opponentHeldCardObject.GetComponent<ObjectShadow>().hideShadow();
            FindObjectOfType<CardBattleMechanics>().setOpponentPlayedCard(opponentHeldCardObject);
            opponentHeldCardObject = null;
        }
    }

    bool opponentHeldCardObjectDoneMovingToPlayPos() {
        if(opponentHeldCardObject != null)
            return (Vector2)opponentHeldCardObject.transform.position == recentOpponentPlayPos;
        return false;
    }


    public void stopMovingPlayerHeldCardObject() {
        if(playerHeldCardObject != null)
            playerHeldCardObject.transform.DOComplete();
    }

    public void stopMovingOpponentHeldCardObject() {
        if(opponentHeldCardObject != null)
            opponentHeldCardObject.transform.DOComplete();
    }


    //  setters
    
    public void setPlayerHeldCardObject(GameObject card) {
        playerHeldCardObject = card;
    }

    public void setPlayerHeldCardObjectOrigin(GameObject o) {
        playerHeldCardObjectOrigin = o;
    }

    public void setOpponentHeldCardObject(GameObject card) {
        opponentHeldCardObject = card;
    }



    //  getters
    
    public GameObject getPlayerHeldCardObject() {
        return playerHeldCardObject;
    }

    public GameObject getOpponentHeldCardObject() {
        return opponentHeldCardObject;
    }

    public Vector2 getRecentOpponentPlayPos() {
        return recentOpponentPlayPos;
    }
}
