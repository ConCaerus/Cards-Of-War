using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour {


    //  buttons
    public void Play() {
        StartCoroutine(loading(SceneManager.LoadSceneAsync("Game")));
    }

    public void SelectCheat() {
        Debug.Log("Select Cheat");
    }

    public void Options() {
        Debug.Log("Options");
    }

    public void Exit() {
        Debug.Log("Exit");
        Application.Quit();
    }


    //  coroutines
    IEnumerator loading(AsyncOperation op) {
        while(!op.isDone) {
            //  fucking do something with this cunt
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            //Debug.Log(progress * 100.0f + "%");

            yield return null;
        }
    }
}
