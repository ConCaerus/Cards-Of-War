using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialog", menuName = "Cards of War/Dialog", order = 1)]
public class Dialog : ScriptableObject {
    public List<string> introDialog = new List<string>();

    public List<string> battleDialog = new List<string>();
    public List<string> cheatDialog = new List<string>();

    public List<string> winDialog = new List<string>();
    public List<string> loseDialog = new List<string>();


    //  specific dialog
    public List<string> bestLoveDialog = new List<string>();
    public List<string> goodLoveDialog = new List<string>();
    public List<string> badLoveDialog = new List<string>();
    public List<string> worstLoveDialog = new List<string>();
}
