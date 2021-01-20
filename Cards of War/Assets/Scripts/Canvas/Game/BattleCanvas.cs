using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BattleCanvas : MonoBehaviour {
    [SerializeField] TextMeshProUGUI playerCardValueModText, opponentCardValueModText;
    float cardValueModOffset = 0.65f;
    float cardValueModSpeed = 0.05f;
    Color shownColor, hiddenColor;

    CardBattleMechanics cbm;
    CardMovement cm;

    private void Awake() {
        DOTween.Init();

        playerCardValueModText.color = hiddenColor;
        opponentCardValueModText.color = hiddenColor;

        shownColor = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
        hiddenColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0.0f);

        cbm = FindObjectOfType<CardBattleMechanics>();
        cm = FindObjectOfType<CardMovement>();
    }


    private void Update() {
        cardValueModHandler();
    }


    void cardValueModHandler() {
        int playerCardValueMod = cbm.getPlayerCardValueMod() + cbm.getTempPlayerCardValueMod();
        int opponentCardValueMod = cbm.getOpponentCardValueMod() + cbm.getTempOpponentCardValueMod();

        //  enables and disables the player's card value mod
        if(playerCardValueMod > 0 && (cbm.getPlayerPlayedCard() != null || cm.getPlayerHeldCardObject() != null))
            playerCardValueModText.color = shownColor;
        else
            playerCardValueModText.color = hiddenColor;

        //  enables and disables the opponent's card value mod
        if(opponentCardValueMod > 0 && (cbm.getOpponentPlayedCard() != null || cm.getOpponentHeldCardObject() != null))
            opponentCardValueModText.color = shownColor;
        else
            opponentCardValueModText.color = hiddenColor;


        //  sets the text for the player's card value mod
        if(playerCardValueModText.color == shownColor) {
            playerCardValueModText.text = "+" + playerCardValueMod.ToString();
        }

        //  sets the text for the opponent's card value mod
        if(opponentCardValueModText.color == shownColor) {
            opponentCardValueModText.text = "+" + opponentCardValueMod.ToString();
        }


        //  moves the player's card value mod to the active card
        if(cbm.getPlayerPlayedCard() != null || cm.getPlayerHeldCardObject() != null) {
            int numOfDigits = 0;
            if(cbm.getPlayerCardValueMod() + cbm.getTempPlayerCardValueMod() > 99)
                numOfDigits = 3;
            else if(cbm.getPlayerCardValueMod() + cbm.getTempPlayerCardValueMod() > 9)
                numOfDigits = 2;
            else
                numOfDigits = 1;

            var playerTarget = Vector3.zero;
            if(cbm.getPlayerPlayedCard() != null) {

                playerTarget = cbm.getPlayerPlayedCard().transform.position + new Vector3(cardValueModOffset + ((cardValueModOffset / 5.0f) * (numOfDigits - 1)), 0.0f, 0.0f);
            }
            else if(cm.getPlayerHeldCardObject() != null)
                playerTarget = cm.getPlayerHeldCardObject().transform.position + new Vector3(cardValueModOffset + ((cardValueModOffset / 5.0f) * (numOfDigits - 1)), 0.0f, 0.0f);

            playerCardValueModText.transform.DOMove(playerTarget, cardValueModSpeed);
        }
        //  moves the player's card value mod to the deck if no card is in play
        else {
            var playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getDeckPos();
            playerCardValueModText.transform.position = playerTarget;
        }


        //  moves the opponent's card value mod to the active card
        if(cbm.getOpponentPlayedCard() != null || cm.getOpponentHeldCardObject() != null) {
            int numOfDigits = 0;
            if(cbm.getOpponentCardValueMod() + cbm.getTempOpponentCardValueMod() > 99)
                numOfDigits = 3;
            else if(cbm.getOpponentCardValueMod() + cbm.getTempOpponentCardValueMod() > 9)
                numOfDigits = 2;
            else
                numOfDigits = 1;

            var opponentTarget = Vector3.zero;
            if(cbm.getOpponentPlayedCard() != null)
                opponentTarget = cbm.getOpponentPlayedCard().transform.position + new Vector3(cardValueModOffset + ((cardValueModOffset / 8.0f) * (numOfDigits - 1)), 0.0f, 0.0f);
            else if(cm.getOpponentHeldCardObject() != null)
                opponentTarget = cm.getOpponentHeldCardObject().transform.position + new Vector3(cardValueModOffset + ((cardValueModOffset / 8.0f) * (numOfDigits - 1)), 0.0f, 0.0f);

            opponentCardValueModText.transform.DOMove(opponentTarget, cardValueModSpeed);
        }
        //  moves the opponent's card value mod to the deck if no card is in play
        else {
            var opponentTarget = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getDeckPos();
            opponentCardValueModText.transform.position = opponentTarget;
        }
    }
}
