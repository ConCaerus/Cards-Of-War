using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCanvas : MonoBehaviour {

    [SerializeField] GameObject table;


    public Quaternion getTableRotation() {
        return table.transform.rotation;
    }
}
