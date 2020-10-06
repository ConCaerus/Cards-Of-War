using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour {
    private void Start() {
        //FindObjectOfType<CheatSelectCanvas>().showBackground();

        dealCards();
        //startOpponentDialog();
        lockInCheats();
    }

    public void dealCards() {
        FindObjectOfType<MasterDeck>().populateDecks();
    }

    public void startOpponentDialog() {
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startIntroDialog();
    }


    public void lockInCheats() {
        foreach(var i in FindObjectsOfType<CheatHandler>())
            i.setCheat();
    }
}
