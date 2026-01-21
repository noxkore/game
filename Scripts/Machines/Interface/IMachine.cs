using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMachine
{
    BaseInventory Inventory { get; }

    bool CanInput(ICollectable collectable);
    bool TryInput(ICollectable collectable);

    bool CanOutput();
    ICollectable PeekOutput();
    ICollectable TakeOutput();

    void Execute(float deltaTime);
}