using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : BreakableObject
{
    [Header("Drop")]
    [SerializeField] private GameObject woodDropPrefab;
    [SerializeField] private int dropAmount = 3;

    public override void Break()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Instantiate(
                woodDropPrefab,
                transform.position + Random.insideUnitSphere * 0.3f,
                Quaternion.identity
            );
        }
        OnDestroyedInvoke();
        Destroy(gameObject);
    }
}