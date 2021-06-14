using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageblePlayer : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHeath;
    public HealthBar healthBar;
    bool godMode = false;
    public Main main;
    void Start()
    {
        currentHeath = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    public void TakeDamage(float damage)
    {
        if (!godMode) { 
            currentHeath -= damage;
            healthBar.SetHealth(currentHeath);
            godMode = true;


            Invoke("OffGodMode", 2f);
           }
        if (currentHeath <= 0)
            Lose();
    }

    private void OffGodMode()
    {
        godMode = false;
    }

    void Lose()
    {
        main.GetComponent<Main>().Losing();
    }
}
