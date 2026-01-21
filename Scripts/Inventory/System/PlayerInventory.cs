using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : BaseInventory
{
    public static PlayerInventory Instance { get; private set; }

    public event Action OnInventoryChanged;

    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        base.Awake();
    }

    protected override void OnItemAdded()
    {
        OnInventoryChanged?.Invoke();
    }

    public override void SwapSlots(int from, int to)
    {
        if (from == to) return;

        var temp = Slots[from];
        Slots[from] = Slots[to];
        Slots[to] = temp;

        OnInventoryChanged?.Invoke();
    }
    public void NotifyChanged()
    {
        OnInventoryChanged?.Invoke();
    }
}