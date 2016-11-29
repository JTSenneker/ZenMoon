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
    /// The selected inventory item
    /// </summary>
    GameObject currItem;
    /// <summary>
    /// The selected item that shows up on the screen for a small amount of time
    /// </summary>
    GameObject tempObj;
    /// <summary>
    /// The index of the current selected item
    /// </summary>
    int currIndex = 0;
    /// <summary>
    /// Whether or not the animation for swapping inventory is finished
    /// </summary>
    public bool animFinished = true;
    /// <summary>
    /// Whether or not the item is already showing on screen
    /// </summary>
    bool isShowing = false;
    /// <summary>
    /// The beginning hoe that the player starts out with
    /// </summary>
    public GameObject hoe;
    /// <summary>
    /// The beginning watering can that the player starts out with
    /// </summary>
    public GameObject wateringCan;

	/// <summary>
	/// Adds certain starting items to inventory
	/// </summary>
	void Start () 
    {
        AddItem(hoe);
        AddItem(wateringCan);
	}
	
	/// <summary>
	/// Sets the currently selected item in the inventory
	/// </summary>
	void Update () 
    {
        currItem = (GameObject)inventory[currIndex];
	}

    /// <summary>
    /// Moves through the inventory and wraps the inventory
    /// </summary>
    public void MoveInventory(float input)
    {
        if (animFinished)
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
        }
    }

    /// <summary>
    /// Adds an item to the inventory
    /// </summary>
    /// <param name="item">The item being added to the inventory</param>
    void AddItem(GameObject item)
    {
        inventory.Add(item);
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
