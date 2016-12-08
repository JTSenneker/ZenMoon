using UnityEngine;
using System.Collections;

public class JDPlantClass : MonoBehaviour
{

    //how long it grows for
    public int growthTime;
    //how long it has been growing for, only is incremented if the tile it's planted on is watered
    int growingDays = 0;
    //bloomed means the plant can be harvested
    public bool bloomed;
    //has it been harvested yet?
    public bool canBeHarvested = false;
    //does it stick around after being harvested?
    public bool destroyOnHarvest = true;
    //how much money is it worth?
    public int moneyValue;
    //the tile this plant is 'planted' on
    public GameObject plantedTile;

    /// <summary>
    /// Possible types of crops
    /// </summary>
    public enum SeedType
    {
        daikon,
        leek,
        corn,
        rice
    }

    /// <summary>
    /// The type of crop this plant is
    /// </summary>
    public SeedType seedType;


    // Use this for initialization
    void Start ()
    {
        switch (seedType)
        {
            case SeedType.daikon:
                moneyValue = 20;
                growthTime = 4;
                break;
            case SeedType.leek:
                moneyValue = 40;
                growthTime = 5;
                break;
            case SeedType.corn:
                moneyValue = 60;
                growthTime = 7;
                break;
            case SeedType.rice:
                moneyValue = 80;
                growthTime = 10;
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //this is where we update the sprite according to it's status
        //unless we use the animator for that

        //this is where we check it's planted time, compared to it's growth time

        
   //most likely, if the ground from the planted tile is not watered, add 1 to planted time, so time effectively doesn't pass
   //for this plant

        if (growingDays == growthTime && !bloomed)
        {
            bloomed = true;
            canBeHarvested = true;
            //transform.localScale *= 2;
        }
        //this part is for harvesting a plant that can be gotten more than once
        if(bloomed && destroyOnHarvest == false && growingDays == growthTime && !canBeHarvested)
        {
            canBeHarvested = true;
            //transform.localScale *= 2;
        }
        
        
        //once bloomed, we can then harvest from it
        //some plants stick around after being harvested
        //so if they have been harvested, they don't change until they bloom again
    }

    /// <summary>
    /// this function simply checks the 'bloomed' and 'canBeHarvested' bools to check if the plant is ready to be harvested
    /// </summary>
    /// <returns>returns true if the plant is ready to be harvested</returns>
    public bool CanHarvest()
    {
        if (bloomed && canBeHarvested) return true;
        return false;
    }

    /// <summary>
    /// this function is run when the player tries to harvest this plant
    /// if the plant is supposed to be destroyed, then the tile is set to base dirt
    /// and the plant is destroyed
    /// then the tile it's planted on is told that it's now empty
    /// 
    /// if the plant is not destroyed on harvest, it's 'canBeHarvested" status is set to false
    /// and the amount of days it needs to grow resets to 0
    /// </summary>
    public void Harvest()
    {
        if (destroyOnHarvest)
        {
            plantedTile.GetComponentInChildren<JDGroundClass>()._tileStatus = JDGroundClass.tiles.dirt;
            plantedTile.GetComponentInChildren<JDGroundClass>().occupiedWith = null;
            Destroy(gameObject);
        }
        else
        {
            canBeHarvested = false;
            growingDays = 0;
        }
    }

    /// <summary>
    /// this function has the plant test if the tile it's planted on has been watered
    /// and if so, increments the growth time
    /// </summary>
    /// <returns>true if the plant can grow, (the tile is watered)</returns>
    public bool Grow()
    {
        if(plantedTile.GetComponentInChildren<JDGroundClass>()._tileStatus == JDGroundClass.tiles.watered)
        {
            growingDays ++;
            return true;
        }
        return false;
    }
}
