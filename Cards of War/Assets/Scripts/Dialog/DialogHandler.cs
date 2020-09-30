using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHandler : MonoBehaviour {
    [SerializeField] Dialog dialog;


    public void startIntroDialog() {
        FindObjectOfType<CharacterCanvas>().startDialog(getIntroDialog());
    }

    public void startBattleDialog() {
        FindObjectOfType<CharacterCanvas>().startDialog(getBattleDialog());
    }

    public void startCheatDialog() {
        FindObjectOfType<CharacterCanvas>().startDialog(getCheatDialog());
    }

    public void startWinDialog() {
        FindObjectOfType<CharacterCanvas>().startDialog(getWinDialog());
    }

    public void startLoseDialog() {
        FindObjectOfType<CharacterCanvas>().startDialog(getLoseDialog());
    }




    string getIntroDialog() {
        if(dialog != null) {
            var rand = Random.Range(0, dialog.introDialog.Count);

            return dialog.introDialog[rand];
        }

        return null;
    }

    string getBattleDialog() {
        if(dialog != null) {
            var rand = Random.Range(0, dialog.battleDialog.Count);

            return dialog.battleDialog[rand];
        }

        return null;
    }

    string getCheatDialog() {
        if(dialog != null) {
            var rand = Random.Range(0, dialog.cheatDialog.Count);

            return dialog.cheatDialog[rand];
        }

        return null;
    }

    string getWinDialog() {
        if(dialog != null) {
            var rand = Random.Range(0, dialog.winDialog.Count);

            return dialog.winDialog[rand];
        }

        return null;
    }

    string getLoseDialog() {
        if(dialog != null) {
            var rand = Random.Range(0, dialog.loseDialog.Count);

            return dialog.loseDialog[rand];
        }

        return null;
    }
}
