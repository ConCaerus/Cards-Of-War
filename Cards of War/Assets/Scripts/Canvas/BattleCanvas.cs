using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BattleCanvas : MonoBehaviour {
    [SerializeField] TextMeshProUGUI playerCardValueModText, opponentCardValueModText;
    float cardValueModOffset;

    CardBattleMechanics cbm;
    CardMovement cm;

    private void Awake() {
        DOTween.Init();

        playerCardValueModText.enabled = false;
        opponentCardValueModText.enabled = false;

        cardValueModOffset = 1.5f;

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
            playerCardValueModText.enabled = true;
        else
            playerCardValueModText.enabled = false;

        //  enables and disables the opponent's card value mod
        if(opponentCardValueMod > 0 && (cbm.getOpponentPlayedCard() != null || cm.getOpponentHeldCardObject() != null))
            opponentCardValueModText.enabled = true;
        else
            opponentCardValueModText.enabled = false;


        //  sets the text for the player's card value mod
        if(playerCardValueModText.enabled == true) {
            playerCardValueModText.text = "+" + playerCardValueMod.ToString();
        }

        //  sets the text for the opponent's card value mod
        if(opponentCardValueModText.enabled == true) {
            opponentCardValueModText.text = "+" + opponentCardValueMod.ToString();
        }


        //  moves the player's card value mod to the active card
        if(playerCardValueModText.enabled == true) {
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

            playerCardValueModText.transform.DOMove(playerTarget, 0.15f);
        }
        //  moves the player's card value mod to the deck if not enabled
        else {
            var playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getDeckPos();
            playerCardValueModText.transform.DOMove(playerTarget, 0.1f);
        }


        //  moves the opponent's card value mod to the active card
        if(opponentCardValueModText.enabled == true) {
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

            opponentCardValueModText.transform.DOMove(opponentTarget, 0.15f);
        }
        //  moves the opponent's card value mod to the deck if not enabled
        else {
            var opponentTarget = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getDeckPos();
            opponentCardValueModText.transform.DOMove(opponentTarget, 0.1f);
        }
    }
}
