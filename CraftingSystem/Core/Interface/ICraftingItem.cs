using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICraftingItem
{
    string[] CraftingKeys { get; }
    string Name { get; }
}
