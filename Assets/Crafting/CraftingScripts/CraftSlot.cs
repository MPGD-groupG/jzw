using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerHP playerHP;
    private GameObject player;
    public static CraftSlot instance;
    public GameObject itemInSlot;

    public Text slotID;// IDnumber
    public CraftItem slotItem; // Props in the grid
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    private bool chooseItem;
    public Text resultInformation;

    // Detect double click
    private float realTime = 0;
    private bool timeContinue = false;
    private bool doubleClick;
    private float intervalTime = 0.3f;

    public Item[] thisItem;
    public Inventory playerInventory;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHP = player.GetComponent<PlayerHP>();

    }


    private void Update()
    {
        if (realTime >= 0)
        {
            realTime -= Time.deltaTime;
        }

        if (timeContinue && realTime <= 0.01)
        {
            // Debug.Log("one click");// The timer ends, one click
            doubleClick = false;
            timeContinue = false;
        }

    }




    public void ItemOnClicked()
    {
        CraftingManager.UpdateItemInfo(slotItem.itemInfo);
        chooseItem = true;

        if (realTime <= 0)// is equal to 0, the timer starts and ends with a click
        {
            realTime = intervalTime;
            timeContinue = true;
        }
        else if (realTime > 0)// Not equal to 0, means in time, double click, stop timing
        {
            // Debug.Log("double click");//
            doubleClick = true;
            realTime = 0;
            timeContinue = false;
        }


        // Obtain new items
        if (chooseItem && doubleClick)
        {
            switch (slotItem.itemID)
            {
                case 1: // Create Hotdog need consume 2 Hams
                    // thisItem need to be changed in slot_craft
                    if (playerInventory.itemList.Contains(thisItem[2]) && thisItem[2].itemHeld >= 2)
                    {
                        thisItem[2].itemHeld -= 2; // Consume 2 item1
                                                   // Create new item in inventory list
                        if (!playerInventory.itemList.Contains(thisItem[3]))
                        {
                            playerInventory.itemList.Add(thisItem[3]);
                            thisItem[3].itemHeld += 1;
                        }
                        else thisItem[3].itemHeld += 1;
                        CraftingManager.ShowCraftResult("Crafte success");
                    }
                    else
                    {
                        CraftingManager.ShowCraftResult("Insufficient materials");
                    }
                    break;
                case 2: // Create Hamburger need consume 2 Steaks

                    if (playerInventory.itemList.Contains(thisItem[7]) && thisItem[7].itemHeld >= 2)
                    {
                        thisItem[7].itemHeld -= 2; // Consume 2 item2
                                                   // Create new item in inventory list
                        if (!playerInventory.itemList.Contains(thisItem[3]))
                        {
                            playerInventory.itemList.Add(thisItem[3]);
                            thisItem[3].itemHeld += 1;
                        }
                        else thisItem[3].itemHeld += 1;
                        CraftingManager.ShowCraftResult("Crafte success");
                    }
                    else
                    {
                        CraftingManager.ShowCraftResult("Insufficient materials");
                    }
                    break;

            }

            InventoryManager.RefreshItem();

            //InventoryManager.UpdateItemInfo(slotItem.itemInfo);

            chooseItem = false;
            CraftingManager.RefreshItem();
        }
    }


    public void SetupSlot(CraftItem item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString(); // Use the itemHeld to temporarily replace the ID
        slotID.text = item.itemID.ToString(); // Can't change the slotID
        slotInfo = item.itemInfo;

    }


}
