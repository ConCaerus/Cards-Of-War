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



    //  delete this when you feel comfortable
    public void cheatHasBeenSelected(Cheat selectedCheat) {
        //  hides the cheat select canvas
        FindObjectOfType<CheatSelectCanvas>().hideBackground();

        //  adds the cheat to the player object
        selectedCheat.addToPlayer();
        foreach(var i in FindObjectsOfType<CheatHandler>())
            i.setCheat();


        //  deals cards
        FindObjectOfType<MasterDeck>().populateDecks();

        //  opponent starts intro dialog
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startIntroDialog();

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
