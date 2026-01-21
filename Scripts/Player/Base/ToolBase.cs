using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolBase : MonoBehaviour, ITool
{
    [Header("Tool")]
    [SerializeField] protected DamageDealer damageDealer;
    [SerializeField] protected DamageType damageType;

    public DamageType DamageType => damageType;

    protected virtual void Awake()
    {
        if (damageDealer != null)
            damageDealer.SetDamageType(damageType);
    }

    public virtual void Use()
    {
        print("Usando Ferramenta (ToolBase)");
        damageDealer?.TryHit();
    }
}