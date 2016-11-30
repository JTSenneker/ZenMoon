using UnityEngine;
using System.Collections;

public static class JDStaticVariables
{

    public enum tiles
    {
        dirt = 1,
        tilledDirt = 2,
        rock = 3,
        seeded = 4,
        watered = 5,
        //waterSeeds = 6
    }

    public enum inventory
    {
        nothing = 0,
        water = 1,
        //seed = 2,
        rice = 3,
        corn = 4,
        daikon = 5,
        leek = 6,
        scythe,
        hoe
    }

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
    public static int zenTotal;
    //total amount of money the player has
    public static int moneyTotal;

    static public void DayChange()
    {
        dayCount++;

        //put in logic here that makes all watered tiles turn back to seeded
    }
}
