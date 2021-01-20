using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CheatCanvas : MonoBehaviour {
    [SerializeField] Slider playerCheatSlider, opponentCheatSlider;
    [SerializeField] GameObject playerCheatSliderFill;
    Color normColor;
    Vector3 normPlayerSliderScale;
    [SerializeField] bool showOpponentCheatSlider = false;
    public bool animate = true;

    float speed = 18.0f;

    private void Awake() {
        playerCheatSlider.maxValue = 10.0f;
        opponentCheatSlider.maxValue = 10.0f;

        playerCheatSlider.value = 0.0f;
        opponentCheatSlider.value = 0.0f;

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>() == null)
            playerCheatSlider.enabled = false;
        else {
            playerCheatSlider.enabled = true;
            normPlayerSliderScale = playerCheatSlider.GetComponent<RectTransform>().localScale;
            normColor = playerCheatSliderFill.GetComponent<Image>().color;
        }

        if(GameObject.FindGameObjectWithTag("Opponent").GetComponent<Cheat>() == null && showOpponentCheatSlider)
            opponentCheatSlider.enabled = false;
        else
            opponentCheatSlider.enabled = true;
    }


    private void Update() {
        changeCheatSliderValues();
        animatePlayerSliderWhenCharged();
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


    void animatePlayerSliderWhenCharged() {
        if(playerCheatSlider.enabled == true && GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().getCharged() && animate) {
            playerCheatSlider.GetComponent<RectTransform>().DOScaleY(normPlayerSliderScale.y + 2.0f, 0.15f);
            playerCheatSliderFill.GetComponent<Image>().DOComplete();
            playerCheatSliderFill.GetComponent<Image>().DOColor(GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().cheatColor, 0.15f);
            animate = false;
        }
        else if(playerCheatSlider == true && !GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().getCharged()) {
            playerCheatSlider.GetComponent<RectTransform>().DOScaleY(normPlayerSliderScale.y, 0.15f);
            playerCheatSliderFill.GetComponent<Image>().DOColor(normColor, 0.5f);
        }

        animate = !GameObject.FindGameObjectWithTag("Player").GetComponent<Cheat>().getCharged();
    }
}