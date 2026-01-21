using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [Header("Objeto para copiar a posição")]
    [SerializeField] private Transform target;

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
        }
    }
}