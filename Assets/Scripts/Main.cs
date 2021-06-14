using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public Player player;
    public GameObject PauseScreen;
    public GameObject LoseScreen;

    void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseScreen.activeSelf)
            PauseOn();
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseScreen.activeSelf)
            PauseOff();
    }

    public void ReloadLvl()
    {     
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
        player.enabled = true;
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        PauseScreen.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        PauseScreen.SetActive(false);
    }

    public void Losing()
    {
        Time.timeScale = 0f;
        LoseScreen.SetActive(true);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

}
