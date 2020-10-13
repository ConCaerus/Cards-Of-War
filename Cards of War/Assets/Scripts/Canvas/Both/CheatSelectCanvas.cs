using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CheatSelectCanvas : MonoBehaviour {
    [SerializeField] Image background;
    [SerializeField] GameObject[] cheats;

    [SerializeField] TextMeshProUGUI cheatNameText;

    GameObject canvasThatWasBlocked = null;

    const float buffer = 3.0f;
    const float showHideTime = 0.15f;

    int selectedCheatIndex = 0;


    private void Start() {
        turnList(true);
    }


    public void showBackground(GameObject thingThatWasBlocked = null) {
        background.gameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), showHideTime);
        canvasThatWasBlocked = thingThatWasBlocked;
        turnList(true);
    }

    public void hideBackground() {
        background.gameObject.transform.DOScale(new Vector3(0.0f, 1.0f, 0.0f), showHideTime);
        if(canvasThatWasBlocked != null)
            canvasThatWasBlocked.gameObject.SetActive(true);
        canvasThatWasBlocked = null;
    }


    //  rotates the cheats around to show the selected cheat
    void turnList(bool snap = false) {
        for(int i = 0; i < cheats.Length; i++) {
            int indexOffset = i - selectedCheatIndex;
            cheats[i].transform.DOComplete();

            warpObjectBasedOnIndexOffset(cheats[i], indexOffset, snap);

            if(indexOffset == 0)
                changeCheatNameText(cheats[i]);
        }
    }


    void warpObjectBasedOnIndexOffset(GameObject ob, int offset, bool snap = false) {
        float distBtw = buffer + ob.transform.lossyScale.x / 4.0f;
        var target = new Vector2(distBtw * offset, 0.0f);
        float time = 0.25f;
        ob.transform.DOComplete();

        if(!snap) {
            //  move
            ob.transform.DOMove(target, time);

            //  scale
            if(offset == 0)
                ob.transform.DOScale(new Vector2(2.0f, 2.0f), time);
            else
                ob.transform.DOScale(new Vector2(1.0f, 1.0f), time);
        }
        else {
            //  move
            ob.transform.position = target;

            //  scale
            if(offset == 0)
                ob.transform.DOScale(new Vector2(2.0f, 2.0f), 0.0f);
            else 
                ob.transform.DOScale(new Vector2(1.0f, 1.0f), 0.0f);



            //  restarts the whole thing without snapping becuase fuck you
            turnList(false);
        }
    }

    void changeCheatNameText(GameObject cheat) {
        string text = cheat.GetComponent<Cheat>().getName();
        cheatNameText.text = text;
    }

    void animateCheatNameText() {
        cheatNameText.transform.DOComplete();
        cheatNameText.transform.DOPunchPosition(new Vector3(0.0f, 15.0f, 0.0f), 0.25f);
        cheatNameText.transform.DOShakeRotation(0.25f, 30, 10, 45, false);
    }


    //  buttons

    public void doneSelecting() {
        cheats[selectedCheatIndex].GetComponent<Cheat>().setAsPlayerCheat();

        hideBackground();
    }

    public void moveListRight() {
        if(selectedCheatIndex < cheats.Length - 1)
            selectedCheatIndex++;
        else 
            selectedCheatIndex = 0;

        animateCheatNameText();
        turnList();
    }

    public void moveListLeft() {
        if(selectedCheatIndex > 0)
            selectedCheatIndex--;
        else
            selectedCheatIndex = cheats.Length - 1;

        animateCheatNameText();
        turnList();
    }
}