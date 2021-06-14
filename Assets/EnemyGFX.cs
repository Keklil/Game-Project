using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyGFX : MonoBehaviour
{
    private string currentState;
    public float attackRange;
    public Transform attackPoint;
    public float damage;
    public LayerMask Damageble;
    private float timer;
    public float TimeAttack = 0.875f;
    public Animator anim;
    public Transform player;
    public AIPath aiPath;
    private bool isAttack = false;
    private bool isAttackPressed = false;

    const string RUN = "RUN";
    const string ATTACK = "ATTACK";

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); 
        }
        
        

        if (!isAttack) 
        { 
            ChangeAnimationState(RUN);
        }

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            isAttackPressed = true;
        }

        if (timer <= 0)
        {
            if (isAttackPressed)
            {
                isAttackPressed = false;
                if (!isAttack)
                {
                    isAttack = true;
                        Attack();
                        ChangeAnimationState(ATTACK);
                }
                Invoke("AttackComplete", TimeAttack);
                timer = TimeAttack;
            }
        }
        else
            timer -= Time.deltaTime;

    }
    void Attack()
    {
        if (timer <= 0)
        {           
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {       
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
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void AttackComplete()
    {
        isAttack = false;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }
}
