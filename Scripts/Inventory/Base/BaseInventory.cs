using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInventory : MonoBehaviour
{
    [SerializeField] protected int size = 20;
    public List<BaseInventorySlot> Slots { get; private set; }
    public bool IsFull
    {
        get
        {
            foreach (var slot in Slots)
            {
                if (slot.IsEmpty)
                    return false;
            }
            return true;
        }
    }
    protected virtual void Awake()
    {
        Slots = new List<BaseInventorySlot>(size);
        for (int i = 0; i < size; i++)
            Slots.Add(new BaseInventorySlot());
    }

    public bool TryAddItem(ItemData item, int amount)
    {
        int remaining = amount;

        foreach (var slot in Slots)
        {
            if (slot.CanStack(item))
            {
                remaining = slot.Add(remaining);
                if (remaining <= 0)
                {
                    OnItemAdded();
                    return true;
                }
            }
        }

        foreach (var slot in Slots)
        {
            if (slot.IsEmpty)
            {
                slot.item = item;
                remaining = slot.Add(remaining);
                if (remaining <= 0)
                {
                    OnItemAdded();
                    return true;
                }
            }
        }

        return false;
    }

    public virtual void SwapSlots(int from, int to)
    {
        if (from == to) return;

        var temp = Slots[from];
        Slots[from] = Slots[to];
        Slots[to] = temp;

    }

    protected virtual void OnItemAdded() { }

    public virtual void ClearInventory()
    {
        foreach (var slot in Slots)
            slot.Clear();

    }

    public void SwapWith(int fromIndex, BaseInventory other, int toIndex)
    {
        if (other == null)
            return;

        if (fromIndex < 0 || fromIndex >= Slots.Count)
            return;

        if (toIndex < 0 || toIndex >= other.Slots.Count)
            return;

        var fromSlot = Slots[fromIndex];
        var toSlot = other.Slots[toIndex];

        // Nada para trocar
        if (fromSlot.IsEmpty && toSlot.IsEmpty)
            return;

        // ───────── SWAP ─────────
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

    }
}