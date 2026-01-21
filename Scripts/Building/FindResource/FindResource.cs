using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindResource : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private LayerMask searchLayer;
    [SerializeField] private string targetTag = "Tree";
    [SerializeField] private int maxResults = 128;

    private Collider2D[] results;

    private void Awake()
    {
        results = new Collider2D[maxResults];
    }

    public Transform FindClosest()
    {
        int count = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            maxDistance,
            results,
            searchLayer
        );

        Transform closest = null;
        float closestSqrDist = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (!results[i].CompareTag(targetTag))
                continue;

            float sqrDist = (results[i].transform.position - transform.position).sqrMagnitude;

            if (sqrDist < closestSqrDist)
            {
                closestSqrDist = sqrDist;
                closest = results[i].transform;
            }
        }

        return closest;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
#endif
}