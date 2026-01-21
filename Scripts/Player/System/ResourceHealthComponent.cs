using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHealthComponent : BaseHealthComponent
{
    [Header("Resource Health")]
    [SerializeField] private float initialHealth = 5f;

    protected virtual void Awake()
    {
        maxHealth = initialHealth;
        currentHealth = initialHealth;

        InvokeHealthChanged();
    }

    public override void Heal(float ammount)
    {
        currentHealth = Mathf.Min(currentHealth + ammount, maxHealth);

        InvokeHealthChanged();
        InvokeHealed(ammount);
    }

    public override void Die()
    {
        InvokeDeath();
    }
}