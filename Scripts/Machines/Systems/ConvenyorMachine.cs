using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMachine : BaseMachine
{
    [Header("Conveyor Settings")]
    [SerializeField] private float transferSpeed = 1f;  // itens por segundo
    [SerializeField] private float detectDistance = 1f;  // distância para detectar máquina à frente
    [SerializeField] private Vector2 direction = Vector2.up;

    private float transferTimer = 0f;

    private void Update()
    {
        Execute(Time.deltaTime);
    }

    protected override void Awake()
    {
        base.Awake();

        if (inventory == null)
            inventory = GetComponent<MachineInventory>();
    }

    private BaseMachine GetMachineAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectDistance, LayerMask.GetMask("Machine"));

        if (hit.collider != null)
        {
            BaseMachine machine = hit.collider.GetComponent<BaseMachine>();
            if (machine != null && machine != this)
            {
                print("Found Machine Ahead: " + machine.name);
                return machine;
            }
        }

        return null;
    }

    private BaseInventorySlot GetFirstNonEmptySlot()
    {
        foreach (var slot in inventory.Slots)
            if (!slot.IsEmpty)
                return slot;
        return null;
    }

    private bool InventoryHasItems()
    {
        return GetFirstNonEmptySlot() != null;
    }

    public override bool CanInput(ICollectable collectable)
    {
        foreach (var slot in inventory.Slots)
        {
            if (slot.IsEmpty || (slot.item == collectable.Item && slot.amount < slot.item.maxStack))
                return true;
        }
        return false;
    }

    public override bool TryInput(ICollectable collectable)
    {
        if (CanInput(collectable))
        {
            inventory.TryAddItem(collectable.Item, 1);
            return true;
        }
        return false;
    }

    public override bool CanOutput()
    {
        BaseMachine machineAhead = GetMachineAhead();
        return InventoryHasItems() && machineAhead != null;
    }

    public override ICollectable PeekOutput()
    {
        // Apenas retorna ItemData, sem instanciar prefab
        var slot = GetFirstNonEmptySlot();
        if (slot == null) return null;

        // Cria CollectOnEnter temporário só quando enviar
        return null;
    }

    public override ICollectable TakeOutput()
    {
        var slot = GetFirstNonEmptySlot();
        if (slot == null) return null;

        // Remove do slot
        slot.amount -= 1;
        ItemData itemData = slot.item;
        if (slot.amount <= 0)
            slot.Clear();

        // Instancia o prefab real, que já contém BaseCollectable / CollectOnEnter
        GameObject go = Instantiate(itemData.prefab);
        ICollectable collectable = go.GetComponent<ICollectable>();

        return collectable;
    }

    public override void Execute(float deltaTime)
    {
        transferTimer += deltaTime;
        float transferInterval = 1f / transferSpeed;

        if (transferTimer < transferInterval)
            return;

        transferTimer = 0f;

        BaseMachine machineAhead = GetMachineAhead();
        if (!InventoryHasItems() || machineAhead == null)
            return;

        ICollectable itemToSend = TakeOutput();

        if (itemToSend != null)
        {
            bool sent = machineAhead.TryInput(itemToSend);

            if (!sent)
            {
                print("fudeu");
            }
        }
    }
}
