using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private int inventorySize = 21;
    private PlayerInventory playerInventory;
    private List<InventorySlotUI> slots;

    private void Awake()
    {
        slots = new List<InventorySlotUI>(
            GetComponentsInChildren<InventorySlotUI>(true)
        );
    }

    private void Start()
    {
        playerInventory = PlayerInventory.Instance;

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory não encontrado.");
            enabled = false;
            return;
        }

        playerInventory.OnInventoryChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        if (playerInventory != null)
            playerInventory.OnInventoryChanged -= Refresh;
    }

    private void Refresh()
    {
        var inventorySlots = playerInventory.Slots;

        for (int i = 0; i < slots.Count; i++)
        {
            if (i >= inventorySize)
            {
                slots[i].Clear();
                continue;
            }

            int inventoryIndex = i;
            slots[i].slotIndex = inventoryIndex;
            slots[i].boundInventory = playerInventory;

            if (inventoryIndex >= inventorySlots.Count ||
                inventorySlots[inventoryIndex].IsEmpty)
            {
                slots[i].Clear();
            }
            else
            {
                slots[i].Set(
                    inventorySlots[inventoryIndex].item,
                    inventorySlots[inventoryIndex].amount
                );
            }
        }
    }

}
