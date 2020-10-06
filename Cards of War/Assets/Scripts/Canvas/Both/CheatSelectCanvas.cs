using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheatSelectCanvas : MonoBehaviour {
    [SerializeField] Image background;
    [SerializeField] GameObject[] cheats;

    float buffer = 3.0f;

    int selectedCheatIndex = 0;


    private void Awake() {
        turnList(true);
    }


    public void showBackground() {
        background.gameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f);
        turnList(true);
    }

    public void hideBackground() {
        background.gameObject.transform.DOScale(Vector3.zero, 0.5f);
    }


    //  rotates the cheats around to show the selected cheat
    void turnList(bool snap = false) {
        for(int i = 0; i < cheats.Length; i++) {
            int indexOffset = i - selectedCheatIndex;

            warpObjectBasedOnIndexOffset(cheats[i], indexOffset, snap);
        }
    }


    void warpObjectBasedOnIndexOffset(GameObject ob, int offset, bool snap = false) {
        float distBtw = buffer + ob.transform.lossyScale.x / 4.0f;
        var target = new Vector2(distBtw * offset, 0.0f);
        float time = 0.25f;

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



            //  restarts the whole thing without snapping
            turnList(false);
        }
    }


    //  buttons

    //  THIS FUNCTION ADD THE CHEAT TO THE PLAYER OBJECT FUCK YOU
    public void doneSelecting() {
        //  remove this line if you want to have some fun
        foreach(var i in GameObject.FindGameObjectWithTag("Player").GetComponents<Cheat>())
            i.enabled = false;
        cheats[selectedCheatIndex].GetComponent<Cheat>().addToPlayer();

        hideBackground();
    }

    public void moveListRight() {
        if(selectedCheatIndex < cheats.Length - 1)
            selectedCheatIndex++;
        else 
            selectedCheatIndex = 0;

        turnList();
    }

    public void moveListLeft() {
        if(selectedCheatIndex > 0)
            selectedCheatIndex--;
        else
            selectedCheatIndex = cheats.Length - 1;

        turnList();
    }
}