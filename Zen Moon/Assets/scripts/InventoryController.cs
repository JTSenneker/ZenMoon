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
    /// The previous item selected
    /// </summary>
    public GameObject prevItem = null;
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
    public bool isShowing = false;
    /// <summary>
    /// The items that the player starts with
    /// </summary>
    public GameObject[] startItems;
    /// <summary>
    /// Testing variable
    /// </summary>
    public int[] itemCount;

    /// <summary>
    /// Adds certain starting items to inventory
    /// </summary>
    void Start()
    {
        foreach (GameObject item in startItems)
        {
            AddItem(item);
        }
        foreach (int itemC in itemCount)
        {
            inventoryCount.Add(itemC);
        }
        if (inventory[currIndex] != null)
        {
            currItem = (GameObject)inventory[currIndex];
        }
    }

    /// <summary>
    /// removes items that have an inventory count of zero
    /// </summary>
    void FixedUpdate()
    {
        if (currIndex != -1)
        {
            if ((int)inventoryCount[currIndex] == 0)
            {
                RemoveItem(currItem);
            }
        }
    }

    /// <summary>
    /// Gets the Inventory but in an array of strings rather than gameobjects
    /// </summary>
    /// <returns>An inventory in strings</returns>
    public ArrayList GetInventory()
    {
        ArrayList invString = new ArrayList();
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject temp = (GameObject)inventory[i];
            invString.Add(temp.name);
        }
        return invString;
    }

    /// <summary>
    /// Gets the inventory count
    /// </summary>
    /// <returns>The inventory count</returns>
    public ArrayList GetInventoryCount()
    {
        return inventoryCount;
    }

    /// <summary>
    /// Gets the inventory count
    /// </summary>
    /// <returns>The current item's count</returns>
    public int GetCurrentCount()
    {
        return (int)inventoryCount[currIndex];
    }

    /// <summary>
    /// Sets the inventory when the game is being loaded
    /// </summary>
    /// <param name="inv">The inventory using strings</param>
    public void SetInventory(ArrayList inv)
    {
        inventory.Clear();

        InventoryItems invIt = GetComponent<InventoryItems>();
        for (int i = 0; i < inv.Count; i++)
        {
            for (int j = 0; j < invIt.pItemNames.Length; j++)
            {
                if ((string)inv[i] == invIt.pItemNames[j] + "(Clone)")
                {
                    AddItem(invIt.possibleItems[j]);
                }
            }
        }
    }

    /// <summary>
    /// Sets the inventory count when the game is being loaded
    /// </summary>
    /// <param name="invCount">The array of intigers with the inventory count</param>
    public void SetInventoryCount(ArrayList invCount)
    {
        inventoryCount.Clear();
        inventoryCount = invCount;
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
        prevItem = currItem;
        currItem = (GameObject)inventory[currIndex];
    }

    /// <summary>
    /// Adds an item to the inventory
    /// </summary>
    /// <param name="item">The item being added to the inventory</param>
    public void AddItem(GameObject item)
    {
        int index = CheckInventory(item);

        if (index == -1)
        {
            Vector3 placement = new Vector3(0, 0, -11);
            GameObject newItem = (GameObject)Instantiate(item, placement, Quaternion.identity);
            inventory.Add(newItem);
            //inventoryCount.Add(1);
        }
        else if ((int)inventoryCount[index] <= 99)
        {
            inventoryCount[index] = (int)inventoryCount[index] + 1;
        }
    }

    /// <summary>
    /// Checks if the item being added is already in the inventory
    /// </summary>
    /// <param name="item">The item being added</param>
    /// <returns>The index of the item if it is already in the inventory, if it is not in the inventory it returns -1</returns>
    private int CheckInventory(GameObject item)
    {
        foreach (GameObject g in inventory)
        {
            if (item.tag == "tool" && g.tag == "tool")
            {
                if (item.GetComponent<Tool>().toolType == g.GetComponent<Tool>().toolType)
                {
                    return inventory.IndexOf(g);
                }
            }
            if (item.tag == "seeds" && g.tag == "seeds")
            {
                if (item.GetComponent<JDPlantClass>().seedType == g.GetComponent<JDPlantClass>().seedType)
                {
                    return inventory.IndexOf(g);
                }
            }
            if (item.tag == "crop" && g.tag == "crop")
            {
                if (item.GetComponent<Crop>().cropType == g.GetComponent<Crop>().cropType)
                {
                    return inventory.IndexOf(g);
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// removes an item from the inventory
    /// </summary>
    /// <param name="item">The item being removed</param>
    public void RemoveItem(GameObject item)
    {
        int index = inventory.IndexOf(item);

        if (index != -1)
        {
            if ((int)inventoryCount[index] > 0)
            {
                inventoryCount[index] = (int)inventoryCount[index] - 1;
                print(inventoryCount[index]);
            }
            else if ((int)inventoryCount[index] == 0)
            {
                inventoryCount.Remove(inventoryCount[index]);
                inventory.Remove(inventory[index]);
                Destroy(currItem.gameObject);
                MoveInventory(-1);
            }
        }
    }

    /// <summary>
    /// Shows the item that is currently selected
    /// </summary>
    public void ShowItem()
    {
        if (!isShowing)
        {
            Vector3 placement = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            tempObj = (GameObject)Instantiate(currItem, placement, Quaternion.identity);
            isShowing = true;
            if (tempObj.tag == "crop")
            {
                tempObj.GetComponent<Crop>().Selected();
            }
            else if (tempObj.tag == "fence")
            {
                tempObj.GetComponent<Fence>().Selected();
            }
        }
    }

    /// <summary>
    /// Hides the item that is currently selected after the animation has finished.
    /// </summary>
    public void HideItem()
    {
        if (tempObj.tag == "crop")
        {
            tempObj.GetComponent<Crop>().NotSelected();
        }
        if (tempObj.tag == "fence")
        {
            tempObj.GetComponent<Fence>().NotSelected();
        }
        Destroy(tempObj.gameObject);
        isShowing = false;
    }
}

