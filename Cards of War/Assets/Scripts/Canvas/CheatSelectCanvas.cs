using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheatSelectCanvas : MonoBehaviour {
    [SerializeField] Image background;


    private void Awake() {
        hideBackground();
    }


    public void showBackground() {
        background.gameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f);
    }

    public void hideBackground() {
        background.gameObject.transform.DOScale(Vector3.zero, 0.5f);
    }


    //  buttons

    public void doneSelecting() {
        GameObject selectedToggle = null;
        GameObject toggleGroup = background.GetComponentInChildren<ToggleGroup>().gameObject;

        foreach(var i in toggleGroup.GetComponentsInChildren<Toggle>()) {
            if(i.isOn) {
                selectedToggle = i.gameObject;
                break;
            }
        }

        FindObjectOfType<GameStateHandler>().cheatHasBeenSelected(selectedToggle.GetComponent<Cheat>());
    }
}