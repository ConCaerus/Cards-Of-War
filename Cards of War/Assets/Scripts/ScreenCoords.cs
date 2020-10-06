using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenCoords {


    public static float screenTop() {
        return Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y;
    }

    public static float screenRight() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x;
    }

    public static float screenBottom() {
        return Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).y;
    }

    public static float screenLeft() {
        return Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x;
    }

    public static Vector2 screenMid() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));
    }
}
