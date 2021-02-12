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

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player");
        PlayerInventory = Player.GetComponent<Inventory>();
        PlayerThermometter.value = startValue;
    }

    void Update()
    {
        PlayerThermometter.value -= 0.01f;
    }

    public IEnumerator AddTemperature(float value)
    {
        float temp = PlayerThermometter.value + value;
        float diff = PlayerThermometter.maxValue - temp;

        if(diff < 0)
        {
            temp += diff;
            temp -= 0.1f;
        }

        while (PlayerThermometter.value < temp)
        {
            PlayerThermometter.value += 1.5f;
            yield return new WaitForSeconds(0.01f);
        }
        PlayerThermometter.value = temp;

        yield return new WaitForSeconds(0.1f);

        if (PlayerThermometter.value >= PlayerThermometter.maxValue || diff < 0)
        {
            PlayerInventory.token++;
            while (PlayerThermometter.value > startValue)
            {
                PlayerThermometter.value -= 2.5f;
                yield return new WaitForSeconds(0.01f);
            }
            PlayerThermometter.value = startValue;
            PlayerThermometter.maxValue += PlayerThermometter.maxValue * (percent / 100);
        }
    }
}
