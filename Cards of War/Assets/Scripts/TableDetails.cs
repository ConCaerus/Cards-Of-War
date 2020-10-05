using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableDetails : MonoBehaviour {
    [SerializeField] Sprite[] detailSprites;

    List<GameObject> details = new List<GameObject>();

    const int minDetailCount = 750, maxDetailCount = 1000;
    const float distToBeMoved = 1.0f;

    float topPos, rightPos, bottomPos, leftPos;

    private void Awake() {
        topPos = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 1.0f, 0.0f)).y * -1.0f;
        rightPos = Camera.main.ScreenToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x * -1.0f;
        bottomPos = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y;
        leftPos = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x;
    }


    private void Start() {
        createDetails();
    }


    void createDetails() {
        int detailCount = Random.Range(minDetailCount, maxDetailCount + 1);

        for(int i = 0; i < detailCount; i++) {
            float randX = Random.Range(leftPos, rightPos);
            float randY = Random.Range(bottomPos, topPos);
            int randSpriteIndex = Random.Range(0, detailSprites.Length - 1);

            var temp = new GameObject();
            var sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = detailSprites[randSpriteIndex];
            sr.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
            temp.transform.position = new Vector3(randX, randY, transform.position.z);
            temp.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-360.0f, 360.0f));

            temp.transform.SetParent(transform);

            details.Add(temp);
        }
    }

    public void moveDetailsAwayFromPosition(Vector2 pos) {
        foreach(var i in details) {
            if(Vector2.Distance(i.transform.position, pos) < distToBeMoved) {
                //  1/4 chance to be moved
                int rand = Random.Range(0, 4);

                if(rand == 0) {
                    var randX = Random.Range(-2.0f * Mathf.Abs(pos.x - i.transform.position.x), 2.0f * Mathf.Abs(pos.x - i.transform.position.x));
                    var randY = Random.Range(-2.0f * Mathf.Abs(pos.y - i.transform.position.y), 2.0f * Mathf.Abs(pos.y - i.transform.position.y));
                    var target = new Vector2(i.transform.position.x + randX, i.transform.position.y + randY);

                    i.transform.DOMove(target, 0.2f, false);
                }
            }
        }
    }
}
