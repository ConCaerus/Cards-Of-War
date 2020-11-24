using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour {
    Transition startTrans;


    private void Start() {
        startTrans = gameObject.AddComponent<Transition>();
        startTrans.setStageOrder(new Transition.events[] {  Transition.events.table,    Transition.events.crowd,    Transition.events.cards,    Transition.events.opponent});
        startTrans.setTimes     (new float[] {              0.15f,                      0.15f,                       0.15f,                      0.25f});
        startTrans.start();

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
                FindObjectOfType<CharacterPicture>().showCharacterPicture();
                break;
        }

        if(gameState < 2)
            StartCoroutine(startGameWaiters(++gameState));
    }


    //  end game

    public void loadEndGameScreen() {
        StartCoroutine(LevelLoader.waitToLoadLevel("EndGameScreen", FindObjectOfType<SceneTransitionCanvas>()));
    }
}
