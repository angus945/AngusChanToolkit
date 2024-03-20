using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICraftingEncoder
{
    string[] Encode(ICraftingItem[] recipes);
}
