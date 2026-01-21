using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseSufferPipeline))]
[RequireComponent(typeof(BaseHealthComponent))]
public abstract class BreakableObject : MonoBehaviour, IBreakable
{
    protected BaseSufferPipeline sufferPipeline;
    protected BaseHealthComponent health;

    public event Action<Vector2> OnDestroyed;

    protected virtual void OnDestroyedInvoke()
    {
        OnDestroyed?.Invoke(transform.position);
    }

    protected virtual void Awake()
    {
        sufferPipeline = GetComponent<BaseSufferPipeline>();
        health = GetComponent<BaseHealthComponent>();

        health.OnDeath += Break;
    }

    protected virtual void OnDestroy()
    {
        if (health != null)
            health.OnDeath -= Break;
    }

    public virtual void Suffer(float amount, SufferContext context)
    {
        if (context.Equals(default))
            context = SufferContext.Default;

        sufferPipeline.Suffer(amount, context);
    }

    public abstract void Break();
}