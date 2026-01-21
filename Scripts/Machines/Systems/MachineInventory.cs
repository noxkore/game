using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MachineInventory : BaseInventory
{
    public event Action OnInventoryChanged;

    protected override void OnItemAdded()
    {
        OnInventoryChanged?.Invoke();
    }

    /// <summary>
    /// Swap interno (máquina com máquina)
    /// </summary>
    public override void SwapSlots(int from, int to)
    {
        base.SwapSlots(from, to);
        OnInventoryChanged?.Invoke();
    }

    /// <summary>
    /// Swap entre inventários (Player <-> Machine)
    /// </summary>
    public new void SwapWith(int fromIndex, BaseInventory other, int toIndex)
    {
        if (other == null)
            return;

        if (fromIndex < 0 || fromIndex >= Slots.Count)
            return;

        if (toIndex < 0 || toIndex >= other.Slots.Count)
            return;

        var fromSlot = Slots[fromIndex];
        var toSlot = other.Slots[toIndex];

        if (fromSlot.IsEmpty && toSlot.IsEmpty)
            return;

        ItemData tempItem = fromSlot.item;
        int tempAmount = fromSlot.amount;

        fromSlot.item = toSlot.item;
        fromSlot.amount = toSlot.amount;

        toSlot.item = tempItem;
        toSlot.amount = tempAmount;

        if (fromSlot.amount <= 0)
            fromSlot.Clear();

        if (toSlot.amount <= 0)
            toSlot.Clear();

        OnInventoryChanged?.Invoke();

        if (other is PlayerInventory pi)
            pi.NotifyChanged();
        else if (other is MachineInventory mi)
            mi.OnInventoryChanged?.Invoke();
    }

    public override void ClearInventory()
    {
        foreach (var slot in Slots)
            slot.Clear();

        OnInventoryChanged?.Invoke();
    }
}