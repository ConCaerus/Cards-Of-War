using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoveCheatCanvas : MonoBehaviour {
    LoveCheat cheat;

    [SerializeField] GameObject bestObject, goodObject, badObject, worstObject;
    [SerializeField] GameObject phone;

    bool alreadyReset = false;

    private void Start() {
        cheat = GameObject.FindGameObjectWithTag("Player").GetComponent<LoveCheat>();

        resetPhoneOptions();

        Debug.Log(phone.transform.position);
    }


    void resetPhoneOptions() {
        setOptionTexts();
        mixUpOptionPoses();
        alreadyReset = true;
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


    public void showPhone() {
        //  zoom or some shit

        
        if(!alreadyReset)
            resetPhoneOptions();
        phone.transform.DOMove(new Vector3(0.9f, -1.9f, phone.transform.position.z), 0.25f);
    }

    public void hidePhone() {
        phone.transform.DOMove(new Vector3(0.9f, -10.0f, phone.transform.position.z), 0.25f);
    }




    //  Buttons 
    public void bestButton() {
        cheat.chooseOption(LoveCheat.LoveOption.bestOption);
        alreadyReset = false;
    }

    public void goodButton() {
        cheat.chooseOption(LoveCheat.LoveOption.goodOption);
        alreadyReset = false;
    }

    public void badButton() {
        cheat.chooseOption(LoveCheat.LoveOption.badOption);
        alreadyReset = false;
    }

    public void worstButton() {
        cheat.chooseOption(LoveCheat.LoveOption.worstOption);
        alreadyReset = false;
    }
}
