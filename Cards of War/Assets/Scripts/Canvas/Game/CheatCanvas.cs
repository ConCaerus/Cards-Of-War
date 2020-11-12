using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatCanvas : MonoBehaviour {
    [SerializeField] Slider playerCheatSlider, opponentCheatSlider;
    [SerializeField] bool showOpponentCheatSlider = false;

    float speed = 18.0f;

    private void Awake() {
        playerCheatSlider.maxValue = 10.0f;
        opponentCheatSlider.maxValue = 10.0f;

        playerCheatSlider.value = 0.0f;
        opponentCheatSlider.value = 0.0f;

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>() == null)
            playerCheatSlider.enabled = false;
        else 
            playerCheatSlider.enabled = true;

        if(GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>() == null && showOpponentCheatSlider)
            opponentCheatSlider.enabled = false;
        else
            opponentCheatSlider.enabled = true;
    }


    private void Update() {
        changeCheatSliderValues();
    }

    void changeCheatSliderValues() {
        if(playerCheatSlider.enabled == true) {
            float target = GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().getChargeAmount();
            playerCheatSlider.value = Mathf.Lerp(playerCheatSlider.value, target, speed * Time.deltaTime);
        }

        if(opponentCheatSlider.enabled == true && showOpponentCheatSlider) {
            float target = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>().getChargeAmount();
            opponentCheatSlider.value = Mathf.Lerp(opponentCheatSlider.value, target, speed * Time.deltaTime);
        }
    }
}