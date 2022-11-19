using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftItem", menuName = "Crafting/New CraftItem")]
public class CraftItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    public int itemID; // Unlike Item, it does not have a buff type
    [TextArea]
    public string itemInfo;

}
