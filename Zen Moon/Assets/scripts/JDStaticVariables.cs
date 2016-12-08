using UnityEngine;
using System.Collections;

public static class JDStaticVariables
{
    //// TODO: may need to be in a shipping class
    public static ArrayList shippingStock = new ArrayList();

    //to check if we're using the mouse or gamepad, defaults to mouse
    public static bool usingMouse = true;

    //keep track of the number of days, starts at 1, because not an array
    public static int dayCount = 1;
    //total amount of zen the player has
    [Range(0,100)]
    public static int zenTotal = 0;
    //total amount of money the player has
    public static int moneyTotal;

    static public void DayChange()
    {
        dayCount++;
        DoShippingStuff();
    }

    ////TODO: This might need it's own class when whe get a shipping box

    /// <summary>
    /// all the stuff we need to do for shipping
    /// namely calculating money and clearing the list
    /// </summary>
    static void DoShippingStuff()
    {
        int moneyToShip = 0;
        foreach(GameObject stuff in shippingStock)
        {
            int moneyValue = stuff.GetComponent<JDPlantClass>().moneyValue;                 
            moneyToShip += moneyValue;
            Debug.Log(moneyToShip);
        }
        moneyTotal += moneyToShip;
        shippingStock.Clear();
    }
}
