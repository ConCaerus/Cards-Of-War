using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndGameCanvas : MonoBehaviour {
    [SerializeField] TextMeshProUGUI mainText, subText;
    [SerializeField] string[] winSubText, loseSubText, drawSubText;

    [SerializeField] TextMeshProUGUI playerScore, opponentScore;

    [SerializeField] GameObject[] buttons;

    //  0 = playerWon, 1 = opponent won, 2 = draw
    static int gameState = 0;


    private void Awake() {
        DOTween.Init();
    }


    private void Start() {
        FindObjectOfType<CheatSelectCanvas>().hideBackground();
        gameObject.SetActive(true);

        setScoreTexts();
        setTexts();
        animate();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
            animate();
    }


    void setTexts() {
        int rand = 0;
        switch(GameInformation.gameResult) {
            case 0:
                mainText.text = "You Won";

                rand = Random.Range(0, winSubText.Length);
                subText.text = winSubText[rand];
                break;
            case 1:
                mainText.text = "You Lost";

                rand = Random.Range(0, loseSubText.Length);
                subText.text = loseSubText[rand];
                break;
            case 2:
                mainText.text = "Draw";

                rand = Random.Range(0, drawSubText.Length);
                subText.text = drawSubText[rand];
                break;
        }
    }

    void setScoreTexts() {
        playerScore.text = "Your Score:\n" + GameInformation.playerEndScore.ToString();
        opponentScore.text = "Opponent's Score:\n" + GameInformation.opponentEndScore.ToString();
    }



    void animate() {
        animateMainText();
        animateSubText();
        animateButtons();
    }


    //  animators

    void animateMainText() {
        mainText.transform.DOComplete();
        mainText.transform.DOPunchPosition(new Vector3(0.0f, mainText.transform.position.y * 2, mainText.transform.position.z), 0.25f, 0, 0);
    }

    void animateSubText() {
        subText.transform.DOComplete();
        subText.transform.DOPunchPosition(new Vector3(0.0f, subText.transform.position.y * 2, subText.transform.position.z), 0.75f, 0, 0);
    }

    void animateButtons() {
        foreach(var i in buttons) {
            i.transform.DOComplete();
            i.transform.DOPunchPosition(new Vector3(0.0f, i.transform.position.y * -20, i.transform.position.z), 1.5f, 0, 0);
        }
    }


    //  buttons
    
    public void replay() {
        StartCoroutine(LevelLoader.waitToLoadLevel("Game", FindObjectOfType<SceneTransitionCanvas>()));
    }

    public void changeCheat() {
        FindObjectOfType<CheatSelectCanvas>().showBackground(gameObject);
        gameObject.SetActive(false);
    }

    public void nextBattle() {
        StartCoroutine(LevelLoader.waitToLoadLevel("Game", FindObjectOfType<SceneTransitionCanvas>()));
    }

    //  setters

    public void setGameState(int i) {
        gameState = i;
    }
}
