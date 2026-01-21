using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUIBinder : MonoBehaviour
{
    [SerializeField] private int hotbarSize = 7;
    [SerializeField] private int hotbarStartIndex = 21;

    private PlayerInventory inventory;
    private HotbarController hotbar;
    private List<InventorySlotUI> slots;

    private Action<int> onHotbarSelectionChanged;


    private void Start()
    {
        inventory = PlayerInventory.Instance;
        hotbar = FindObjectOfType<HotbarController>();

        slots = new List<InventorySlotUI>(
            GetComponentsInChildren<InventorySlotUI>(true)
        );

        onHotbarSelectionChanged = _ => Refresh();

        inventory.OnInventoryChanged += Refresh;
        hotbar.OnSelectionChanged += onHotbarSelectionChanged;
        Refresh();
    }

    private void OnDestroy()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= Refresh;

        if (hotbar != null)
            hotbar.OnSelectionChanged -= onHotbarSelectionChanged;
    }

    private void Refresh()
    {
        int inventoryCount = inventory.Slots.Count;

        for (int i = 0; i < hotbarSize; i++)
        {
            if (i >= slots.Count)
                break;

            int inventoryIndex = hotbarStartIndex + i;
            var slotUI = slots[i];

            slotUI.slotIndex = inventoryIndex;
            slotUI.boundInventory = inventory; 

            if (inventoryIndex < 0 || inventoryIndex >= inventoryCount)
            {
                slotUI.Clear();
                slotUI.SetSelected(false);
                continue;
            }

            var invSlot = inventory.Slots[inventoryIndex];

            if (invSlot.IsEmpty)
                slotUI.Clear();
            else
                slotUI.Set(invSlot.item, invSlot.amount);

            slotUI.SetSelected(i == hotbar.SelectedIndex);
        }
    }

}