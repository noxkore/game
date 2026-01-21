using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] protected ItemData item;
    [SerializeField] protected int amount = 1;

    public ItemData Item => item;
    public int Amount => amount;
    public Transform Transform => transform;

    public void Initialize(ItemData itemData, int itemAmount)
    {
        item = itemData;
        amount = itemAmount;
    }

    public virtual void Collect(BaseInventory inventory)
    {
        if (inventory.TryAddItem(item, amount))
        {
            OnCollected();
        }
    }

    protected virtual void OnCollected()
    {
        Destroy(gameObject);
    }
}