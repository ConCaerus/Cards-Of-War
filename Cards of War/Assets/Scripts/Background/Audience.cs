using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Audience : MonoBehaviour {
    [SerializeField] Sprite[] sprites;
    [SerializeField] float xOffset, yOffset, maxXRand, maxYRand;
    [SerializeField] Color memberColor;
    float colorModYStep = 0.005f, colorModXStep = 0.005f;

    int rowIndex = 0;

    List<GameObject> members = new List<GameObject>();
    List<int> alreadyAnimatingIndex = new List<int>();

    const int numOfAnimators = 20;
    List<Coroutine> animators = new List<Coroutine>();

    Vector2 spawnPoint;

    private void Start() {
        deleteAllMembers();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            deleteAllMembers();
            populate();
        }
    }

    public void populate() {
        int sortingIndex = -10;
        int count = 0;
        while(spawnPoint.y < GetComponent<Background>().getGreatestY()) {
            Time.timeScale = 0;
            //  moves the spawn point along the y axis
            float randY = Random.Range(0.0f, maxYRand);
            spawnPoint = new Vector2(GetComponent<Background>().getLeastX(), spawnPoint.y + yOffset + randY);
            int rowTemp = 0;




            while(spawnPoint.x < GetComponent<Background>().getGreatestX()) {
                //  moves the spawn point along the x axis
                float randX = Random.Range(0.0f, maxXRand);
                spawnPoint += new Vector2(xOffset + randX, 0.0f);


                //  creates new audience member
                var member = new GameObject("audience");
                var sr = member.AddComponent<SpriteRenderer>();
                int randIndex = Random.Range(0, sprites.Length);
                sr.sprite = sprites[randIndex];
                sr.sortingOrder = sortingIndex + Random.Range(0, 2);
                var randColor = Random.Range(-1, 2);
                member.transform.position = spawnPoint;
                float distFromCenter = Mathf.Abs(spawnPoint.x);
                sr.color = new Color(memberColor.r - (distFromCenter * colorModXStep) - (colorModYStep * randColor), memberColor.g - (distFromCenter * colorModXStep) - (colorModYStep * randColor), memberColor.b - (distFromCenter * colorModXStep) - (colorModYStep * randColor), 1.0f);
                if(sr.color.r == Color.black.r) {
                    Destroy(member);
                    break;
                }
                member.transform.SetParent(transform);

                members.Add(member);
                count++;
                rowTemp++;
            }


            if(rowIndex == 0)
                rowIndex = rowTemp;

            sortingIndex-=2;
            memberColor = new Color(memberColor.r - colorModYStep, memberColor.g - colorModYStep, memberColor.b - colorModYStep, 1.0f);
            if(memberColor.r == Color.black.r) {
                break;
            }
        }
        Time.timeScale = 1;

        for(int i = 0; i < numOfAnimators; i++) {
            StartCoroutine(animateMember());
        }

        spawnPoint = new Vector2(GetComponent<Background>().getLeastX(), GetComponent<Background>().getLeastY());
    }

    void deleteAllMembers() {
        if(animators.Count > 0) {
            foreach(var i in animators) 
                StopCoroutine(i);
        }
        foreach(var i in members)
            Destroy(i.gameObject);

        members.Clear();
        var b = GetComponent<Background>();
        spawnPoint = new Vector2(b.getLeastX(), b.getLeastY());
    }


    IEnumerator animateMember() {
        yield return new WaitForEndOfFrame();
        int index = Random.Range(rowIndex * 1, members.Count);

        bool useable = false;
        while(!useable) {
            useable = true;
            foreach(var i in alreadyAnimatingIndex) {
                if(index == i) {
                    useable = false;

                    index = Random.Range(0, members.Count);
                    Debug.Log("useful");
                }
            }
        }


        var temp = members[index];

        temp.transform.DOComplete();
        temp.transform.DOPunchPosition(new Vector3(0.0f, temp.transform.localScale.y / 4.0f, 0.0f), 0.15f);

        StartCoroutine(animateMember());
    }
}
