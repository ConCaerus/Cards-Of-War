using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalValueCheat : Cheat {
    public override float getChargeAmount() {
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
        }
    }


    //  can be used at all times
    public override bool canBeUsed() {
        return true;
    }



    //  adds the script to the player
    public override void addToPlayer() {
        GameObject.FindGameObjectWithTag("Player").AddComponent<AdditionalValueCheat>();
    }
}
