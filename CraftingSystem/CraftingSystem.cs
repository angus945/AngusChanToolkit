using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem
{
    ICraftingTable craftingTable;
    ICraftingEncoder craftingEncoder;
    public CraftingSystem(ICraftingTable craftingTable, ICraftingEncoder craftingEncoder)
    {
        this.craftingTable = craftingTable;
        this.craftingEncoder = craftingEncoder;
    }
    public bool SearchByResult(ICraftingItem resultItem, out int recipeIndex)
    {
        return craftingTable.SearchByResult(resultItem.CraftingKeys[0], out recipeIndex);
    }
    public string[] GetRecipesKyes(ICraftingItem[] recipes)
    {
        return craftingEncoder.Encode(recipes);
    }
}
