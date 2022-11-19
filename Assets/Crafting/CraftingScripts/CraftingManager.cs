using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;

    public Crafting myCraft;
    public GameObject slotGrid;
    public CraftSlot slotPrefab;
    //public GameObject emptySlot;
    public Text itemInformation;
    public Text craftResult;

    public List<GameObject> slots = new List<GameObject>(); // Manage the slots generated

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = ""; // Item information is empty at first
        instance.craftResult.text = "";
    }


    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }


    public static void ShowCraftResult(string craftResult)
    {
        instance.craftResult.text = craftResult;

    }


    public static void CreateNewItem(CraftItem item)
    {
        CraftSlot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }

    public static void RefreshItem()
    {
        // Loop to delete a subset of objects under slotGrid
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        // Regenerate the slots corresponding to the items in myBag
        for (int i = 0; i < instance.myCraft.itemList.Count; i++)
        {
            CreateNewItem(instance.myCraft.itemList[i]);

        }
    }
}
