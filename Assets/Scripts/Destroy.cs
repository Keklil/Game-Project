using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    public Main main;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Столкновение");
            Invoke("Lose", 0.1f);
            //SceneManager.LoadScene("Level1");
        }
    }
    void Lose()
    {
        main.GetComponent<Main>().Losing();
    }
}
