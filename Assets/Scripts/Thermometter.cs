using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thermometter : MonoBehaviour
{
    [SerializeField]
    float startValue;

    [SerializeField]
    float percent;

    [SerializeField]
    Slider PlayerThermometter;

    GameObject Player;
    Inventory PlayerInventory;

    bool stopDepleting;

    public float decreaseSpeed, gainSpeed, rebootSpeed;

    public Animator furnace;

    public GameObject LooseScreen, HUD;

    public Mining mining;
    public Upgrades upgrades;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerInventory = Player.GetComponent<Inventory>();
        PlayerThermometter.value = startValue;
    }

    void Update()
    {
        if (!stopDepleting)
        {
            PlayerThermometter.value -= decreaseSpeed;
        }

        if(PlayerThermometter.value <= 0)
        {
            HUD.SetActive(false);
            LooseScreen.SetActive(true);
            mining.enabled = false;
            upgrades.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        
        if(PlayerThermometter.value < PlayerThermometter.maxValue * 0.3)
        {
            furnace.SetBool("slow", true);
            furnace.SetBool("fast", false);

        }
        else if(PlayerThermometter.value < PlayerThermometter.maxValue * 0.6)
        {
            furnace.SetBool("slow", false);
            furnace.SetBool("fast", false);
        }
        else
        {
            furnace.SetBool("slow", false);
            furnace.SetBool("fast", true);
        }
    }

    public IEnumerator AddTemperature(float value)
    {
        stopDepleting = true;

        float temp = PlayerThermometter.value + value;
        float diff = PlayerThermometter.maxValue - temp;

        if(diff < 0)
        {
            temp += diff;
            temp -= 0.1f;
        }

        float upTime = 1;

        while (PlayerThermometter.value < temp)
        {
            PlayerThermometter.value += gainSpeed * Mathf.Exp(upTime);
            upTime += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.1f);

        if (PlayerThermometter.value >= PlayerThermometter.maxValue || diff < 0)
        {
            float downTime = 1;
            PlayerInventory.token++;
            while (PlayerThermometter.value > startValue)
            {
                PlayerThermometter.value -= rebootSpeed * Mathf.Exp(downTime);
                downTime += 0.05f;
                yield return new WaitForSeconds(0.01f);
            }
            PlayerThermometter.value = startValue;
            PlayerThermometter.maxValue += PlayerThermometter.maxValue * (percent / 100);
        }

        stopDepleting = false;
    }
}
