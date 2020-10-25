using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoveCheatCanvas : MonoBehaviour {
    LoveCheat cheat;

    [SerializeField] GameObject bestObject, goodObject, badObject, worstObject;
    [SerializeField] GameObject canvasHolder;

    bool shown = false;

    private void Awake() {
        if(canvasHolder == null)
            canvasHolder = GetComponentInChildren<GameObject>();
    }

    private void Start() {
        cheat = GameObject.FindGameObjectWithTag("Player").GetComponent<LoveCheat>();
        canvasHolder.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10.0f, canvasHolder.transform.position.z);
    }


    void setOptionTexts() {
        bestObject.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().getLoveOptionDialog(LoveCheat.LoveOption.bestOption);
        goodObject.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().getLoveOptionDialog(LoveCheat.LoveOption.goodOption);
        badObject.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().getLoveOptionDialog(LoveCheat.LoveOption.badOption);
        worstObject.GetComponentInChildren<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Opponent").GetComponent<DialogHandler>().getLoveOptionDialog(LoveCheat.LoveOption.worstOption);
    }

    void mixUpOptionPoses() {
        List<Vector2> poses = new List<Vector2>();
        List<GameObject> options = new List<GameObject>() {bestObject, goodObject, badObject, worstObject};
        for(int i = 0; i < 4; i++)
            poses.Add(options[i].transform.position);

        for(int i = 0; i < 4; i++) {
            int rand = Random.Range(0, poses.Count);
            options[i].transform.position = poses[rand];
            poses.RemoveAt(rand);
        }
    }



    public void showCanvas() {
        if(!shown) {
            canvasHolder.transform.DOMove(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, canvasHolder.transform.position.z), 0.5f);

            shown = true;
        }
    }

    public void hideCanvas() {
        if(shown) {
            canvasHolder.transform.DOMove(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 10.0f, canvasHolder.transform.position.z), 0.5f);

            shown = false;
        }
    }




    //  Buttons 
    public void bestButton() {
        cheat.chooseOption(LoveCheat.LoveOption.bestOption);
    }

    public void goodButton() {
        cheat.chooseOption(LoveCheat.LoveOption.goodOption);
    }

    public void badButton() {
        cheat.chooseOption(LoveCheat.LoveOption.badOption);
    }

    public void worstButton() {
        cheat.chooseOption(LoveCheat.LoveOption.worstOption);
    }
}
