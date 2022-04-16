using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;

    private bool isDead;

    void Start()
    {
        currHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float value)
    {
        currHealth -= value;
        if (currHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
