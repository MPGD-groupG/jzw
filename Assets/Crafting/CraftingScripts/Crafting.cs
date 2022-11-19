using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting", menuName = "Crafting/New Crafting")]
public class Crafting : ScriptableObject
{
    // Use list to store your items
    public List<CraftItem> itemList = new List<CraftItem>();
}
