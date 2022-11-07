using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerInventory;
    private int playerTouchedOnce = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerTouchedOnce++;
            if (playerTouchedOnce == 1)
            {
                Debug.Log("crush");
                AddNewItem();
                Destroy(this.gameObject);
            }

            // Destroy(this.gameObject);
        }
    }

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
            thisItem.itemHeld += 1;
            // InventoryManager.CreateNewItem(thisItem);
            /*            for (int i = 0; i < playerInventory.itemList.Count; i++)
                        {
                            if (playerInventory.itemList[i] == null)
                            {
                                playerInventory.itemList[i] = thisItem;
                                break;
                            }
                        }*/
        }
        else
        {
            thisItem.itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
}
