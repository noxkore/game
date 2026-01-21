using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseInventorySlot
{
    public ItemData item;
    public int amount;

    public bool IsEmpty => item == null;

    public bool CanStack(ItemData newItem)
    {
        if (item == null) return false;
        return item == newItem && amount < item.maxStack;
    }

    public int Add(int value)
    {
        int space = item.maxStack - amount;
        int added = Math.Min(space, value);
        amount += added;
        return value - added;
    }

    public void Clear()
    {
        amount = 0;
        item = null;
    }
}
