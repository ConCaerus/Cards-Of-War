using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatCanvas : MonoBehaviour {
    [SerializeField] Slider playerCheatSlider, opponentCheatSlider;

    float speed = 18.0f;

    private void Awake() {
        playerCheatSlider.maxValue = 10.0f;
        opponentCheatSlider.maxValue = 10.0f;

        playerCheatSlider.value = 0.0f;
        opponentCheatSlider.value = 0.0f;

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>() == null)
            playerCheatSlider.enabled = false;

        if(GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>() == null)
            opponentCheatSlider.enabled = false;
    }


    private void Update() {
        changeCheatSliderValues();
    }

    void changeCheatSliderValues() {
        if(playerCheatSlider.enabled == true) {
            float target = GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().getChargeAmount();
            playerCheatSlider.value = Mathf.Lerp(playerCheatSlider.value, target, speed * Time.deltaTime);
        }

        if(opponentCheatSlider == true) {
            float target = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>().getChargeAmount();
            opponentCheatSlider.value = Mathf.Lerp(opponentCheatSlider.value, target, speed * Time.deltaTime);
        }
    }
}