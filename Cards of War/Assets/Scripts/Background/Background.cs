using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public float getGreatestX() {
        return transform.position.x + transform.localScale.x / 4.0f;
    }

    public float getGreatestY() {
        return transform.position.y + transform.localScale.y / 4.0f;
    }

    public float getLeastX() {
        return transform.position.x - transform.localScale.x / 4.0f;
    }

    public float getLeastY() {
        return transform.position.y - transform.localScale.y / 4.0f;
    }
}
