using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInformation {
    //  -1 = game not finished, 0 = player won, 1 = opponent won, 2 = draw
    public static int gameResult = -1;

    public static int playerEndScore = -1;
    public static int opponentEndScore = -1;

    //  NOTE: I must've been high or some shit when i wrote the logic behind this one varible
    //          I'm so fucking sorry Connor.
    //  used to find cheats in the cheatIndex script
    public static int playerCheatIndex = -1;


    public static int setGameResult() {
        if(playerEndScore > opponentEndScore)
            gameResult = 0;
        else if(opponentEndScore > playerEndScore)
            gameResult = 1;
        else 
            gameResult = 2;

        return gameResult;
    }



    public static bool advanceBattle() {
        return Input.GetMouseButtonDown(1);
    }
}
