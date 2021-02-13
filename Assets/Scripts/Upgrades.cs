using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    GameObject Player;
    Movements PlayerMovements;
    Inventory PlayerInventory;

    [SerializeField]
    float maxDistance, speedPercent, InventoryPercent;

    [SerializeField]
    LayerMask layer;

    [SerializeField]
    Text ButtonText;

    GameObject target;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerMovements = Player.GetComponent<Movements>();
        PlayerInventory = Player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, gameObject.transform.GetChild(0).transform.forward, out hit, maxDistance, layer))
        {
            target = hit.transform.gameObject;

            if (hit.transform.CompareTag("SpeedUpgrade"))
            {
                if (PlayerInventory.token > 0)
                {
                    ButtonText.color = new Color(0.5f, 1.0f, 0.5f);
                    ButtonText.text = "press E to upgrade Speed";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        float upValue = PlayerMovements.walkSpeed * (speedPercent / 100);
                        PlayerMovements.walkSpeed += upValue;
                        PlayerMovements.sprintSpeed += upValue;
                        PlayerInventory.token--;
                    }
                }
                else
                {
                    ButtonText.color = new Color(0.5f, 0.5f, 0.5f);
                    ButtonText.text = "press E to upgrade Speed";
                }
            }

            else if (hit.transform.CompareTag("InventoryUpgrade"))
            {
                if (PlayerInventory.token > 0)
                {
                    ButtonText.color = new Color(0.5f, 1.0f, 0.5f);
                    ButtonText.text = "press E to upgrade Inventory";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        int upValue = Mathf.RoundToInt(PlayerInventory.maxCoalNumber * (speedPercent / 100));
                        PlayerInventory.maxCoalNumber += upValue;
                        PlayerInventory.token--;
                    }
                }
                else
                {
                    ButtonText.color = new Color(0.5f, 0.5f, 0.5f);
                    ButtonText.text = "press E to upgrade Inventory";
                }
            }
        }
        else
        {
            if(target != null)
            {
                ButtonText.text = "";
                target = null;
            }
        }
    }
}
