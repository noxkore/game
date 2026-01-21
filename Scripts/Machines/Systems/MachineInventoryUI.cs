using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineInventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotsParent;

    private BaseInventory boundInventory;
    private List<InventorySlotUI> slots = new List<InventorySlotUI>();

    private bool isOpen;

    public void Bind(BaseInventory inventory)
    {
        boundInventory = inventory;

        ClearSlots();
        CreateSlots();
        Refresh();
    }

    private void Update()
    {
        if (!isOpen)
            return;

        Refresh();
    }

    public void Refresh()
    {
        if (boundInventory == null)
            return;

        for (int i = 0; i < slots.Count; i++)
        {
            var uiSlot = slots[i];
            var dataSlot = boundInventory.Slots[i];

            if (dataSlot.IsEmpty)
                uiSlot.Clear();
            else
                uiSlot.Set(dataSlot.item, dataSlot.amount);
        }
    }

    private void CreateSlots()
    {
        for (int i = 0; i < boundInventory.Slots.Count; i++)
        {
            var slotUI = Instantiate(slotPrefab, slotsParent);

            slotUI.slotIndex = i;
            slotUI.boundInventory = boundInventory;

            slotUI.Clear();
            slots.Add(slotUI);
        }
    }

    private void ClearSlots()
    {
        foreach (var slot in slots)
            Destroy(slot.gameObject);

        slots.Clear();
    }

    public void Open()
    {
        isOpen = true;
        slotsParent.gameObject.SetActive(true);
        Refresh();
    }

    public void Close()
    {
        isOpen = false;
        slotsParent.gameObject.SetActive(false);
    }
}