using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    GameObject Player;

    Inventory PlayerInventory;

    [SerializeField]
    Thermometter PlayerThermometer;

    [SerializeField]
    float radius, multiplyCoeff;

    public GameObject PlaceText;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerInventory = Player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < radius)
        {
            PlaceText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(PlayerThermometer.AddTemperature(PlayerInventory.coalNumber * multiplyCoeff));
                PlayerInventory.coalNumber = 0;
            }
        }
        else
        {
            PlaceText.SetActive(false);
        }
    }
}
