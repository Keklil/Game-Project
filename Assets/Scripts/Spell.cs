using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private LayerMask dashLayerMask;

    private Vector2 mouse;
    private Vector3 mousePos;
    private Vector2 spellPosition;
    public GameObject hand;
    float distance = Mathf.Infinity;
    private float timer;
    public float cooldown = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D rc = Physics2D.Raycast(mousePos, -Vector2.up, distance, dashLayerMask);
                spellPosition = new Vector2(rc.point.x, rc.point.y + 2.78f);
                Instantiate(hand, spellPosition, Quaternion.identity);              
                timer = cooldown;
            }
        }
        else 
        {
            timer -= Time.deltaTime;
        }
    }

    
}
