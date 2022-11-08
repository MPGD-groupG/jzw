﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject player;
    public static Slot instance;

    public int slotID;//空格ID 等于 物品ID
    public Item slotItem; // 格子里的道具
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
                playerController.gotSuperpower = true;
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