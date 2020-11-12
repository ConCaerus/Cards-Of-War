using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour {
    private void Start() {
        StartCoroutine(startGameWaiters());

        //startOpponentDialog();
    }


    //  start game

    public void startOpponentDialog() {
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startIntroDialog();
    }


    IEnumerator startGameWaiters(int gameState = 0) {
        switch(gameState) {
            //  show table
            case 0:
                yield return new WaitForSeconds(0.25f);
                FindObjectOfType<Table>().showTable();
                break;

            //  deal cards
            case 1: 
                yield return new WaitForSeconds(0.35f);
                FindObjectOfType<MasterDeck>().populateDecks();
                break;
            
            //  show opponent character
            case 2:
                yield return new WaitForSeconds(0.25f);
                FindObjectOfType<OpponentCharacterCanvas>().showCharacter();
                break;
        }

        if(gameState < 2)
            StartCoroutine(startGameWaiters(++gameState));
    }


    //  end game

    public void loadEndGameScreen() {
        SceneManager.LoadSceneAsync("EndGameScreen");
    }
}
