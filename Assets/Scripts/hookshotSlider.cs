using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hookshotSlider : MonoBehaviour
{
    public float maxValue;
    public Image fill;

    public float currentValue;

    // Start is called before the first frame update
    void Start()
    {
        currentValue = maxValue;
        fill.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Add(10.0f);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Deduct(10.0f);
        }
    }

    public void Add(float i)
    {
        currentValue += i;

        if(currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        fill.fillAmount = currentValue / maxValue;
    }

    public void Deduct(float i)
    {
        currentValue -= i;

        if (currentValue < 0)
        {
            currentValue = 0;
        }
        fill.fillAmount = currentValue / maxValue;
    }
}
