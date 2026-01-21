using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    private DamageType damageType;

    [Header("Raycast")]
    [SerializeField] private Transform origin;
    [SerializeField] private float distance = 1.2f;
    [SerializeField] private LayerMask hitMask;

    public void SetDamageType(DamageType type)
    {
        damageType = type;
    }

    public void TryHit()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - origin.position).normalized;

        Debug.DrawRay(origin.position, direction * distance, Color.red, 0.2f);

        RaycastHit2D hit = Physics2D.Raycast(
            origin.position,
            direction,
            distance,
            hitMask
        );

        if (!hit)
        {
            print("Nada Atingido (DamageDealer)");
            return;
        }

        if (hit.collider.TryGetComponent<IBreakable>(out var breakable))
        {
            print("Raycast Atingiu (DamageDealer)");

            var context = new SufferContext
            {
                Type = damageType
            };

            breakable.Suffer(damage, context);
        }
    }
}