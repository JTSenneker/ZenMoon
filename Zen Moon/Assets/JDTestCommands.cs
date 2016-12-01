using UnityEngine;
using System.Collections;
/// <summary>
/// these commands are strictly for debug testing
/// giving access to the static values to change them on command 
/// so we can test iterations better and faster
/// </summary>
public class JDTestCommands : MonoBehaviour
{

    public JDStaticVariables.inventory inventory;
    public int dayCount = 0;
    public int money = 0;

    // Use this for initialization
    void Start()
    {
        inventory = JDStaticVariables.inventory.hoe;
    }

    // Update is called once per frame
    void Update()
    {
        JDStaticVariables.playerInventory = inventory;
        if (Input.GetButtonDown("Fire2")) JDStaticVariables.DayChange();
        dayCount = JDStaticVariables.dayCount;
        money = JDStaticVariables.moneyTotal;
    }

}
