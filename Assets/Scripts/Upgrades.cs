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
    int hookShotPrice, elevatorPrice;

    [SerializeField]
    LayerMask layer, elevatorLayer;

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
                        float upValue = PlayerMovements.StaminaSlider.maxValue * (speedPercent / 100);
                        PlayerMovements.StaminaSlider.maxValue += upValue;
                        PlayerMovements.StaminaSlider.value = PlayerMovements.StaminaSlider.maxValue;
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
                        ButtonText.text = "";
                    }
                }
                else
                {
                    ButtonText.color = new Color(0.5f, 0.5f, 0.5f);
                    ButtonText.text = "press A to purchase HookShot for " + hookShotPrice + " token";
                }
            }

            else if (hit.transform.CompareTag("Elevator"))
            {
                if (!hit.transform.GetChild(0).GetComponent<Elevator>().isActivated)
                {
                    if (PlayerInventory.token >= elevatorPrice)
                    {
                        ButtonText.color = new Color(0.5f, 1.0f, 0.5f);
                        ButtonText.text = "press A to purchase Elevator for " + elevatorPrice + " token";
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            hit.transform.GetChild(0).GetComponent<Elevator>().isActivated = true;
                            PlayerInventory.token -= elevatorPrice;
                            ButtonText.text = "";
                        }
                    }
                    else
                    {
                        ButtonText.color = new Color(0.5f, 0.5f, 0.5f);
                        ButtonText.text = "press A to purchase Elevator for " + elevatorPrice + " token";
                    }
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

        RaycastHit hit2;
        if (Physics.Raycast(transform.position, gameObject.transform.GetChild(0).transform.forward, out hit2, maxDistance, elevatorLayer))
        {
            if(Input.GetMouseButtonDown(0) && hit2.transform.GetComponent<Elevator>().isActivated && hit2.transform.GetComponent<Elevator>().canPress)
            {
                hit2.transform.GetComponent<Elevator>().UseElevator();
            }
        }
        
    }
}
