using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu, Credit, Panel, Explication, Control;

    public void Play()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(LaunchGame());
    }

    public void LaunchCredit()
    {
        Menu.SetActive(false);
        Credit.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        Credit.SetActive(false);
        Menu.SetActive(true);
    }

    IEnumerator LaunchGame()
    {
        Menu.SetActive(false);
        Panel.SetActive(true);
        Explication.SetActive(true);
        yield return new WaitForSeconds(15);
        Explication.SetActive(false);
        Control.SetActive(true);
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(1);
    }
}
