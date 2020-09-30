using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour {
    [SerializeField] GameObject cardPreset;

    float screenTop, screenRight, screenBottom, screenLeft;

    int screenWidth, screenHeight;
    float widthBuffer = 0.1f, heightBuffer = 0.5f;
    float cardWidth, cardHeight;

    private void Awake() {
        screenHeight = Camera.main.scaledPixelHeight;
        screenWidth = Camera.main.scaledPixelWidth;

        cardWidth = cardPreset.transform.lossyScale.x;
        cardHeight = cardPreset.transform.lossyScale.y;

        screenTop = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 1.0f)).y;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(1.0f, 0.0f)).x;
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).y;
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).x;

        positionDecks();
    }

    private void Update() {
        screenHeight = Camera.main.scaledPixelHeight;
        screenWidth = Camera.main.scaledPixelWidth;

        cardWidth = cardPreset.transform.lossyScale.x;
        cardHeight = cardPreset.transform.lossyScale.y;

        screenTop = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 1.0f)).y * -1.0f;  //  dont ask why multing by neg 1
        screenRight = Camera.main.ScreenToWorldPoint(new Vector2(1.0f, 0.0f)).x * -1.0f;//  it's a whole thing
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).y;
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).x;


        positionDecks();
        positionWinPiles();
    }


    //  positions the decks in relation to the current resolution
    void positionDecks() {
        Vector2 playerDeckPos = new Vector2(screenLeft + cardWidth + widthBuffer, screenBottom + cardHeight + heightBuffer);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Deck>().gameObject.transform.position = playerDeckPos;

        Vector2 opponentDeckPos = new Vector2(-playerDeckPos.x, -playerDeckPos.y + cardHeight + heightBuffer);
        GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<Deck>().gameObject.transform.position = opponentDeckPos;
    }

    void positionWinPiles() {
        Vector2 playerWinPilePos = new Vector2(screenRight - cardWidth - widthBuffer * 5.0f, screenBottom + cardHeight + heightBuffer);
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WinPile>().gameObject.transform.position = playerWinPilePos;

        Vector2 opponentWinPilePos = new Vector2(-playerWinPilePos.x, -playerWinPilePos.y + cardHeight + heightBuffer);
        GameObject.FindGameObjectWithTag("Opponent").GetComponentInChildren<WinPile>().gameObject.transform.position = opponentWinPilePos;
    }


    //  getters

    public int getScreenWidth() {
        return Camera.main.pixelWidth;
    }

    public int getScreenHeight() {
        return screenHeight;
    }

    public float getCardWidth() {
        return cardWidth;
    }
}
