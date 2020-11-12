using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Table : MonoBehaviour {
    const float tableSpeed = 0.25f;
    Vector2 tableShowPos, tableHidePos;

    [SerializeField] GameObject pDeckPos, oDeckPos, pWinPilePos, oWinPilePos, pDeckCountPos, oDeckCountPos, pWinPileCountPos, oWinPileCountPos;


    private void Awake() {
        tableShowPos = transform.position;
        tableHidePos = tableShowPos - new Vector2(0.0f, 10.0f);

        forceHideTable();
    }


    public void showTable() {
        transform.DOComplete();
        transform.DOMove(tableShowPos, tableSpeed);
    }

    public void hideTable() {
        transform.DOComplete();
        transform.DOMove(tableHidePos, tableSpeed);
    }

    public void forceHideTable() {
        transform.DOComplete();
        transform.position = tableHidePos;
    }


    //  things
    public Vector2 getPlayerDeckPos() {
        return pDeckPos.transform.position;
    }

    public Vector2 getOpponentDeckPos() {
        return oDeckPos.transform.position;
    }

    public Vector2 getPlayerWinPilePos() {
        return pWinPilePos.transform.position;
    }

    public Vector2 getOpponentWinPilePos() {
        return oWinPilePos.transform.position;
    }

    public Vector2 getPlayerDeckCountPos() {
        return pDeckCountPos.transform.position;
    }

    public Vector2 getOpponentDeckCountPos() {
        return oDeckCountPos.transform.position;
    }

    public Vector2 getPlayerWinPileCountPos() {
        return pWinPileCountPos.transform.position;
    }

    public Vector2 getOpponentWinPileCountPos() {
        return oWinPileCountPos.transform.position;
    }
}
