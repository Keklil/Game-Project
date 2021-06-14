using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public LayerMask Damageble;
    float timeToDisable = 1.1f;
    public float damage;
    public Transform spellPoint;
    private Vector2 spellRange;
    private float timer;
    private float cooldown = 2f;


    void Start()
    {
        /*Spell spell = new Spell();
        cooldown = spell.cooldown;*/
        spellRange = new Vector3(1f, 2f);      
        Invoke("SpellDestroy", timeToDisable);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spellPoint.position, new Vector3(1, 2));
    }

    void Update()
    {
        Invoke("Hit", 0.3f);
    }

    void Hit()
    {
        if (timer <= 0)
        {

            Collider2D[] enemies = Physics2D.OverlapAreaAll(spellPoint.position, spellRange, Damageble);

            if (enemies.Length != 0)
            {
                for (int i = 0; i < enemies.Length; i++)
                    enemies[i].GetComponent<DamagebleObject>().TakeDamage(damage);
            }
            timer = cooldown;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void SpellDestroy() {
        Destroy(gameObject);
    }

}
