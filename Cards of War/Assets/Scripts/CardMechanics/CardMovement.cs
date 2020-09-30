using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardMovement : MonoBehaviour {
    Vector2 playerDeckPos, opponentDeckPos;
    Vector2 playerWinPilePos, opponentWinPilePos;

    Vector2 opponentPlayPos = new Vector2(0.0f, 1.5f), playerPlayPos = new Vector2(0.0f, -1.5f);
    Vector2 recentOpponentPlayPos;
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
    }

    private void LateUpdate() {
        moveHeldCardObjects();


        //  stop showing opponent shadow
        if(opponentHeldCardObjectDoneMovingToPlayPos())
            sendOpponentCardObject();
            

        playerDeckPos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getDeckPos();
        opponentDeckPos = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getDeckPos();

        playerWinPilePos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getWinPilePos();
        opponentWinPilePos = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getWinPilePos();
    }



    //  Direct movers

    //  move to decks

    float allowedError = 0.05f, avgTime = 0.5f;
    public void moveCardObjectToPlayerDeckPos(GameObject ob) {
        float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-allowedError, allowedError);
        randPos.y = Random.Range(-allowedError, allowedError);

        DOTween.Kill(ob, true);
        ob.transform.DOMove(playerDeckPos + randPos, randTime, false);
    }

    public void moveCardObjectToOpponentDeckPos(GameObject ob) {
        float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-allowedError, allowedError);
        randPos.y = Random.Range(-allowedError, allowedError);

        DOTween.Kill(ob, true);
        ob.transform.DOMove(opponentDeckPos + randPos, randTime, false);
    }


    //  move to win piles

    public void moveCardObjectToPlayerWinPile(GameObject ob) {
        float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-allowedError, allowedError);
        randPos.y = Random.Range(-allowedError, allowedError);

        DOTween.Kill(ob, true);
        ob.transform.DOMove(playerWinPilePos + randPos, randTime, false);

        if(!ob.GetComponent<CardObject>().getShowingface())
            ob.GetComponent<CardObject>().showCardFace();
    }

    public void moveCardObjectToOpponentWinPile(GameObject ob) {
        float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-allowedError, allowedError);
        randPos.y = Random.Range(-allowedError, allowedError);

        DOTween.Kill(ob, true);
        ob.transform.DOMove(opponentWinPilePos + randPos, randTime, false);

        if(!ob.GetComponent<CardObject>().getShowingface())
            ob.GetComponent<CardObject>().showCardFace();
    }

    //  move resolved cards

    public void moveResolvedCardObjectsPlayerWins(GameObject a, GameObject b = null) {
        float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-allowedError, allowedError);
        randPos.y = Random.Range(-allowedError, allowedError);

        DOTween.Kill(a, true);
        a.transform.DOMove(playerWinPilePos + randPos, randTime, false);
        //  moves the resolved cards over to the win pile script
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().addCardToPile(a);

        if(b != null)
            moveResolvedCardObjectsPlayerWins(b);
    }

    public void moveResolvedCardObjectsOpponentWins(GameObject a, GameObject b = null) {
        float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-allowedError, allowedError);
        randPos.y = Random.Range(-allowedError, allowedError);

        DOTween.Kill(a, true);
        a.transform.DOMove(opponentWinPilePos + randPos, randTime, false);

        //  moves the resolved cards over to the win pile script
        GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().addCardToPile(a);

        if(b != null)
            moveResolvedCardObjectsOpponentWins(b);
    }

    public void moveResolvedCardObjectsDraw(GameObject p, GameObject o = null) {
        float randTime = Random.Range(avgTime - allowedError, avgTime + allowedError);

        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-allowedError, allowedError);
        randPos.y = Random.Range(-allowedError, allowedError);

        DOTween.Kill(p, true);
        p.transform.DOMove(playerWinPilePos + randPos, randTime, false);

        //  moves the resolved cards over to the win pile script
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().addCardToPile(p);

        if(o != null)
            moveResolvedCardObjectsOpponentWins(o);
    }


    //  move to play pos

    void moveOpponentHeldCardToPlayPos() {
        float opponentHeldCardAllowedError = 0.5f;
        float randTime = 0.5f; //   set to the desired norm time;

        //  don't use opponent allowed error here, possibility for instant movement
        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-opponentHeldCardAllowedError, opponentHeldCardAllowedError);
        randPos.y = Random.Range(-opponentHeldCardAllowedError, opponentHeldCardAllowedError);

        DOTween.Kill(opponentHeldCardObject, true);
        opponentHeldCardObject.transform.DOMove(opponentPlayPos + randPos, randTime, false);

        recentOpponentPlayPos = opponentPlayPos + randPos;
    }

    //  if I ever have a player auto play feature. (gave up cause onMouseDrag doesn't detect right clicks)
    void movePlayerHeldCardToPlayPos() {
        float playerHeldCardAllowedError = 0.5f;
        float randTime = 0.5f; //   set to the desired norm time;

        //  don't use player allowed error here, possibility for instant movement
        randTime = Random.Range(randTime - allowedError, randTime + allowedError);

        Vector2 randPos;
        randPos.x = Random.Range(-playerHeldCardAllowedError, playerHeldCardAllowedError);
        randPos.y = Random.Range(-playerHeldCardAllowedError, playerHeldCardAllowedError);

        DOTween.Kill(playerHeldCardObject, true);
        playerHeldCardObject.transform.DOMove(playerPlayPos + randPos, randTime, false);
    }

    //  move held cards

    void moveHeldCardObjects() {
        //  player card code
        if(playerHeldCardObject != null) {
            //  start showing card shadow
            playerHeldCardObject.GetComponent<CardObjectShadow>().showShadow();

            //  card is still being held
            if(Input.GetMouseButton(0)) {
                playerHeldCardObject.transform.DOMove((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
            }

            //  player let of of the card and it is in play
            //  send it over to the battle mechanics script
            else if(!Input.GetMouseButton(0) && !GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getMouseOverCollider()) {
                FindObjectOfType<CardBattleMechanics>().setPlayerPlayedCard(playerHeldCardObject);
                playerHeldCardObject.GetComponent<CardObjectShadow>().hideShadow();
                playerHeldCardObject = null;
            }

            //  player wants to put their card back in their deck
            else if(!Input.GetMouseButton(0) && GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getMouseOverCollider()) {
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().addCardToDeck(playerHeldCardObject);
                playerHeldCardObject.GetComponent<CardObjectShadow>().hideShadow();

                playerHeldCardObject = null;
            }
        }
    }

    public void moveOpponentCardObject() {
        //  start showing card shadow
        opponentHeldCardObject.GetComponent<CardObjectShadow>().showShadow();

        moveOpponentHeldCardToPlayPos();
    }

    void sendOpponentCardObject() {
        opponentHeldCardObject.GetComponent<CardObjectShadow>().hideShadow();
        FindObjectOfType<CardBattleMechanics>().setOpponentPlayedCard(opponentHeldCardObject);
        opponentHeldCardObject = null;
    }

    bool opponentHeldCardObjectDoneMovingToPlayPos() {
        if(opponentHeldCardObject != null)
            return (Vector2)opponentHeldCardObject.transform.position == recentOpponentPlayPos;
        return false;
    }


    public void stopMovingPlayerHeldCardObject() {
        if(playerHeldCardObject != null)
            DOTween.Kill(playerHeldCardObject, true);
    }

    public void stopMovingOpponentHeldCardObject() {
        if(opponentHeldCardObject != null)
            DOTween.Kill(opponentHeldCardObject, true);
    }


    //  setters
    
    public void setPlayerHeldCardObject(GameObject card) {
        playerHeldCardObject = card;
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
