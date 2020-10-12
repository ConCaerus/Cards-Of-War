using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour {
    private void Start() {
        //FindObjectOfType<CheatSelectCanvas>().showBackground();

        dealCards();
        //startOpponentDialog();
    }

    public void dealCards() {
        FindObjectOfType<MasterDeck>().populateDecks();
    }

    public void startOpponentDialog() {
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startIntroDialog();
    }


    //  end game

    public void loadEndGameScreen() {
        SceneManager.LoadSceneAsync("EndGameScreen");
    }
}
