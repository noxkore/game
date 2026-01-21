using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 0.8f;
    [SerializeField] private DamageType damageType;

    [Header("Brain")]
    [SerializeField] private GetterBrain getterBrain;

    private float timer;

    private Transform currentTarget;
    private IBreakable currentBreakable;

    private void Start()
    {
        getterBrain = GetComponent<GetterBrain>();
    }

    public void SetTarget(Transform target)
    {
        ClearTarget();

        currentTarget = target;

        if (currentTarget != null &&
            currentTarget.TryGetComponent(out currentBreakable))
        {
            currentBreakable.OnDestroyed += OnBreakableDestroyed;
        }
    }

    public void ClearTarget()
    {
        if (currentBreakable != null)
        {
            currentBreakable.OnDestroyed -= OnBreakableDestroyed;
            currentBreakable = null;
        }

        currentTarget = null;
    }

    private void Update()
    {
        if (currentTarget == null || currentBreakable == null)
            return;

        timer -= Time.deltaTime;

        float sqrDist =
            (currentTarget.position - transform.position).sqrMagnitude;

        if (sqrDist > attackRange * attackRange)
            return;

        if (timer > 0f)
            return;

        timer = attackCooldown;
        Attack();
    }

    private void Attack()
    {
        var context = new SufferContext
        {
            Type = damageType
        };

        currentBreakable.Suffer(damage, context);
    }

    private void OnBreakableDestroyed(Vector2 position)
    {
        getterBrain.OnResourceDestroyed(position);
        ClearTarget();
    }

    private void OnDisable()
    {
        ClearTarget();
    }
}