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
        setScores();
        positionScores();
    }

    void setScores() {
        playerScore.text = playerWinPile.getNumOfCardsInPile().ToString();
        opponentScore.text = opponentWinPile.getNumOfCardsInPile().ToString();
    }

    void positionScores() {
        playerScore.transform.position = FindObjectOfType<Table>().getPlayerWinPileCountPos();
        opponentScore.transform.position = FindObjectOfType<Table>().getOpponentWinPileCountPos();
    }
}
