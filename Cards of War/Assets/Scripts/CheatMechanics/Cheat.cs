using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cheat : MonoBehaviour {
    public float chargeAmount;
    public const float filledChargeAmount = 10.0f;
    bool inUse;

    public Color cheatColor;

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

        if(gameObject.tag == "Player" || gameObject.tag == "Opponent")
            inUse = true;
        else 
            inUse = false;


        if(cheatColor == new Color(0.0f, 0.0f, 0.0f, 0.0f)) {
            foreach(var i in FindObjectsOfType<Cheat>()) {
                if(i.getName() == this.getName() && i.cheatColor != new Color(0.0f, 0.0f, 0.0f, 0.0f)) {
                    cheatColor = i.cheatColor;
                    break;
                }
            }
        }
    }

    private void Update() {
        if(inUse) {
            if(getCharged())
                showCanUse();
            else 
                hideCanUse();
            if(useCondition())
                use();
        }
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


    public bool getInUse() {
        return inUse;
    }


    public void setChargeAmount(float f) {
        chargeAmount = f;
        roundChargeAmount();
    }
    public void addWinChargeAmount() {
        chargeAmount += getChargeWinAmount();
        roundChargeAmount();
    }
    public void addLoseChargeAmount() {
        chargeAmount += getChargeWinAmount() / 2;
        roundChargeAmount();
    }


    void roundChargeAmount() {
        //  dont fucking question this, please leave it
        if(gameObject.tag == "Player")
            FindObjectOfType<CheatCanvas>().animate = true;
        for(int i = 1; i <= 10; i++) {
            //  charge amount needs to be rounded
            if(Mathf.Abs(chargeAmount - (i / filledChargeAmount)) < 0.005f) {
                chargeAmount = (i / filledChargeAmount);
                return;
            }

            //  charge amount is already greater than the value and no longer needs to repeat
            else if(chargeAmount < (i / filledChargeAmount)) 
                return;
        }
    }
}
