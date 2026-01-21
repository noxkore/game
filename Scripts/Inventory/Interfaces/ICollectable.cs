using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    Transform Transform { get; }
    ItemData Item { get; }
    int Amount { get; }

    void Collect(BaseInventory inventory);
}