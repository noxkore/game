using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortPlayerOrder : MonoBehaviour
{
    [Header("Sorting")]
    [SerializeField] private int sortingFactor = 100;
    [SerializeField] private int sortingOffset = 0;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder =
            -(int)(transform.position.y * sortingFactor) + sortingOffset;
    }
}