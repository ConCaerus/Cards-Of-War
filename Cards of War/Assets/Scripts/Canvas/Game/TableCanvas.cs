using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableCanvas : MonoBehaviour {

    [SerializeField] GameObject table;
    const float tableSpeed = 0.25f;
    Vector2 tableShowPos, tableHidePos;

    [SerializeField] GameObject playerDeckPos, opponentDeckPos, playerWinPilePos, opponentWinPilePos, playerDeckCountPos, opponentDeckCountPos, playerWinPileCountPos, opponentWinPileCountPos;


    private void Awake() {
        tableShowPos = table.GetComponent<RectTransform>().TransformPoint(Vector2.zero);
        tableHidePos = tableShowPos - new Vector2(0.0f, 10.0f);

        forceHideTable();
    }


    public void showTable() {
        table.transform.DOComplete();
        table.transform.DOMove(tableShowPos, tableSpeed);
    }

    public void hideTable() {
        table.transform.DOComplete();
        table.transform.DOMove(tableHidePos, tableSpeed);
    }

    public void forceHideTable() {
        table.transform.DOComplete();
        table.transform.position = tableHidePos;
    }


    //  some shit that should prob be deleted
    public Quaternion getTableRotation() {
        return table.transform.rotation;
    }



    //  things
    public Vector2 getPlayerDeckPos() {
        return Camera.main.ViewportToWorldPoint(playerDeckPos.GetComponent<RectTransform>().position);
    }

    public Vector2 getOpponentDeckPos() {
        return opponentDeckPos.GetComponent<RectTransform>().anchoredPosition;
    }

    public Vector2 getPlayerWinPilePos() {
        return playerWinPilePos.GetComponent<RectTransform>().anchoredPosition;
    }

    public Vector2 getOpponentWinPilePos() {
        return opponentWinPilePos.GetComponent<RectTransform>().anchoredPosition;
    }

    public Vector2 getPlayerDeckCountPos() {
        return playerDeckCountPos.GetComponent<RectTransform>().anchoredPosition;
    }

    public Vector2 getOpponentDeckCountPos() {
        return opponentDeckCountPos.GetComponent<RectTransform>().position;
    }

    public Vector2 getPlayerWinPileCountPos() {
        return playerWinPileCountPos.GetComponent<RectTransform>().position;
    }

    public Vector2 getOpponentWinPileCountPos() {
        return opponentWinPileCountPos.GetComponent<RectTransform>().position;
    }
}
