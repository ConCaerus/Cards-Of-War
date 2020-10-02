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

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<CheatHandler>() == null)
            playerCheatSlider.enabled = false;

        if(GameObject.FindGameObjectWithTag("Opponent").GetComponent<CheatHandler>() == null)
            opponentCheatSlider.enabled = false;
    }


    private void Update() {
        changeCheatSliderValues();
    }

    void changeCheatSliderValues() {
        if(playerCheatSlider.enabled == true) {
            float target = GameObject.FindGameObjectWithTag("Player").GetComponent<CheatHandler>().getCheatChargeAmount();
            playerCheatSlider.value = Mathf.Lerp(playerCheatSlider.value, target, speed * Time.deltaTime);
        }

        if(opponentCheatSlider == true) {
            float target = GameObject.FindGameObjectWithTag("Opponent").GetComponent<CheatHandler>().getCheatChargeAmount();
            opponentCheatSlider.value = Mathf.Lerp(opponentCheatSlider.value, target, speed * Time.deltaTime);
        }
    }
}