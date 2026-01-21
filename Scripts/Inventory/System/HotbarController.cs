using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarController : MonoBehaviour
{
    [SerializeField] private int hotbarStartIndex = 21;
    [SerializeField] private int hotbarSize = 7;

    public int SelectedIndex { get; private set; }

    public event Action<int> OnSelectionChanged;

    private PlayerInventory inventory;

    private void Awake()
    {
        inventory = PlayerInventory.Instance;
    }

    private void Start()
    {
        Select(0);
    }

    private void Update()
    {
        for (int i = 0; i < hotbarSize; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Select(i);
                return;
            }
        }

        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0)
            Select((SelectedIndex - 1) % hotbarSize);
        else if (scroll < 0)
            Select((SelectedIndex + 1 + hotbarSize) % hotbarSize);
    }

    public void Select(int index)
    {
        if (index < 0 || index >= hotbarSize)
            return;

        SelectedIndex = index;
        OnSelectionChanged?.Invoke(index);
    }

    public BaseInventorySlot GetSelectedSlot()
    {
        int inventoryIndex = hotbarStartIndex + SelectedIndex;
        return inventory.Slots[inventoryIndex];
    }
}