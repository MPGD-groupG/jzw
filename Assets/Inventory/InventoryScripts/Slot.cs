using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerHP playerHP;
    private PlayerVP playerVP;
    private GameObject player;
    public GameObject itemInSlot;
    public static Slot instance;

    public int slotID;// IDnumber
    public Item slotItem; // Props in the grid
    public Image slotImage;
    public Text slotNum;
    public string slotInfo;

    private bool chooseItem;

    // Detect double click
    private float realTime = 0;
    private bool timeContinue = false;
    private bool doubleClick;
    private float intervalTime = 0.3f;


    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHP = player.GetComponent<PlayerHP>();
        playerVP = player.GetComponent<PlayerVP>();
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
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        chooseItem = true;

        if (realTime <= 0)// is equal to 0, the timer starts and ends with a click
        {
            realTime = intervalTime;
            timeContinue = true;
        }
        else if (realTime > 0)// Not equal to 0, means in time, double click, stop timing
        {
            // Debug.Log("double click");
            doubleClick = true;
            realTime = 0;
            timeContinue = false;
        }


        if (chooseItem && doubleClick)
        {
            //InventoryManager.UpdateItemInfo(slotItem.itemInfo);
            if (slotItem.itemHeld != 0)
            {
                slotItem.itemHeld -= 1;
                switch (slotItem.itemPowerType)
                {
                    case 1:
                        playerController.gotSpeedUpPower = true; ;
                        break;
                    case 2:
                        playerHP.gotRestoreHPPower = true;
                        break;
                    case 3:
                        playerVP.gotRestoreVPPower = true;
                        break;
                    case 4:
                        // Debug.Log("4th power");
                        playerHP.isGod = true;
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
