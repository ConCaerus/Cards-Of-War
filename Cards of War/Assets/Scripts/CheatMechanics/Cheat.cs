using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cheat : MonoBehaviour {
    public abstract float getChargeAmount();
    public abstract string getName();

    public abstract void use();

    public void setAsPlayerCheat() {
        GameInformation.playerCheatIndex = FindObjectOfType<CheatIndex>().getCheatIndexOfType(this);
    }

    public abstract bool canBeUsed();
}
