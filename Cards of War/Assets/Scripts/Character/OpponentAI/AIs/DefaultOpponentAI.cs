using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultOpponentAI : OpponentAI {

    private void Update() {
        //  play card logic
        if(!cardInPlay())
            playCard();

        var cheat = GetComponent<Cheat>();
        if(cheat.canBeUsed() && cheat.useCondition()) {
            cheat.use();
        }
    }
}
