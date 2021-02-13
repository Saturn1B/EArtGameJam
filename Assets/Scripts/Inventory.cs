using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject Player;

    public Text CoalText;
    public Text TokenText;


    //[HideInInspector]
    public int coalNumber;
    public int maxCoalNumber;
    public int token;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        CoalText.text = coalNumber.ToString() + " / " + maxCoalNumber;
        TokenText.text = token.ToString();

    }
}
