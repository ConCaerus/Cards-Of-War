using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHandler : MonoBehaviour {
    float chargeAmount = 0.0f;

    Cheat activeCheat;


    private void Awake() {
        chargeAmount = 0.0f;
    }
    
    private void Update() {
        cheatUseHandler();
    }



    void cheatUseHandler() {
        if(gameObject.tag == "Player") {
            //  the cheat is charged and the player wants to use it
            if(Input.GetKeyDown(KeyCode.Space) && canUseCheat())
                useAndResetCharge();
        }
    }


    void useAndResetCharge() {
        chargeAmount = 0.0f;
        if(activeCheat != null)
            activeCheat.use();
    }

    bool canUseCheat() {
        bool temp = chargeAmount >= 10.0f &&
                    activeCheat.canBeUsed();

        return temp;
    }


    public void opponentCheatUseHandler() {
        if(gameObject.tag == "Opponent" && canUseCheat()) {
            useAndResetCharge();
            gameObject.GetComponent<DialogHandler>().startCheatDialog();
        }
    }


    //  setters

    public void addWinChargeAmount() {
        if(activeCheat != null)
            chargeAmount += activeCheat.getChargeAmount();
    }

    public void addLoseChargeAmount() {
        if(activeCheat != null)
            chargeAmount += activeCheat.getChargeAmount() / 2;
    }

    public void changeCheatChargeAmount(float val) {
        chargeAmount+=val;
    }

    public void setPlayerCheatToPlayerCheat() {
        if(gameObject.tag == "Player") {
            //  have fun fucking debugging this fucker of a line.
            activeCheat = FindObjectOfType<CheatIndex>().addCheatToObject(gameObject, FindObjectOfType<CheatIndex>().getCheatFromIndex(GameInformation.playerCheatIndex));
            Debug.Log(activeCheat);
        }
    }

    public void setOpponentCheat() {
        if(gameObject.tag == "Opponent")
            activeCheat = GetComponent<Cheat>();
    }


    //  getters

    public float getCheatChargeAmount() {
        return chargeAmount;
    }
}
