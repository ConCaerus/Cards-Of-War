using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalValueCheat : Cheat {
    private void Start() {
        if(gameObject.tag == "Opponent" && GetComponent<OpponentAI>() == null)
            gameObject.AddComponent<DefaultOpponentAI>();
    }

    public override float getChargeWinAmount() {
        return 2.0f;
    }

    public override string getName() {
        return "Add Value Cheat";
    }

    //  This cheat adds one value to the value of the played card 
    public override void use() {
        //  player used cheat
        if(gameObject.tag == "Player") {
            FindObjectOfType<CardBattleMechanics>().setPlayerCardValueMod(FindObjectOfType<CardBattleMechanics>().getPlayerCardValueMod() + 1);
        }

        //  opponent used cheat
        else if(gameObject.tag == "Opponent") {
            FindObjectOfType<CardBattleMechanics>().setOpponentCardValueMod(FindObjectOfType<CardBattleMechanics>().getOpponentCardValueMod() + 1);
            GetComponent<DialogHandler>().startCheatDialog();
        }

        setChargeAmount(0.0f);
    }


    public override void showCanUse() {
        //  does nothing
    }

    public override void hideCanUse() {
        //  does nothing
    }


    public override bool useCondition() {
        if(!getCharged())
            return false;
        return chargeAmount >= filledChargeAmount;
    }
}
