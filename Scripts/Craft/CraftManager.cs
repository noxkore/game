using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public static CraftManager Instance { get; private set; }

    private PlayerInventory playerInventory;

    public CraftRecipe recipeToTest; 

    public void TryCraftButton()
    {
        TryCraft(recipeToTest);
    }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        playerInventory = PlayerInventory.Instance;

        if (playerInventory == null)
            Debug.LogError("CraftManager: PlayerInventory não encontrado!");
    }

    /// <summary>
    /// Tenta craftar o item do recipe. Retorna true se deu certo.
    /// </summary>
    public bool TryCraft(CraftRecipe recipe)
    {
        if (!CanCraft(recipe))
        {
            Debug.Log($"Craft impossível: {recipe.RecipeName}");
            return false;
        }

        // Remove os ingredientes
        foreach (var ingredient in recipe.Ingredients)
        {
            RemoveItemsFromInventory(ingredient.Item, ingredient.Amount);
        }

        // Adiciona o item final ao inventário
        bool added = playerInventory.TryAddItem(recipe.item, 1);
        if (!added)
        {
            Debug.LogWarning("Inventário cheio! Não foi possível adicionar o item craftado.");
            return false;
        }

        Debug.Log($"Craft realizado: {recipe.RecipeName}");
        playerInventory.NotifyChanged();
        return true;
    }

    /// <summary>
    /// Verifica se o player tem todos os itens e quantidades necessárias.
    /// </summary>
    public bool CanCraft(CraftRecipe recipe)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            if (!HasEnoughItem(ingredient.Item, ingredient.Amount))
                return false;
        }
        return true;
    }

    private bool HasEnoughItem(ItemData item, int amount)
    {
        int total = 0;
        foreach (var slot in playerInventory.Slots)
        {
            if (!slot.IsEmpty && slot.item == item)
                total += slot.amount;

            if (total >= amount)
                return true;
        }
        return false;
    }

    private void RemoveItemsFromInventory(ItemData item, int amount)
    {
        int remaining = amount;

        foreach (var slot in playerInventory.Slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                if (slot.amount >= remaining)
                {
                    slot.amount -= remaining;
                    if (slot.amount <= 0)
                        slot.Clear();
                    return;
                }
                else
                {
                    remaining -= slot.amount;
                    slot.Clear();
                }
            }
        }
    }
}