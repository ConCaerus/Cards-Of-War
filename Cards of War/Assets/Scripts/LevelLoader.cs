using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader {


    public static IEnumerator waitToLoadLevel(string levelName, SceneTransitionCanvas transition) {
        if(transition != null)
            transition.show();

        //  transition is still being played
        while(transition != null && !transition.getShowing()) {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadSceneAsync(levelName);
    }
}
