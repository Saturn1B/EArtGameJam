using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu, LooseMenu, WinMenu, HUD;
    public Mining mining;
    public Upgrades upgrades;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HUD.SetActive(false);
            PauseMenu.SetActive(true);
            mining.enabled = false;
            upgrades.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        HUD.SetActive(true);
        PauseMenu.SetActive(false);
        mining.enabled = true;
        upgrades.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void Continue()
    {
        HUD.SetActive(true);
        //ContinueMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
