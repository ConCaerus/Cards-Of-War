using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundCameraFlash : MonoBehaviour {
    [SerializeField] Sprite[] flashSprites;
    Coroutine flashDelay;
    float waitTime = 0.25f, maxRand = 0.25f;
    int maxFlashRecreateCount = 1;

    int flashCount;
    const int maxFlashCount = 5, minFlashCount = 2;

    private void Start() {
        StartCoroutine(flash());
    }

    private void Update() {
        if(flashCount <= minFlashCount)
            StartCoroutine(flash());
    }


    IEnumerator flash() {
        yield return new WaitForSeconds(waitTime + Random.Range(-maxRand, maxRand));

        //  create the object
        GameObject ob = new GameObject("Camera Flash");
        flashCount++;
        
        //  set Sprite
        int spriteIndex = Random.Range(0, flashSprites.Length);
        var sr = ob.AddComponent<SpriteRenderer>();
        sr.sprite = flashSprites[spriteIndex];
        sr.color = Color.black;
        sr.sortingOrder = -5;
        ob.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

        //  set Position
        var b = GetComponent<Background>();
        ob.transform.position = new Vector3(Random.Range(b.getLeastX(), b.getGreatestX()), Random.Range(b.getLeastY(), b.getGreatestY()), 0.0f);
        ob.transform.SetParent(Camera.main.transform);  //  so the scale does not get warped

        StartCoroutine(animateFlash(ob));

        
        //  start new camera flashes

        int flashNum = Random.Range(1, maxFlashRecreateCount + 1);
        if(flashCount < maxFlashCount && flashNum > 0) {
            for(int i = 0; i < flashNum; i++) {
                StartCoroutine(flash());
            }
        }
    }

    IEnumerator animateFlash(GameObject flash) {
        //  start flash animation by increasing the scale
        const float tOne = 0.05f;
        flash.transform.DOPunchScale(new Vector3(1.25f, 1.25f, 1.25f), tOne);
        flash.GetComponent<SpriteRenderer>().DOColor(Color.white, tOne);

        yield return new WaitForSeconds(tOne);

        const float tTwo = 0.15f;

        //  scale the flash down to zero

        flash.transform.DOComplete();
        flash.transform.DOScale(new Vector3(0.0f, 0.0f, 0.0f), tTwo);
        flash.GetComponent<SpriteRenderer>().DOColor(Color.black, tTwo);

        yield return new WaitForSeconds(tTwo);

        //  destroy flash
        Destroy(flash.gameObject);
        flashCount--;
    }
}
