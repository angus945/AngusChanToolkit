using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Allows the crafting item use as multiple input type
/// </summary>
public class CraftingEncoder_MultipleInput : ICraftingEncoder
{
    string[] ICraftingEncoder.Encode(ICraftingItem[] recipes)
    {
        List<string> resultKeys = new List<string>();
        List<string> combineKeys = new List<string>();

        Permutations(recipes, 0, combineKeys, resultKeys);

        return resultKeys.ToArray();
    }
    void Permutations(ICraftingItem[] recipes, int depth, List<string> combineKeys, List<string> resultKyes)
    {
        if (depth == recipes.Length)
        {
            string key = string.Join(",", combineKeys.OrderBy(x => x));
            resultKyes.Add(key);
            return;
        }

        ICraftingItem item = recipes[depth];
        for (int i = 0; i < item.CraftingKeys.Length; i++)
        {
            string key = item.CraftingKeys[i];

            combineKeys.Add(key);
            Permutations(recipes, depth + 1, combineKeys, resultKyes);
            combineKeys.RemoveAt(combineKeys.Count - 1);
        }
    }
}