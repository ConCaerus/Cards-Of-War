using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinCheatHand : MonoBehaviour {
    [SerializeField] float handYCoord, handShowXCoord, handHideXCoord;
    float speed = 0.15f;

    private void Start() {
        if(transform.parent.gameObject.tag == "Player")
            handYCoord -= transform.localScale.y / 3.0f;
        else if(transform.parent.gameObject.tag == "Opponent")
            handYCoord += transform.localScale.y / 3.0f;
        forceHide();
    }


    public void show() {
        clearChildren();
        
        transform.DOMove(new Vector3(handShowXCoord, handYCoord, transform.position.z), speed);
    }

    public void hide() {
        StartCoroutine(waitToHide());
    }

    public void forceHide() {
        transform.DOComplete();
        transform.position = new Vector3(handHideXCoord, handYCoord, transform.position.z);
    }


    void clearChildren() {
        if(transform.childCount > 0) {
            for(int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    //  this is because there is a bug that makes the win badge not attach to the hand right away
    //  if the hand were to hide immediatley, the badge would not follow
    IEnumerator waitToHide() {
        yield return new WaitForSeconds(0.075f);


        transform.DOMove(new Vector3(handHideXCoord, handYCoord, transform.position.z), speed);
    }


    public bool getShown() {
        return transform.position == new Vector3(handShowXCoord, handYCoord, transform.position.z);
    }
}
