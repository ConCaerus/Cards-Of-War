using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckCanvas : MonoBehaviour {
    [SerializeField] TextMeshProUGUI playerCardCount, opponentCardCount;


    private void Update() {
        positionTexts();
        setCardCountText();
    }

    void setCardCountText() {
        playerCardCount.text = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().getNumOfCardsInDeck().ToString();
        opponentCardCount.text = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().getNumOfCardsInDeck().ToString();
    }


    void positionTexts() {
        playerCardCount.transform.position = FindObjectOfType<Table>().getPlayerDeckCountPos();
        opponentCardCount.transform.position = FindObjectOfType<Table>().getOpponentDeckCountPos();
    }
}
