using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    public Sprite sprite;
    private SpriteRenderer spriteRenderer;
    public bool act = false;
    public GameObject wall;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Act();
        }
            
    }
    
    public void Act()
    {
        spriteRenderer.sprite = sprite;
        Destroy(wall);
    }
}
