using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveCheatOpponentAI : OpponentAI {
    public override bool playCardLogic() {
        return !cardInPlay();
    }

    public override bool useCheatLogic() {
        return GetComponent<Cheat>().getCharged() && GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile() > 0;
    }
}
