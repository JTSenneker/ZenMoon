using UnityEngine;
using System.Collections;

public static class JDStaticVariables
{

    public enum tiles
    {
        dirt,
        tilledDirt,
        rock,
        seeded,
        watered,
        fence
        //watered seeds??
    }

    public enum inventory
    {
        nothing,
        water,
        //seed,
        riceSeed,
        cornSeed,
        daikonSeed,
        leekSeed,
        ricePlant,
        daikonPlant,
        leekPlant,
        cornPlant,
        scythe,
        hoe,
        fence
    }


    public static GameObject[,] groundArray;

    //public static ArrayList plantArray;

    public static ArrayList shippingStock = new ArrayList();

    public static int riceGrowthTime = 4;
    public static int riceMoneyValue = 50;

    public static int cornGrowthTime = 7;
    public static int cornMoneyValue = 100;

    public static int leekGrowthTime = 3;
    public static int leekMoneyValue = 75;

    public static int daikonGrowthTime = 5;
    public static int daikonMoneyValue = 100;


    //what is currently selected, defaults to nothing
    public static inventory playerInventory = inventory.water;

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
        CheckPlantsGrow();
        DoShippingStuff();
        //put in logic here that makes all watered tiles turn back to seeded
    }
    /// <summary>
    /// all the stuff we need to do for shipping
    /// namely calculating money and clearing the list
    /// </summary>
    static void DoShippingStuff()
    {
        int moneyToShip = 0;
        foreach(GameObject stuff in shippingStock)
        {
            int moneyValue = 0;
            if (stuff.name == "Rice") moneyValue = riceMoneyValue;
            if (stuff.name == "Leek") moneyValue = leekMoneyValue;
            if (stuff.name == "Daikon") moneyValue = daikonMoneyValue;
            if (stuff.name == "Corn") moneyValue = cornMoneyValue;                  
            moneyToShip += moneyValue;
            //Debug.Log(moneyToShip);
        }
        moneyTotal += moneyToShip;
        shippingStock.Clear();
    }
    /// <summary>
    /// this function checks each tile
    /// if the tiles has something planted in it
    /// it tells the plant to grow if it can
    /// then resets every watered tile to it's default status
    /// so the player must water the tile again the next day
    /// </summary>
    static void CheckPlantsGrow()
    {
        foreach(GameObject ground in groundArray)
        {
            //Debug.Log(ground.name);
            if (ground.GetComponentInChildren<JDGroundClass>().occupiedWith != null)
            {
                //the plant checks if the tile has been watered, if so it will grow                
                ground.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().Grow();
                if(ground.GetComponentInChildren<JDGroundClass>()._tileStatus == JDStaticVariables.tiles.watered)
                {
                    ground.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                }
            }
            if(ground.GetComponentInChildren<JDGroundClass>()._tileStatus == JDStaticVariables.tiles.watered)
            {
                ground.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.dirt;
            }
        }
    }
}
