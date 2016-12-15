using UnityEngine;
using UnityEngine.UI;
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
    Text itemCount;

    /// <summary>
    /// Sets the inventory controller
    /// </summary>
    void Start()
    {
        invCon = player.GetComponent<InventoryController>();
        itemCount = GetComponentInChildren<Text>();
        itemCount.text = "0";
    }

    /// <summary>
    /// switches the inventory items when the player selects a new item
    /// </summary>
    void Update()
    {
        if (invChanged())
        {
            hideItem();
            showItem();
        }
        RefreshCount();
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
        if (item != null)
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
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            GameObject newItem = (GameObject)Instantiate(invCon.currItem, position, Quaternion.identity);
            item = newItem;
            itemCount.text = invCon.GetCurrentCount().ToString();
            item.transform.parent = this.transform;
        }

    }

    /// <summary>
    /// Refreshes the current inventory count if the player has removed or added an item
    /// </summary>
    void RefreshCount()
    {
        itemCount.text = invCon.GetCurrentCount().ToString();
    }
}