using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickageOnlyModifier : BaseSufferModifier
{
    [SerializeField] private float blockedDamage = 0f;

    public override float Modify(float baseAmount, SufferContext context)
    {
        if (context.Type == null)
            return blockedDamage;

        if (context.Type.elementName != "Pickaxe")
            return blockedDamage;

        return baseAmount;
    }
}