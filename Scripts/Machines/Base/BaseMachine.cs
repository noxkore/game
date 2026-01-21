using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMachine : MonoBehaviour, IMachine
{
    [Header("Machine Inventory")]
    [SerializeField] protected BaseInventory inventory;

    public BaseInventory Inventory => inventory;

    protected virtual void Awake()
    {
        if (inventory == null)
            inventory = GetComponent<BaseInventory>();
    }

    public abstract bool CanInput(ICollectable collectable);
    public abstract bool TryInput(ICollectable collectable);

    public abstract bool CanOutput();
    public abstract ICollectable PeekOutput();
    public abstract ICollectable TakeOutput();

    public abstract void Execute(float deltaTime);
}