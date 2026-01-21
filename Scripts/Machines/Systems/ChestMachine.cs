using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMachine : BaseMachine
{

    public override bool CanInput(ICollectable collectable)
    {
        // Sempre aceita qualquer coletável
        return collectable != null;
    }

    public override bool TryInput(ICollectable collectable)
    {
        if (collectable == null)
            return false;

        return inventory.TryAddItem(
            collectable.Item,
            collectable.Amount
        );
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
        // Baú não gera collectable sozinho
        return null;
    }

    public override ICollectable TakeOutput()
    {
        // Output manual (player drag & drop)
        return null;
    }

    public override void Execute(float deltaTime)
    {
        // Baú não processa nada
    }
}