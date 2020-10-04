using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterPictureLines : MonoBehaviour {
    [SerializeField] Sprite[] lineSprites;

    List<GameObject> lines = new List<GameObject>();
    List<float> speeds = new List<float>();

    const float lineOffset = 0.85f;

    const int minLineCount = 4, maxLineCount = 8;
    const float minSpeed = 10.0f, maxSpeed = 100.0f;



    private void Awake() {
        createLines();
    }


    private void Update() {
        rotateLines();
    }


    void createLines() {
        int randCount = Random.Range(minLineCount, maxLineCount + 1);


        //  create lines
        for(int i = 0; i < randCount; i++) {
            var temp = new GameObject("ProfileLine");

            //  sprite
            int randSprite = Random.Range(0, lineSprites.Length);
            var sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = lineSprites[randSprite];

            //  position
            temp.transform.position = transform.position + new Vector3(-lineOffset, 0.0f, 0.0f);

            //  line holder
            //  this is what gets added to the lines list so that I can use this to rotate the actual lines
            var holder = new GameObject("line holder");
            holder.transform.position = transform.position;
            holder.transform.SetParent(transform);
            temp.transform.SetParent(holder.transform);
            holder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-360.0f, 360.0f));
            lines.Add(holder);

            //  speed
            float randSpeed = Random.Range(minSpeed, maxSpeed);
            speeds.Add(randSpeed);
        }
    }


    void rotateLines() {
        //  inside lines
        for(int i = 0; i < lines.Count; i++) {
            if(lines[i].transform.eulerAngles.z > 360.0f)
                lines[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, lines[i].transform.eulerAngles.z - 360.0f);
            lines[i].transform.Rotate(new Vector3(0.0f, 0.0f, speeds[i] * Time.deltaTime));
        }
    }
}
