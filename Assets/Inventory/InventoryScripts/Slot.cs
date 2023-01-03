using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerHP playerHP;
    private PlayerSP playerSP;
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
        playerSP = player.GetComponent<PlayerSP>();
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
                        playerHP.restoreHP = 5f;
                        break;
                    case 2:
                        playerSP.itemRestoreSP = 5f;
                        break;
                    case 3:
                        playerHP.restoreHP = 20f;
                        break;
                    case 4:
                        playerHP.isGod = true;
                        break;
                    case 5:
                        playerController.gotSpeedUpPower = true;
                        break;
                    case 6:
                        playerVP.restoreVP = 5f;
                        break;
                    case 7:
                        playerSP.itemRestoreSP = 20f;
                        break;
                    case 8:
                        playerVP.restoreVP = 20f;
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
