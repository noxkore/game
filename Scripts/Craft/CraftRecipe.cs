using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Craft Recipe")]
public class CraftRecipe : ScriptableObject
{
    [Header("Buildable")]
    public ItemData item;

    [Header("Recipe Info")]
    [SerializeField] public string recipeName;

    [Header("Ingredients")]
    [SerializeField] public CraftIngredient[] ingredients;

    public string RecipeName => recipeName;
    public CraftIngredient[] Ingredients => ingredients;
}

[Serializable]
public struct CraftIngredient
{
    public ItemData Item;
    public int Amount;

    public CraftIngredient(ItemData item, int amount)
    {
        Item = item;
        Amount = amount;
    }
}