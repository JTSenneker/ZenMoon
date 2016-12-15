using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Describes a merchant
/// </summary>
public class Merchant : MonoBehaviour
{
    /// <summary>
    /// The items that the merchant could sell
    /// </summary>
    public GameObject[] possibleItemSale;
    /// <summary>
    /// The panel for an item in the merchant screen
    /// </summary>
    public GameObject itemPanel;
    /// <summary>
    /// The indexes already used for putting together a random inventory
    /// </summary>
    ArrayList indexesUsed = new ArrayList();
    /// <summary>
    /// The possible amount of things the merchant can carry - 1 for fences, he always sells fences
    /// </summary>
    public int maxSale = 2;
    /// <summary>
    /// The current inventory of the merchant
    /// </summary>
    public ArrayList itemSale = new ArrayList();
    /// <summary>
    /// The amount of items the merchant has in stock
    /// </summary>
    ArrayList stock = new ArrayList();

    /// <summary>
    /// Initializes the items the merchant has
    /// </summary>
    void Start()
    {
        for (int i = 0; i < maxSale; i++)
        {
            PickInventory();
        }
        itemSale.Add(possibleItemSale[4]);

        for (int j = 0; j < itemSale.Count; j++)
        {
            int die1 = Random.Range(10, 31);
            stock.Add(die1);
        }
    }

    /// <summary>
    /// Picks the items the merchant has in stock randomly
    /// </summary>
    void PickInventory()
    {
        int die1 = Random.Range(0, 4);
        if (!inStock(die1))
        {
            indexesUsed.Add(die1);
            itemSale.Add(possibleItemSale[die1]);
        }
    }

    /// <summary>
    /// Makes sure that the stock doesn't have duplicates
    /// </summary>
    /// <param name="die1">The random number being selected</param>
    /// <returns>true if the item is already in stock</returns>
    bool inStock(int die1)
    {
        for (int i = 0; i < indexesUsed.Count; i++)
        {
            if ((int)indexesUsed[i] == die1)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Opens the sell screen
    /// </summary>
    /// <param name="merchantScreen">The sell screen</param>
    public void openSell(GameObject merchantScreen)
    {
        merchantScreen.SetActive(true);
    }
}
