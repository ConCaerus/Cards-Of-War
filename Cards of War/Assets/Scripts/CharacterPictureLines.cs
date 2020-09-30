using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterPictureLines : MonoBehaviour {
    [SerializeField] Sprite[] insideLineSprites, outsideLineSprites;

    List<GameObject> insideLines = new List<GameObject>(), outsideLines = new List<GameObject>();
    List<float> insideSpeeds = new List<float>(), outsideSpeeds = new List<float>();

    float insideOffset = 0.75f, outsideOffset = 0.85f;

    int minInside = 2, maxInside = 4, minOutside = 4, maxOutside = 6;
    float minSpeed = 10.0f, maxSpeed = 50.0f;



    private void Awake() {
        createLines();
    }


    private void Update() {
        rotateLines();
    }


    void createLines() {
        int randInside = Random.Range(minInside, maxInside + 1);
        int randOutside = Random.Range(minOutside, maxOutside + 1);


        //  create inside lines
        for(int i = 0; i < randInside; i++) {
            var temp = new GameObject("inside");

            //  sprite
            int randSprite = Random.Range(0, insideLineSprites.Length);
            var sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = insideLineSprites[randSprite];

            //  position
            temp.transform.position = transform.position + new Vector3(-insideOffset, 0.0f, 0.0f);

            //  line holder
            //  this is what gets added to the lines list so that I can use this to rotate the actual lines
            var holder = new GameObject("inside holder");
            holder.transform.position = transform.position;
            holder.transform.SetParent(transform);
            temp.transform.SetParent(holder.transform);
            holder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-360.0f, 360.0f));
            insideLines.Add(holder);

            //  speed
            float randSpeed = Random.Range(minSpeed, maxSpeed);
            insideSpeeds.Add(randSpeed);
        }


        //  create outside lines
        for(int i = 0; i < randOutside; i++) {
            var temp = new GameObject("outside");

            //  sprite
            int randSprite = Random.Range(0, outsideLineSprites.Length);
            var sr = temp.AddComponent<SpriteRenderer>();
            sr.sprite = outsideLineSprites[randSprite];

            //  position
            temp.transform.position = transform.position + new Vector3(-outsideOffset, 0.0f, 0.0f);

            //  line holder
            //  this is what gets added to the lines list so that I can use this to rotate the actual lines
            var holder = new GameObject("outside holder");
            holder.transform.position = transform.position;
            holder.transform.SetParent(transform);
            temp.transform.SetParent(holder.transform);
            holder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(-360.0f, 360.0f));
            outsideLines.Add(holder);

            //  speed
            float randSpeed = Random.Range(minSpeed, maxSpeed);
            outsideSpeeds.Add(randSpeed);
        }
    }


    void rotateLines() {
        //  inside lines
        for(int i = 0; i < insideLines.Count; i++) {
            if(insideLines[i].transform.eulerAngles.z > 360.0f)
                insideLines[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, insideLines[i].transform.eulerAngles.z - 360.0f);
            insideLines[i].transform.Rotate(new Vector3(0.0f, 0.0f, insideSpeeds[i] * Time.deltaTime));
        }

        //  outside lines
        for(int i = 0; i < outsideLines.Count; i++) {
            if(outsideLines[i].transform.eulerAngles.z > 360.0f)
                outsideLines[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, outsideLines[i].transform.eulerAngles.z - 360.0f);
            outsideLines[i].transform.Rotate(new Vector3(0.0f, 0.0f, outsideSpeeds[i] * Time.deltaTime));
        }
    }
}
