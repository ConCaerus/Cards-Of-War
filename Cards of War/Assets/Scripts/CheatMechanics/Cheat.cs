using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cheat : MonoBehaviour {
    public float chargeAmount;
    public const float filledChargeAmount = 10.0f;

    public abstract float getChargeWinAmount();
    public float getChargeAmount() {
        return chargeAmount;
    }
    public abstract string getName();

    public abstract void use();

    public void setAsPlayerCheat() {
        GameInformation.playerCheatIndex = FindObjectOfType<CheatIndex>().getCheatIndexOfType(this);
    }

    public abstract bool canBeUsed();
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
