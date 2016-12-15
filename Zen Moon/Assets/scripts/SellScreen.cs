using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This controls the sell screen stuff
/// </summary>
public class SellScreen : MonoBehaviour
{
    /// <summary>
    /// The price of daikon seeds
    /// </summary>
    public int daikonPrice = 10;
    /// <summary>
    /// The price of leek seeds
    /// </summary>
    public int leekPrice = 20;
    /// <summary>
    /// The price of corn seeds
    /// </summary>
    public int cornPrice = 30;
    /// <summary>
    /// The price of rice seeds
    /// </summary>
    public int ricePrice = 40;
    /// <summary>
    /// The price of fences
    /// </summary>
    public int fencePrice = 5;

    /// <summary>
    /// The reference to the daikon seed sprite
    /// </summary>
    public GameObject daikonSeeds;
    /// <summary>
    /// The reference to the leek seeds sprite
    /// </summary>
    public GameObject leekSeeds;
    /// <summary>
    /// The reference to the corn seeds sprite
    /// </summary>
    public GameObject cornSeeds;
    /// <summary>
    /// The reference to the rice seeds sprite
    /// </summary>
    public GameObject riceSeeds;
    /// <summary>
    /// The reference to to the fence
    /// </summary>
    public GameObject fence;

    /// <summary>
    /// The total price text box
    /// </summary>
    public Text Totalprice;
    /// <summary>
    /// The total money text box
    /// </summary>
    public Text money;
    /// <summary>
    /// The reference to the player
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Total of money
    /// </summary>
    int Total = 0;
    /// <summary>
    /// How many items the merchant has
    /// </summary>
    ArrayList totalItems = new ArrayList();

    /// <summary>
    /// Sets the total money
    /// </summary>
    void Start()
    {
        money.text = "$" + JDStaticVariables.moneyTotal.ToString();
    }

    /// <summary>
    /// Adds to the total number
    /// </summary>
    /// <param name="panel">A reference to the panel</param>
    public void AddToItem(GameObject panel)
    {
        Text name = panel.transform.GetChild(1).GetComponent<Text>();
        Text amount = panel.transform.GetChild(2).GetComponent<Text>();
        Text price = panel.transform.GetChild(3).GetComponent<Text>();

        int numb = int.Parse(amount.text);
        numb++;
        totalItems.Add(GetItem(name.text));
        amount.text = numb.ToString();

        int seedPrice = GetPrice(name.text);
        int currPrice = seedPrice * numb;
        Total += seedPrice;
        price.text = "$" + currPrice.ToString();

        Totalprice.text = "$" + Total.ToString();
    }

    /// <summary>
    /// Subtracts the total number
    /// </summary>
    /// <param name="panel">The reference of the panel</param>
    public void SubtractItem(GameObject panel)
    {
        Text name = panel.transform.GetChild(1).GetComponent<Text>();
        Text amount = panel.transform.GetChild(2).GetComponent<Text>();
        Text price = panel.transform.GetChild(3).GetComponent<Text>();

        int numb = int.Parse(amount.text);
        if (numb != 0)
        {
            numb--;
            totalItems.Remove(GetItem(name.text));
            amount.text = numb.ToString();

            int seedPrice = GetPrice(name.text);
            int currPrice = seedPrice * numb;
            Total -= seedPrice;
            price.text = "$" + currPrice.ToString();

            Totalprice.text = "$" + Total.ToString();
        } 
    }

    /// <summary>
    /// Gets the price according to the panel name
    /// </summary>
    /// <param name="name">The string name</param>
    /// <returns>Returns an intiger</returns>
    int GetPrice(string name)
    {
        if (name == "Daikon Seeds")
        {
            return daikonPrice;
        }
        else if (name == "Leek Seeds")
        {
            return leekPrice;
        }
        else if (name == "Corn Seeds")
        {
            return cornPrice;
        }
        else if (name == "Rice Seeds")
        {
            return ricePrice;
        }
        else if (name == "Fence")
        {
            return fencePrice;
        }
        return 0;
    }

    /// <summary>
    /// Returns an item for the name of the panel
    /// </summary>
    /// <param name="name">The name of the panel</param>
    /// <returns>The gameobject</returns>
    GameObject GetItem(string name)
    {
        if (name == "Daikon Seeds")
        {
            return daikonSeeds;
        }
        else if (name == "Leek Seeds")
        {
            return leekSeeds;
        }
        else if (name == "Corn Seeds")
        {
            return cornSeeds;
        }
        else if (name == "Rice Seeds")
        {
            return riceSeeds;
        }
        else if (name == "Fence")
        {
            return fence;
        }
        return null;
    }

    /// <summary>
    /// Buys the item
    /// </summary>
    public void Buy()
    {
        int currMoney = JDStaticVariables.moneyTotal - Total;
        if (currMoney >= 0)
        {
            JDStaticVariables.moneyTotal = currMoney;
            money.text = "$" + currMoney.ToString();
            player.GetComponent<PlayerController>().AddInventoryItems(totalItems);
        }
    }

    /// <summary>
    /// Gets out of the screen
    /// </summary>
    /// <param name="merchantScreen">The panel</param>
    public void Cancel(GameObject merchantScreen)
    {
        merchantScreen.SetActive(false);
        player.GetComponent<PlayerController>().inMenu = false;
        Time.timeScale = 1;
    }
}
