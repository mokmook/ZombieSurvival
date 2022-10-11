using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class LivingEntity : MonoBehaviour , IDamageable
{
    [SerializeField] float startingHealth = 100f;
    public float _startingHealth => startingHealth;
    public float health { get; protected set; }
    public bool dead { get; protected set; }


    public event Action onDeath;


    protected virtual void onEnable()
    {
        dead = false;

        health = startingHealth;
    }
    
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health -= damage;

        

        if (health <= 0 && !dead)
            Die();

    }
    protected virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;



        health += newHealth;
    }

    protected virtual void Die()
    {
        if (onDeath != null)
            onDeath();
        

        dead = true;
    }


}
