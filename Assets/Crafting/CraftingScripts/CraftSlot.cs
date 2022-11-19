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

    public int slotID;// IDnumber
    public CraftItem slotItem; // Props in the grid
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    private bool chooseItem;

    private float intervalTime = 0.3f;

    private float realTime = 0;
    private bool timeContinue = false;
    private bool doubleClick;

    public Item [] thisItem;
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
            Debug.Log("单击");// The timer ends, one click
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
            Debug.Log("double click");//
            doubleClick = true;
            realTime = 0;
            timeContinue = false;
        }


        // Obtain new items
        if (chooseItem && doubleClick)
        {
            //InventoryManager.UpdateItemInfo(slotItem.itemInfo);
            if (slotItem.itemHeld != 0)
            {
                slotItem.itemHeld -= 1;

                switch (slotItem.itemID)
                {
                    case 1: // Create item3 need consume 2 item1
                        if (playerInventory.itemList.Contains(thisItem[0]) && thisItem[0].itemHeld >= 2)
                        {
                            thisItem[0].itemHeld -= 2;
                            // Create new item in inventory list
                            if (!playerInventory.itemList.Contains(thisItem[2]))
                            {
                                playerInventory.itemList.Add(thisItem[2]);
                                thisItem[2].itemHeld += 1;
                            }else thisItem[2].itemHeld += 1;
                        }
                        break; 
                    case 2:
                        playerInventory.itemList.Contains(thisItem[1]);
                        break; 

                }

                InventoryManager.RefreshItem();
            }
            else
            {
                Debug.Log("no item can use");
            }
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
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;

    }


}
