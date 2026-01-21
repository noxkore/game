using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetterMachine : BaseMachine
{
    [Header("Getter Settings")]
    [SerializeField] private int maxStoredItems = 5;

    public bool IsStorageFull
    {
        get
        {
            int currentAmount = 0;
            foreach (var slot in inventory.Slots)
            {
                currentAmount += slot.amount;
            }
            return currentAmount >= maxStoredItems;
        }
    }

    public override bool CanInput(ICollectable collectable)
    {
        if (collectable == null)
            return false;

        return !IsStorageFull;
    }

    public override bool TryInput(ICollectable collectable)
    {
        if (!CanInput(collectable))
            return false;

        bool added = inventory.TryAddItem(
            collectable.Item,
            collectable.Amount
        );

        return added;
    }

    public override bool CanOutput()
    {
        foreach (var slot in inventory.Slots)
        {
            if (!slot.IsEmpty)
                return true;
        }
        return false;
    }

    public override ICollectable PeekOutput()
    {
        return null;
    }

    public override ICollectable TakeOutput()
    {
        return null;
    }

    public override void Execute(float deltaTime)
    {
    }
}
