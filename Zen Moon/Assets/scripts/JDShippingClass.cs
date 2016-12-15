using UnityEngine;
using System.Collections;
/// <summary>
/// this class is attached to the shipping box and handles 
/// getting money from shipped produce
/// </summary>
public class JDShippingClass : MonoBehaviour {
    /// <summary>
    /// the array of stuff we want to ship
    /// </summary>
    public ArrayList shippingStock = new ArrayList();
    /// <summary>
    /// goes through the array, takes how much the items are worth
    /// then adds it to the player's total money 
    /// </summary>
    public void DoShippingStuff()
    {
        int moneyToShip = 0;
        foreach (GameObject stuff in shippingStock)
        {
            int moneyValue = stuff.GetComponent<JDPlantClass>().moneyValue;
            moneyToShip += moneyValue;
            //Debug.Log(moneyToShip);
        }
     JDStaticVariables.moneyTotal += moneyToShip;
        shippingStock.Clear();
    }
    /// <summary>
    /// this function adds items passed in to the stock arary
    /// </summary>
    /// <param name="stock">the gameobject we want to add to the shipping list</param>
    public void AddToShipList(GameObject stock)
    {
        shippingStock.Add(stock);
    }
}
