using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagebleObject : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    private Material matBlink;
    private Material matDefault;
    public float maxHealthE = 65f;
    public float currentHeath;
    public HealthBar healthBar;
    public Animator anim;

    void Start()
    {
        currentHeath = maxHealthE;
        healthBar.SetMaxHealth(maxHealthE);
        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        currentHeath -= damage;
        healthBar.SetHealth(currentHeath);
        spriteRend.material = matBlink;
        Invoke("ResetMaterial", 0.5f);
        if (currentHeath <= 0)
            Die();

        print("Hit");
    }

    void ResetMaterial() {
        spriteRend.material = matDefault;
    }

    private void Die()
    {
        anim.SetInteger("State", 3);
        Destroy(gameObject, 1f);
    }
}
