using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResourcecs : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float collectRadius = 0.5f;
    [SerializeField] private LayerMask collectableLayer;

    private IMachine machine;

    private void Awake()
    {
        machine = GetComponent<IMachine>();

        if (machine == null)
            Debug.LogError("CollectResource precisa estar no mesmo GameObject de um IMachine");
    }

    private void Update()
    {
        TryCollect();
    }

    private void TryCollect()
    {
        if (machine == null)
            return;

        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            collectRadius,
            collectableLayer
        );

        if (hit == null)
            return;

        var collectable = hit.GetComponent<ICollectable>();
        if (collectable == null)
            return;

        if (!machine.CanInput(collectable))
            return;

        if (machine.TryInput(collectable))
        {
            Destroy(hit.gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collectRadius);
    }
#endif
}