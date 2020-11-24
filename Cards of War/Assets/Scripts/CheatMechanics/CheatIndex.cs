using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatIndex : MonoBehaviour {


    public int getCheatIndexOfType(Cheat cheat) {
        int index = 0;
        foreach(var i in GetComponents<Cheat>()) {
            if(cheat.getName() == i.getName())
                return index;
                index++;
        }

        Debug.LogError("Cheat index needs updating");
        return -1;
    }

    public Cheat getCheatFromIndex(int index) {
        int other = 0;
        foreach(var i in GetComponents<Cheat>()) {
            if(other == index)
                return i;
            other++;
        }

        Debug.LogError("Cheat index needs updating");
        return null;
    }

    public Cheat getCheatFromCheat(Cheat cheat) {
        foreach(var i in GetComponents<Cheat>()) {
            if(i.getName() == cheat.getName())
                return i;
        }

        return null;
    }


    //  didn't want to do it this way but fuck it
    public Cheat addCheatToObject(GameObject ob, Cheat cheat) {
        //  Add cards cheat
        if(cheat.getName() == GetComponent<AddCardsCheat>().getName())
            return ob.AddComponent<AddCardsCheat>();

        //  Additional value cheat
        if(cheat.getName() == GetComponent<AdditionalValueCheat>().getName())
            return ob.AddComponent<AdditionalValueCheat>();

        //  Steal cheat
        if(cheat.getName() == GetComponent<StealCheat>().getName())
            return ob.AddComponent<StealCheat>();

        //  win cheat
        if(cheat.getName() == GetComponent<WinCheat>().getName())
            return ob.AddComponent<WinCheat>();

        //  love cheat
        if(cheat.getName() == GetComponent<LoveCheat>().getName())
            return ob.AddComponent<LoveCheat>();



        Debug.LogError("Cheat index needs updating");
        return null;
    }
}
