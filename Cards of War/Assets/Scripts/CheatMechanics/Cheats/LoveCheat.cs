using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveCheat : Cheat {
    [SerializeField] GameObject loveCheatCanvasPreset;
    LoveCheatCanvas loveCheatCanvas;

    public enum LoveOption {
        bestOption =    0,
        goodOption =    1, 
        badOption =     2,
        worstOption =   3
    }

    private void Start() {
        if(FindObjectOfType<LoveCheatCanvas>() == null) {
            var canvas = Instantiate(loveCheatCanvasPreset);
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;

            loveCheatCanvas = canvas.GetComponent<LoveCheatCanvas>();
        }
        else 
            loveCheatCanvas = FindObjectOfType<LoveCheatCanvas>();
    }

    public override float getChargeWinAmount() {
        return 5.0f;
    }

    public override string getName() {
        return "Love Cheat";
    }


    public override void use() {
        loveCheatCanvas.showPhone();
    }


    public override void showCanUse() {
        //  show the player's phone vibrating with a message from opponent character
        //  the message could be like "You look beautiful during this game"
        //  the player will then have to click on the phone and be given reply options
    }

    public override void hideCanUse() {
        loveCheatCanvas.hidePhone();
    }


    public override bool useCondition() {
        return getCharged();
    }


    public void chooseOption(LoveOption option) {
        switch(option) {
            case LoveOption.bestOption:
                StartCoroutine(getCards(4));
                break;

            case LoveOption.goodOption:
                StartCoroutine(getCards(2));
                break;

            case LoveOption.badOption:
                StartCoroutine(loseCards(2));
                break;

            case LoveOption.worstOption:
                StartCoroutine(loseCards(4));
                break;

            default:
                Debug.Log("Love cheat did not receive a valid love option");
                return;
        }

        setChargeAmount(0.0f);
    }

    IEnumerator getCards(int amount) {
        for(int i = 0; i < amount; i++) {
            if(gameObject.tag == "Player") {
                if(GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getNumOfCardsInPile() <= 0)
                    break;

                var card = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().takeCardFromPile();
                FindObjectOfType<CardMovement>().moveCardObjectToPlayerWinPile(card);
                GetComponentInChildren<WinPile>().addCardToPile(card);
            }

            else if(gameObject.tag == "Opponent") {
                if(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile() <= 0)
                    break;

                var card = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().takeCardFromPile();
                FindObjectOfType<CardMovement>().moveCardObjectToOpponentWinPile(card);
                GetComponentInChildren<WinPile>().addCardToPile(card);
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator loseCards(int amount) {
        for(int i = 0; i < amount; i++) {
            if(gameObject.tag == "Player") {
                if(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile() <= 0)
                    break;

                var card = GetComponentInChildren<WinPile>().takeCardFromPile();
                FindObjectOfType<CardMovement>().moveCardObjectToOpponentWinPile(card);
                GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().addCardToPile(card);
            }

            else if(gameObject.tag == "Opponent") {
                if(GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().getNumOfCardsInPile() <= 0)
                    break;

                var card = GetComponentInChildren<WinPile>().takeCardFromPile();
                FindObjectOfType<CardMovement>().moveCardObjectToPlayerWinPile(card);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().addCardToPile(card);
            }

            yield return new WaitForSeconds(0.15f);
        }
    }
}
