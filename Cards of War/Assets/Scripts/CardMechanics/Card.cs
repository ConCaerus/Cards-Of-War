using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards of War/Card", order = 3)]
public class Card : ScriptableObject {
    public int value;
    public Sprite sprite;
}
