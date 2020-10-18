using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StealCheatOpponentAI : OpponentAI {
    const int valToStealOther = 8, valToStealOwn = 10;
    bool playCheat = false;

    public override bool playCardLogic() {
        return !cardInPlay() && !GetComponent<Cheat>().getCharged();
    }

    public override bool useCheatLogic() {
        if(!playCheat && GetComponent<Cheat>() != null && GetComponent<Cheat>().getCharged() && FindObjectOfType<CardMovement>().getOpponentHeldCardObject() == null && FindObjectOfType<CardBattleMechanics>().getOpponentPlayedCard() == null)
            StartCoroutine(waitToPlayCheat());
        else if(playCheat) {
            playCheat = false;
            return true;
        }

        return false;
    }


    public StealCheat.OpponentStealOption chooseWhatToSteal() {
        int playerVal = 0, opponentVal = 0;
        
        if(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getNumOfCardsInPile() > 0)
            playerVal = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().getTopCardInPile().GetComponent<CardObject>().getCard().value;
        if(GetComponentInChildren<WinPile>().getNumOfCardsInPile() > 0)
            opponentVal = GetComponentInChildren<WinPile>().getTopCardInPile().GetComponent<CardObject>().getCard().value;

        //  playerVal is enough and opponentVal is not
        if(playerVal >= valToStealOther && opponentVal < valToStealOwn)
            return StealCheat.OpponentStealOption.playFromOther;

        //  player Val is not enough and opponentVal is
        else if(playerVal < valToStealOther && opponentVal >= valToStealOwn)
            return StealCheat.OpponentStealOption.playFromOwn;

        //  both playerVal and opponentVal are enough
        else if(playerVal >= valToStealOther && opponentVal >= valToStealOwn) {
            //  larger the number, more the playerVal is greater than opponentVal
            int diff = playerVal - opponentVal;

            if(diff >= 0)
                return StealCheat.OpponentStealOption.playFromOther;
            else
                return StealCheat.OpponentStealOption.playFromOwn;
        }

        return StealCheat.OpponentStealOption.addToWinPile;
    }

    IEnumerator waitToPlayCheat() {
        yield return new WaitForSeconds(0.5f);

        playCheat = true;
    }
}
