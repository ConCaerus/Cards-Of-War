using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour {
    private void Start() {
        FindObjectOfType<CheatSelectCanvas>().showBackground();
    }




    public void cheatHasBeenSelected(Cheat selectedCheat) {
        //  hids the cheat select canvas
        FindObjectOfType<CheatSelectCanvas>().hideBackground();

        //  adds the cheat to the player object
        selectedCheat.addToPlayer();
        foreach(var i in FindObjectsOfType<CheatHandler>())
            i.setCheat();


        //  deals cards
        FindObjectOfType<MasterDeck>().populateDecks();
        foreach(var i in FindObjectsOfType<Deck>())
            StartCoroutine(i.waitToStartPlaying());

        //  opponent starts intro dialog
        GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().startIntroDialog();

    }
}
