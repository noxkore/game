using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerToolUser : MonoBehaviour
{
    [Header("Player DamageDealer")]
    [SerializeField] private DamageDealer damageDealer; // DamageDealer único do player

    [Header("Hand (default tool)")]
    [SerializeField] private ToolBase hand; // Ferramenta "mão" default

    private ToolBase currentTool;       // Ferramenta selecionada da hotbar
    private HotbarController hotbar;

    private void Start()
    {
        hotbar = FindObjectOfType<HotbarController>();
        if (hotbar == null)
            Debug.LogError("HotbarController não encontrado na cena!");
    }

    private void Update()
    {
        UpdateCurrentToolFromHotbar();

        if (Input.GetMouseButtonDown(0))
        {
            // Se tiver ferramenta selecionada, usa o tipo de dano dela
            if (currentTool != null)
            {
                damageDealer.SetDamageType(currentTool.DamageType);
                damageDealer.TryHit();
            }
            else if (hand != null)
            {
                // Se não tiver ferramenta, usa a mão
                damageDealer.SetDamageType(hand.DamageType);
                damageDealer.TryHit();
            }
        }
    }

    /// <summary>
    /// Atualiza a ferramenta atual baseada no slot selecionado da hotbar
    /// </summary>
    private void UpdateCurrentToolFromHotbar()
    {
        var slot = hotbar.GetSelectedSlot();

        if (slot != null && !slot.IsEmpty && slot.item.prefab != null)
        {
            ToolBase tool = slot.item.prefab.GetComponent<ToolBase>();
            if (tool != null)
            {
                currentTool = tool;
                return;
            }
        }

        // Nenhuma ferramenta selecionada
        currentTool = null;
    }
}
