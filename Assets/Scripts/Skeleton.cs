using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    bool moveingRight;
    Transform player;
    public float stoppingDistance;
    bool angry = false;
    bool goBack = false;
    bool idle = false;
    public float attackRange;
    public Transform attackPoint;
    public float damage;
    public LayerMask Damageble;
    private float timer;
    public float TimeAttack = 0.875f;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
            idle = true;

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance) { 
            angry = true;
            idle = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance) { 
            goBack = true;
            angry = false;
        }

        if (idle == true)
            Idle();
        else if (angry == true)
            Angry();
        else if (goBack == true)
            GoBack();
        
        Attack();
        
    }

    void Idle() 
    {
        anim.SetInteger("State", 1);
        if (transform.position.x > point.position.x + positionOfPatrol) { 
            moveingRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < point.position.x - positionOfPatrol) { 
            moveingRight = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (moveingRight)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    void Angry() {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (moveingRight)
            transform.localScale = new Vector3(1f, 1f, 1f);
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //transform.localRotation = Quaternion.Euler(0, 180, 0);

    }

    void GoBack() {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        
        
        if (timer <= 0)
        {
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {   
                anim.SetInteger("State", 2);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Damageble);

                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<DamageblePlayer>().TakeDamage(damage);
                    }
                }
                
            }
            timer = TimeAttack;
        } else 
            {
                timer -= Time.deltaTime;
            }
    }

}
