﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultOpponentAI : OpponentAI {


    public override bool playCardLogic() {
        return !cardInPlay();
    }

    public override bool useCheatLogic() {
        return GetComponent<Cheat>() != null && GetComponent<Cheat>().getCharged();
    }
}
