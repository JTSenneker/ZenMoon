using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the inventory of the player
/// </summary>
public class InventoryController : MonoBehaviour 
{
    /// <summary>
    /// The total inventory of the player
    /// </summary>
    ArrayList inventory = new ArrayList();
    /// <summary>
    /// The count of each item in the inventory
    /// </summary>
    ArrayList inventoryCount = new ArrayList();
    /// <summary>
    /// The selected inventory item
    /// </summary>
    public GameObject currItem = null;
    /// <summary>
    /// The selected item that shows up on the screen for a small amount of time
    /// </summary>
    GameObject tempObj;
    /// <summary>
    /// The index of the current selected item
    /// </summary>
    int currIndex = 0;
    /// <summary>
    /// Whether or not the item is already showing on screen
    /// </summary>
    bool isShowing = false;
    public GameObject[] possibleItems;

	/// <summary>
	/// Adds certain starting items to inventory
	/// </summary>
	void Start () 
    {
        AddItem(possibleItems[0]);
        AddItem(possibleItems[1]);
	}

    /// <summary>
    /// Moves through the inventory and wraps the inventory
    /// </summary>
    public void MoveInventory(float input)
    {
        currIndex += (int)input;

        if (currIndex == -1)
        {
            currIndex = inventory.Count - 1;
        }
        if (currIndex >= inventory.Count)
        {
            currIndex = 0;
        }
        currItem = (GameObject)inventory[currIndex];
    }

    /// <summary>
    /// Adds an item to the inventory
    /// </summary>
    /// <param name="item">The item being added to the inventory</param>
    public void AddItem(GameObject item)
    {
        bool inInventory = false;
        int index = 0;

        
        foreach(GameObject g in inventory)
        {
            if (item.tag == g.tag)
            {
                index = inventory.IndexOf(g);
                inInventory = true;
            }
        }

        if (!inInventory)
        {
            for (int i = 0; i <= possibleItems.Length - 1; i++)
            {
                if (possibleItems[i].tag == item.tag)
                {
                    inventory.Add(possibleItems[i]);
                    inventoryCount.Add(1);
                }
            }
        }
        else if ((int)inventoryCount[index] <= 99)
        {
            inventoryCount[index] = (int)inventoryCount[index] + 1;
        }
    }

    /// <summary>
    /// removes an item from the inventory
    /// </summary>
    /// <param name="item">The item being removed</param>
    void RemoveItem(GameObject item)
    {
        inventory.Remove(item);
    }

    /// <summary>
    /// Shows the item that is currently selected
    /// </summary>
    public void ShowItem()
    {
        if (!isShowing)
        {
            Vector3 placement = new Vector3(0, transform.position.y + .5f, 0);
            tempObj = (GameObject)Instantiate(currItem, placement, Quaternion.identity);
            isShowing = true;
        } 
    }

    /// <summary>
    /// Hides the item that is currently selected after the animation has finished.
    /// </summary>
    public void HideItem()
    {
        Destroy(tempObj.gameObject);
        isShowing = false;
    }
}
