using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiencePopulator : MonoBehaviour {
    [SerializeField] Sprite[] sprites;
    [SerializeField] float xOffset, yOffset, maxXRand, maxYRand;
    [SerializeField] Color memberColor;

    List<GameObject> members = new List<GameObject>();

    Vector2 spawnPoint;

    private void Start() {
        var b = GetComponent<Background>();
        spawnPoint = new Vector2(b.getLeastX(), b.getLeastY());
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
            //  moves the spawn point along the y axis
            float randY = Random.Range(0.0f, maxYRand);
            spawnPoint = new Vector2(GetComponent<Background>().getLeastX(), spawnPoint.y + yOffset + randY);




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
                sr.color = memberColor;
                member.transform.position = spawnPoint;
                member.transform.SetParent(transform);

                members.Add(member);
                count++;
            }

            sortingIndex-=2;
        }

        spawnPoint = new Vector2(GetComponent<Background>().getLeastX(), GetComponent<Background>().getLeastY());
        Debug.Log(count);
    }

    void deleteAllMembers() {
        foreach(var i in members)
            Destroy(i.gameObject);

        members.Clear();
    }
}
