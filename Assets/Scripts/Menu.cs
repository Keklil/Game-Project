using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Exit();
    }

    public void OpenScene(int index) {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void ExitButton()
    {
            Application.Quit();
    }
}
