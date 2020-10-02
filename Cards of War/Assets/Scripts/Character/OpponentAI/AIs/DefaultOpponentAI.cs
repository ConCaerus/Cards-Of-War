using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultOpponentAI : OpponentAI {

    private void Update() {
        if(!cardInPlay())
            playCard();
    }
}
