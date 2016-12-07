using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the Inventory panel
/// </summary>
public class InventoryPanel : MonoBehaviour
{
    /// <summary>
    /// The player
    /// </summary>
    public GameObject player;
    /// <summary>
    /// The inventory Controller on the player
    /// </summary>
    InventoryController invCon;
    /// <summary>
    /// The current item the player has selected
    /// </summary>
    GameObject item = null;
    /// <summary>
    /// The current amount of the item the player has selected
    /// </summary>
    int itemCount = 0;

	/// <summary>
    /// Sets the inventory controller
    /// </summary>
	void Start ()
    {
        invCon = player.GetComponent<InventoryController>();
	}
	
	/// <summary>
    /// switches the inventory items when the player selects a new item
    /// </summary>
	void Update ()
    {
        if(invChanged())
        {
            hideItem();
            showItem();
        }
	}

    /// <summary>
    /// Whether or not the currently selected item has changed
    /// </summary>
    /// <returns>True or False depending on if the item has changed</returns>
    bool invChanged()
    {
        if (invCon.currItem != invCon.prevItem)
        {
            invCon.prevItem = invCon.currItem;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Deletes the current item on the panel
    /// </summary>
    void hideItem()
    {
        if(item != null)
        {
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// Shows the item that is now selected by the player into the panel
    /// </summary>
    void showItem()
    {
        if (invCon.currItem != null)
        {
            GameObject newItem = (GameObject)Instantiate(invCon.currItem, transform.position, Quaternion.identity);
            item = newItem;
            item.transform.parent = this.transform;
        }
       
    }
}
