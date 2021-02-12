using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    GameObject Player;

    //[HideInInspector]
    public int coalNumber;
    public int token;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
    }

}
