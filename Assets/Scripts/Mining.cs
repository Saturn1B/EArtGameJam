using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    GameObject Player;
    Inventory PlayerInventory;

    [SerializeField]
    float maxDistance;

    [SerializeField]
    LayerMask layer;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerInventory = Player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, gameObject.transform.GetChild(0).transform.forward, out hit, maxDistance, layer))
        {
            if (Input.GetMouseButton(0))
            {
                hit.transform.GetComponent<Ore>().currentTime += Time.deltaTime;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                hit.transform.GetComponent<Ore>().currentTime = 0;
            }

            if (hit.transform.GetComponent<Ore>().currentTime >= hit.transform.GetComponent<Ore>().timeToMine)
            {
                hit.transform.GetComponent<Ore>().currentTime = 0;
                PlayerInventory.coalNumber += hit.transform.GetComponent<Ore>().coalGiven;
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
