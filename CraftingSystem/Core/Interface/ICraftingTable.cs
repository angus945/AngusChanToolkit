using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICraftingTable
{
    bool SearchByResult(string items, out int recipeIndex);
    bool SearchByRecipe(string recipes, out string resultItem);
}
