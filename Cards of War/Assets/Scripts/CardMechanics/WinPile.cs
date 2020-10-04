using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPile : MonoBehaviour {
    List<GameObject> cardsInPile = new List<GameObject>();




    void setSortingOrderForCardsInPile() {
        for(int i = 0; i < cardsInPile.Count - 1; i++) {
            cardsInPile[i].GetComponent<SpriteRenderer>().sortingOrder = -(cardsInPile.Count - 1 - i);
        }
    }


    public void addCardToPile(GameObject card, GameObject other = null) {
        cardsInPile.Add(card);
        card.transform.SetParent(transform);

        if(other != null) {
            cardsInPile.Add(other);
            other.transform.SetParent(transform);
        }

        setSortingOrderForCardsInPile();
    }

    public GameObject takeCardFromPile() {
        GameObject temp = cardsInPile[cardsInPile.Count - 1];
        cardsInPile.RemoveAt(cardsInPile.Count - 1);
        temp.transform.SetParent(null);
        return temp;
    }

    public int getNumOfCardsInPile() {
        return cardsInPile.Count;
    }

    public Vector2 getWinPilePos() {
        return transform.position;
    }
}
