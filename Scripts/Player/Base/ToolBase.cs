using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolBase : MonoBehaviour
{
    [Header("Tool")]
    [SerializeField] protected DamageType damageType;

    public DamageType DamageType => damageType;

}