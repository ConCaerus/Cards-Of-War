using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCanvas : MonoBehaviour {
    [SerializeField] TextMeshProUGUI playerScore, opponentScore;

    WinPile playerWinPile, opponentWinPile;

    private void Awake() {
        playerWinPile = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>();
        opponentWinPile = GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>();
    }

    private void Update() {
        setPlayerScore();
        setOpponentScore();
    }

    void setPlayerScore() {
        playerScore.text = playerWinPile.getNumOfCardsInPile().ToString();
    }

    void setOpponentScore() {
        opponentScore.text = opponentWinPile.getNumOfCardsInPile().ToString();
    }
}
