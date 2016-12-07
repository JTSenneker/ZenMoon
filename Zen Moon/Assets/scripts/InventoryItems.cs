using UnityEngine;
using System.Collections;

/// <summary>
/// A list of all possible inventory items
/// </summary>
public class InventoryItems: MonoBehaviour
{
    /// <summary>
    /// The array of items in the inventory
    /// </summary>
    public GameObject[] possibleItems;
    /// <summary>
    /// A string array the corrisponds to the items array
    /// </summary>
    public string[] pItemNames;
}
