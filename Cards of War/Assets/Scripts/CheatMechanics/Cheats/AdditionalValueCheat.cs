using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalValueCheat : Cheat {
    public GameObject button;
    GameObject buttonInstance;

    Coroutine waitToPressButtonWaiter = null;


    private void Start() {
        if(gameObject.tag == "Opponent" && GetComponent<OpponentAI>() == null)
            gameObject.AddComponent<DefaultOpponentAI>();

        if(button == null) {
            button = FindObjectOfType<CheatIndex>().gameObject.GetComponent<AdditionalValueCheat>().button;
        }

        if(getInUse())
            createButton();
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
            if(Input.GetMouseButtonDown(0) && buttonInstance.GetComponent<AdditionalValueButton>().isMouseOver()) {
                FindObjectOfType<CardBattleMechanics>().setPlayerCardValueMod(FindObjectOfType<CardBattleMechanics>().getPlayerCardValueMod() + 1);

                setChargeAmount(0.0f);
            }
        }

        //  opponent used cheat
        else if(gameObject.tag == "Opponent" && waitToPressButtonWaiter == null) {
            waitToPressButtonWaiter = StartCoroutine(waitToPressButton());
        }
    }


    public override void showCanUse() {
        buttonInstance.GetComponent<AdditionalValueButton>().show();
    }

    public override void hideCanUse() {
        buttonInstance.GetComponent<AdditionalValueButton>().hide();
    }


    void createButton() {
        buttonInstance = Instantiate(button);
        buttonInstance.transform.SetParent(transform);
        buttonInstance.GetComponent<AdditionalValueButton>().forceHide();
    }


    public override bool useCondition() {
        if(!getCharged())
            return false;
        return chargeAmount >= filledChargeAmount;
    }


    IEnumerator waitToPressButton() {
        yield return new WaitForSeconds(0.25f);
        FindObjectOfType<CardBattleMechanics>().setOpponentCardValueMod(FindObjectOfType<CardBattleMechanics>().getOpponentCardValueMod() + 1);

        setChargeAmount(0.0f);
        waitToPressButtonWaiter = null;
    }
}
