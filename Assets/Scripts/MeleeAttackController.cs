using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    public Transform AttackPoint;
    public LayerMask Damageble;
    public float Damage;
    public float AttackRange;
    public float TimeAttack = 0.5f;
    private Vector3 mousePos;
    private Vector3 playerPos;
    public Transform player;

    private float timer;
    
    void Start()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    void Update()
    {
        Attack();

    }

    public void Attack() 
    {   
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPos = player.transform.position;

        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {               
                //print("Mouse position: " + mousePos);               
                //print("Player position: " + playerPos);
                Flip();


                Collider2D[] enemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, Damageble);

                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<DamagebleObject>().TakeDamage(Damage);
                    }
                }

                timer = TimeAttack;
            }
        }
        else 
        {
            timer -= Time.deltaTime;
        }


    }

    void Flip()
    {
        if (mousePos.x < playerPos.x && player.transform.localRotation.y == 0)
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
        if (mousePos.x > playerPos.x && player.transform.localRotation.y != 0)
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }


}

