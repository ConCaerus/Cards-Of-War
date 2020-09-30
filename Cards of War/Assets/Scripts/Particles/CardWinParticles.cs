using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWinParticles : MonoBehaviour {
    ParticleSystem particles;

    private void Awake() {
        particles = GetComponent<ParticleSystem>();
    }


    public void playParticles(Vector2 position) {
        transform.position = position;
        particles.Play();
    }
}
