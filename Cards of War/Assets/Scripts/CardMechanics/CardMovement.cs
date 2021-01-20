using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardMovement : MonoBehaviour {
    Vector2 recentOpponentPlayPos;
    GameObject playerHeldCardObjectOrigin;
    GameObject playerHeldCardObject, opponentHeldCardObject;
    [SerializeField] Material cardMaterial;
    public Material normalCardMaterial;


    private void Awake() {
        DOTween.Init();
    }

    private void LateUpdate() {
        moveHeldCardObjects();


        //  stop showing opponent shadow
        if(opponentHeldCardObjectDoneMovingToPlayPos())
            sendOpponentCardObject();
    }



    //  Direct movers

    //  move to decks

    float allowedError = 0.05f, avgTime = 0.75f;
    public void moveCardObjectToPlayerDeckPos(GameObject ob) {
        if(ob != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-allowedError, allowedError);
            randPos.y = Random.Range(-allowedError, allowedError);

            ob.transform.DOComplete();
            ob.transform.DOMove(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getDeckPos() + randPos, randTime, false);
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
            ob.transform.DOMove(GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getDeckPos() + randPos, randTime, false);
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
            ob.transform.DOMove(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getWinPilePos() + randPos, randTime, false);

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
            ob.transform.DOMove(GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getWinPilePos() + randPos, randTime, false);

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

    public void movePlayerCardObjectToOrigin() {
        if(playerHeldCardObjectOrigin != null && playerHeldCardObject != null) {
            float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            playerHeldCardObject.transform.DOComplete();
            playerHeldCardObject.transform.DOMove(playerHeldCardObjectOrigin.transform.position, randTime, false);
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
            a.transform.DOMove(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getWinPilePos() + randPos, randTime, false);
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
            a.transform.DOMove(GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getWinPilePos() + randPos, randTime, false);

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
            p.transform.DOMove(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getWinPilePos() + randPos, randTime, false);

            //  moves the resolved cards over to the win pile script
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().addCardToPile(p);

            if(o != null)
                moveResolvedCardObjectsOpponentWins(o);
        }
    }


    //  move to play pos

    public void moveOpponentHeldCardToPlayPos() {
        if(opponentHeldCardObject != null) {
            float opponentHeldCardAllowedError = 0.25f;
            float randTime = 0.5f; //   set to the desired norm time;

            //  don't use opponent allowed error here, possibility for instant movement
            randTime = Random.Range(randTime - allowedError, randTime + allowedError);

            Vector2 randPos;
            randPos.x = Random.Range(-opponentHeldCardAllowedError, opponentHeldCardAllowedError);
            randPos.y = Random.Range(-opponentHeldCardAllowedError, opponentHeldCardAllowedError);
            recentOpponentPlayPos = (Vector2)FindObjectOfType<Table>().gameObject.transform.position + new Vector2(0.0f, 1.5f) + randPos;

            opponentHeldCardObject.transform.DOComplete();
            opponentHeldCardObject.transform.DOMove(recentOpponentPlayPos, randTime, false);
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
                var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                playerHeldCardObject.transform.position = new Vector3(target.x, target.y, playerHeldCardObject.transform.position.z);
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
                        playerHeldCardObject.GetComponent<SpriteRenderer>().material = normalCardMaterial;
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

                    //  player is a fucking idiot
                    else {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().setChargeAmount(GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().getFilledChargeAmount());
                        movePlayerCardObjectToOrigin();
                        playerHeldCardObject.GetComponent<ObjectShadow>().hideShadow();

                        if(playerHeldCardObjectOrigin.GetComponent<WinPile>() != null)
                            playerHeldCardObjectOrigin.GetComponent<WinPile>().addCardToPile(playerHeldCardObject);
                        else if(playerHeldCardObjectOrigin.GetComponent<Deck>() != null)
                            playerHeldCardObjectOrigin.GetComponent<Deck>().addCardToDeck(playerHeldCardObject);
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
        playerHeldCardObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        playerHeldCardObject.GetComponent<SpriteRenderer>().material = cardMaterial;
    }

    public void setPlayerHeldCardObjectOrigin(GameObject o) {
        playerHeldCardObjectOrigin = o;
    }

    public void setOpponentHeldCardObject(GameObject card) {
        opponentHeldCardObject = card;
        opponentHeldCardObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        opponentHeldCardObject.GetComponent<SpriteRenderer>().material = cardMaterial;
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
