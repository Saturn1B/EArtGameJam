using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    GameObject Player;
    Movements PlayerMovements;
    Inventory PlayerInventory;
    Thermometter PlayerThermometter;
    HookShot PlayerHookShot;

    [SerializeField]
    float maxDistance, speedPercent, InventoryPercent;

    [SerializeField]
    float decreasePercent, gainPercent, rebootPercent;

    [SerializeField]
    int hookShotPrice;

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
        PlayerHookShot = Player.GetComponent<HookShot>();
        PlayerThermometter = GameObject.Find("TestThermometter").GetComponent<Thermometter>();

        PlayerHookShot.enabled = false;
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
                    ButtonText.text = "press A to upgrade Speed";
                    if (Input.GetKeyDown(KeyCode.A))
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
                    ButtonText.text = "press A to upgrade Speed";
                }
            }

            else if (hit.transform.CompareTag("InventoryUpgrade"))
            {
                if (PlayerInventory.token > 0)
                {
                    ButtonText.color = new Color(0.5f, 1.0f, 0.5f);
                    ButtonText.text = "press A to upgrade Inventory";
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        int upValue = Mathf.RoundToInt(PlayerInventory.maxCoalNumber * (speedPercent / 100));
                        PlayerInventory.maxCoalNumber += upValue;
                        PlayerInventory.token--;
                    }
                }
                else
                {
                    ButtonText.color = new Color(0.5f, 0.5f, 0.5f);
                    ButtonText.text = "press A to upgrade Inventory";
                }
            }

            else if (hit.transform.CompareTag("FurnaceUpgrade"))
            {
                if (PlayerInventory.token > 0)
                {
                    ButtonText.color = new Color(0.5f, 1.0f, 0.5f);
                    ButtonText.text = "press A to upgrade Furnace";
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        PlayerThermometter.decreaseSpeed -= PlayerThermometter.decreaseSpeed * (decreasePercent / 100);
                        PlayerThermometter.gainSpeed += PlayerThermometter.gainSpeed * (gainPercent / 100);
                        PlayerThermometter.rebootSpeed += PlayerThermometter.rebootSpeed * (rebootPercent / 100);
                        PlayerInventory.token--;
                    }
                }
                else
                {
                    ButtonText.color = new Color(0.5f, 0.5f, 0.5f);
                    ButtonText.text = "press A to upgrade Furnace";
                }
            }

            else if (hit.transform.CompareTag("HookShot"))
            {
                if (PlayerInventory.token >= hookShotPrice)
                {
                    ButtonText.color = new Color(0.5f, 1.0f, 0.5f);
                    ButtonText.text = "press A to purchase HookShot for " + hookShotPrice + " token";
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        PlayerHookShot.enabled = true;
                        PlayerInventory.token -= hookShotPrice;
                        Destroy(target);
                    }
                }
                else
                {
                    ButtonText.color = new Color(0.5f, 0.5f, 0.5f);
                    ButtonText.text = "press A to purchase HookShot for " + hookShotPrice + " token";
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
