using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cheat : MonoBehaviour {
    public float chargeAmount;
    //  lowered a little cause float round off will fuck the player
    public const float filledChargeAmount = 9.999999f;

    private void Awake() {
        //  can add cheat to player
        if(gameObject.tag == "Player" && GameInformation.playerCheatIndex != -1) {
            //  this cheat isn't the correct cheat
            var chosenCheat = FindObjectOfType<CheatIndex>().getCheatFromIndex(GameInformation.playerCheatIndex);
            if(getName() != chosenCheat.getName()) {
                var temp = FindObjectOfType<CheatIndex>().addCheatToObject(gameObject, chosenCheat);
                temp.enabled = true;
                Destroy(this);
            }
        }
    }

    private void Update() {
        if(getCharged())
            showCanUse();
        else 
            hideCanUse();
        if(useCondition())
            use();
    }

    public abstract float getChargeWinAmount();
    public float getChargeAmount() {
        return chargeAmount;
    }
    public float getFilledChargeAmount() {
        return filledChargeAmount;
    }
    public bool getCharged() {
        return chargeAmount >= filledChargeAmount;
    }
    public abstract string getName();

    public abstract void use();
    public abstract void showCanUse();
    public abstract void hideCanUse();

    public void setAsPlayerCheat() {
        GameInformation.playerCheatIndex = FindObjectOfType<CheatIndex>().getCheatIndexOfType(this);
    }

    public abstract bool useCondition();


    public void setChargeAmount(float f) {
        chargeAmount = f;
    }
    public void addWinChargeAmount() {
        chargeAmount += getChargeWinAmount();
    }
    public void addLoseChargeAmount() {
        chargeAmount += getChargeWinAmount() / 2;
    }
}
