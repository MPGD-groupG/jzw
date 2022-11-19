using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerHP playerHP;
    private GameObject player;
    public static Slot instance;

    public int slotID;// IDnumber
    public Item slotItem; // Props in the grid
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    private bool chooseItem;

    public GameObject itemInSlot;


    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHP = player.GetComponent<PlayerHP>();

    }

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        chooseItem = true;
        if (chooseItem)
        {
            //InventoryManager.UpdateItemInfo(slotItem.itemInfo);
            if (slotItem.itemHeld != 0)
            {
                slotItem.itemHeld -= 1;
                switch (slotItem.itemPowerType)
                {
                    case 1:
                        playerController.gotSpeedUpPower = true;;
                        break;
                    case 2:
                        playerHP.gotRestoreHPPower = true;
                        break; 

                }
                // playerController.gotSpeedUpPower = true;
            }
            else
            {
                Debug.Log("no item can use");
            }
            chooseItem = false;
            InventoryManager.RefreshItem();
        }
    }

/*    public void UseOnClicked()
    {
        if (chooseItem)
        {
            //InventoryManager.UpdateItemInfo(slotItem.itemInfo);
            if (slotItem.itemHeld != 0)
            {
                slotItem.itemHeld -= 1;
            }
            else
            {
                Debug.Log("no item can use");
            }
            chooseItem = false;
            InventoryManager.RefreshItem();
        }
    }*/

    public void SetupSlot(Item item)
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
