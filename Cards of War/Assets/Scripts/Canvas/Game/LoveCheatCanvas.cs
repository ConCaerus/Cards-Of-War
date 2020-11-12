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

    [SerializeField] TextMeshProUGUI dialogText;

    bool shown = false;

    private void Awake() {
        if(canvasHolder == null)
            canvasHolder = GetComponentInChildren<GameObject>();
    }

    private void Start() {
        cheat = GameObject.FindGameObjectWithTag("Player").GetComponent<LoveCheat>();
        forceHideCanvas();
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


    public void initDialogText() {
        dialogText.GetComponent<TextAnimation>().init();
    }


    public void showCanvas() {
        if(!shown) {
            canvasHolder.transform.DOComplete();
            //  sets position
            canvasHolder.transform.DOMove(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, canvasHolder.transform.position.z), 0.5f);

            //  starts text animations
            if(dialogText.GetComponent<TextAnimation>() != null) {
                dialogText.GetComponent<TextAnimation>().startAnimation();
            }

            //  mixes up options
            mixUpOptionPoses();

            setOptionTexts();
            shown = true;
        }
    }

    public void hideCanvas() {
        if(shown) {
            canvasHolder.transform.DOComplete();
            canvasHolder.transform.DOMove(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 10.0f, canvasHolder.transform.position.z), 0.5f);

            shown = false;

            Debug.Log("here");
        }
    }

    public void forceHideCanvas() {
        canvasHolder.transform.DOComplete();
        canvasHolder.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 10.0f, canvasHolder.transform.position.z);

        shown = false;
    }




    //  Buttons 
    public void bestButton() {
        if(shown) 
            cheat.chooseOption(LoveCheat.LoveOption.bestOption);
        hideCanvas();
    }

    public void goodButton() {
        if(shown)
            cheat.chooseOption(LoveCheat.LoveOption.goodOption);
        hideCanvas();
    }

    public void badButton() {
        if(shown)
            cheat.chooseOption(LoveCheat.LoveOption.badOption);
        hideCanvas();
    }

    public void worstButton() {
        if(shown)
            cheat.chooseOption(LoveCheat.LoveOption.worstOption);
        hideCanvas();
    }
}
