using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cheat : MonoBehaviour {
    public abstract float getChargeAmount();

    public abstract void use();

    public abstract void addToPlayer();

    public abstract bool canBeUsed();
}
