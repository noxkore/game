using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeOnlyModifier : BaseSufferModifier
{
    [SerializeField] private float blockedDamage = 0f;

    public override float Modify(float baseAmount, SufferContext context)
    {
        if (context.Type == null)
            return blockedDamage;

        if (context.Type.elementName != "Axe") 
            return blockedDamage;

        return baseAmount;
    }
}