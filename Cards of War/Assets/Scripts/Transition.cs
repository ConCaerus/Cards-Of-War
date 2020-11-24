using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {
    static int NUMBER = 4;
    float[] times = new float[NUMBER];
    events[] stages = new events[NUMBER];
    int stageIndex = 0;

    public enum events {
        table, opponent, cards, crowd
    };

    public void setStageOrder(events[] order) {
        for(int i = 0; i < NUMBER; i++) {
            stages[i] = order[i];
        }
    }

    public void setTimes(float[] t) {
        for(int i = 0; i < NUMBER; i++) {
            times[i] = t[i];
        }
    }

    public void start() {
        StartCoroutine(showAnimation());
    }



    IEnumerator showAnimation() {
        for(int i = 0; i < NUMBER; i++) {
            yield return new WaitForSeconds(times[i]);
            showEvent(stages[i]);
        }
    }

    void showEvent(events e) {
        switch(e) {
            case events.table:
                FindObjectOfType<Table>().showTable();
                break;

            case events.opponent:
                FindObjectOfType<CharacterPicture>().showCharacterPicture();
                break;

            case events.cards:
                FindObjectOfType<MasterDeck>().populateDecks();
                break;
            case events.crowd:
                FindObjectOfType<AudiencePopulator>().populate();
                break;
        }
    }
}
