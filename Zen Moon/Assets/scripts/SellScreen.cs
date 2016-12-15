using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This controls the sell screen stuff
/// </summary>
public class SellScreen : MonoBehaviour
{
    public int daikonPrice = 10;
    public int leekPrice = 20;
    public int cornPrice = 30;
    public int ricePrice = 40;
    public int fencePrice = 5;

    public GameObject daikonSeeds;
    public GameObject leekSeeds;
    public GameObject cornSeeds;
    public GameObject riceSeeds;
    public GameObject fence;

    public Text Totalprice;
    public Text money;
    public GameObject player;

    int Total = 0;
    ArrayList totalItems = new ArrayList();

    void Start()
    {
        money.text = "$" + JDStaticVariables.moneyTotal.ToString();
    }

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

    public void Cancel(GameObject merchantScreen)
    {
        merchantScreen.SetActive(false);
        player.GetComponent<PlayerController>().inMenu = false;
        Time.timeScale = 1;
    }
}
