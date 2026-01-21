using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;

    [Header("Selection")]
    [SerializeField] private Image background;
    [SerializeField] private Color selectedColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.white;

    [HideInInspector] public int slotIndex;
    [HideInInspector] public BaseInventory boundInventory;


    private Transform originalParent;
    private Canvas rootCanvas;
    private Image dragIcon;

    private void Awake()
    {
        rootCanvas = GetComponentInParent<Canvas>();

        if (background != null)
            background.color = normalColor;
    }

    public void Clear()
    {
        icon.enabled = false;
        amountText.text = "";
    }

    public void Set(ItemData item, int amount)
    {
        icon.sprite = item.icon;
        icon.enabled = true;
        amountText.text = amount > 1 ? amount.ToString() : "";
    }

    public void SetSelected(bool selected)
    {
        if (background == null)
            return;

        background.color = selected ? selectedColor : normalColor;
    }

    // ───────── DRAG ─────────

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!icon.enabled) return;

        originalParent = icon.transform.parent;

        dragIcon = Instantiate(icon, rootCanvas.transform);
        dragIcon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            dragIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            Destroy(dragIcon.gameObject);
    }

    // ───────── DROP ─────────

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = eventData.pointerDrag?
            .GetComponent<InventorySlotUI>();

        if (draggedSlot == null)
            return;

        if (draggedSlot.boundInventory == null ||
            boundInventory == null)
            return;

        if (draggedSlot.boundInventory != boundInventory)
        {
            draggedSlot.boundInventory.SwapWith(
                draggedSlot.slotIndex,
                boundInventory,
                slotIndex
            );

            PlayerInventory.Instance?.NotifyChanged();

            draggedSlot
                .GetComponentInParent<MachineInventoryUI>()?
                .Refresh();

            GetComponentInParent<MachineInventoryUI>()?
                .Refresh();
        }
        else
        {
            boundInventory.SwapSlots(
                draggedSlot.slotIndex,
                slotIndex
            );

            PlayerInventory.Instance?.NotifyChanged();
        }
    }
}