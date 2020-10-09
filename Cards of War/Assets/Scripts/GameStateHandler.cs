using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour {
    private void Start() {
        //FindObjectOfType<CheatSelectCanvas>().showBackground();

        dealCards();
        //startOpponentDialog();
        setCheats();
    }

    public void dealCards() {
        FindObjectOfType<MasterDeck>().populateDecks();
    }

    public void startOpponentDialog() {
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startIntroDialog();
    }


    public void setCheats() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CheatHandler>().setPlayerCheatToPlayerCheat();
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<CheatHandler>().setOpponentCheat();
    }


    //  end game

    public void loadEndGameScreen() {
        SceneManager.LoadSceneAsync("EndGameScreen");
    }
}
