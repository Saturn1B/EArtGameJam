using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreManager : MonoBehaviour
{
    public GameObject WinMenu, HUD;
    public Mining mining;
    public Upgrades upgrades;

    public List<GameObject> ores;

    // Update is called once per frame
    void Update()
    {
        if(ores.Count <= 0)
        {
            HUD.SetActive(false);
            WinMenu.SetActive(true);
            mining.enabled = false;
            upgrades.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}
