using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardResolveParticles : MonoBehaviour {
    [SerializeField] ParticleSystem winParticles, loseParticles;

    float normMult = 0.5f, diff = 8.0f;
    float minMult = 0.1f, maxMult = 3.0f;

    public void playParticles(int pCardVal, Vector2 pCardPos, int oCardVal, Vector2 oCardPos) {
        var win = winParticles.main;
        var lose = loseParticles.main;

        int difference = Mathf.Abs(pCardVal - oCardVal);

        win.startSizeMultiplier = Mathf.Clamp(normMult + difference / diff, minMult, maxMult);
        lose.startSizeMultiplier = Mathf.Clamp(normMult - difference / diff, minMult, maxMult);


        //  player won
        if(pCardVal > oCardVal) {
            //  player
            winParticles.gameObject.transform.position = pCardPos;
            winParticles.Play();

            //  opponent 
            loseParticles.gameObject.transform.position = oCardPos;
            loseParticles.Play();
        }

        //  opponent won
        else if(oCardVal > pCardVal) {
            //  player
            loseParticles.gameObject.transform.position = pCardPos;
            loseParticles.Play();

            //  opponent 
            winParticles.gameObject.transform.position = oCardPos;
            winParticles.Play();

        }

        //  draw
        else {
            Vector2 mid = (pCardPos + oCardPos) / 2.0f;

            loseParticles.gameObject.transform.position = mid;
            loseParticles.Play();

        }
    }
}
