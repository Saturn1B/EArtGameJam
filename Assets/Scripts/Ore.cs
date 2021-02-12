using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    GameObject Player;

    Inventory PlayerInventory;

    [SerializeField]
    float radius;

    public float timeToMine;
    public float currentTime;

    public int coalGiven;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerInventory = Player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) < radius)
        {
            if (Input.GetMouseButton(0))
            {
                currentTime += Time.deltaTime;
                Debug.Log("isMining");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                currentTime = 0;
            }

            if(currentTime >= timeToMine)
            {
                currentTime = 0;
                PlayerInventory.coalNumber += coalGiven;
                Destroy(gameObject);
            }
        }
    }*/
}
