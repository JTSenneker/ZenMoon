using UnityEngine;
using System.Collections;

/// <summary>
/// this class contains all the info on plants
/// </summary>
public class JDPlantClass : MonoBehaviour
{
    Sprite daikon1;
    /// <summary>
    /// how long it grows for
    /// </summary>
    public int growthTime;
    /// <summary>
    ///how long it has been growing for, only is incremented if the tile it's planted on is watered 
    /// </summary>
    int growingDays = 0;
    /// <summary>
    /// bloomed means the plant can be harvested
    /// </summary>
    public bool bloomed;
    /// <summary>
    /// can the plant be harvested
    /// </summary>
    public bool canBeHarvested = false;
    /// <summary>
    /// does the plant stick around after being harvested
    /// </summary>
    public bool destroyOnHarvest = true;
    /// <summary>
    /// how much money is the plant worth
    /// </summary>
    public int moneyValue;
    /// <summary>
    /// this tile this plant is 'planted' on
    /// </summary>
    public GameObject plantedTile;


    /// <summary>
    /// the spriterenderer attached to the object, not sure if i need this or the sprites itself to modify which one is being displayed
    /// </summary>
    SpriteRenderer spriteR;

    /// <summary>
    /// A reference to the animator attatched to the GameObject.
    /// </summary>
    Animator anim;

    /// <summary>
    /// The type of crop this plant is
    /// </summary>
    public SeedType seedType;

    /// <summary>
    /// initialize the value of the plant
    /// </summary>
    void Start ()
    {
        daikon1 = (Sprite)Resources.Load("Plants_8");

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

        spriteR = GetComponent<SpriteRenderer>();
    }
	
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
        }
        //this part is for harvesting a plant that can be gotten more than once
        if(bloomed && destroyOnHarvest == false && growingDays == growthTime && !canBeHarvested)
        {
            canBeHarvested = true;
        }

        GrowingFrames();
        
        //once bloomed, we can then harvest from it
        //some plants stick around after being harvested
        //so if they have been harvested, they don't change until they bloom again
    }
    /// <summary>
    /// this function changes the plant's current frame 
    /// based on what seedtype it is and how old it is
    /// </summary>
    void GrowingFrames()
    {
//////////////TODO: currently not called as i'm not sure the exact syntax we're using with this
        switch (seedType)
        {
            case SeedType.corn:
                break;
            case SeedType.leek:
                break;
            case SeedType.rice:
                break;
            case SeedType.daikon:
               // spriteR.sprite = daikon1;
                break;
        }
    }

    /// <summary>
    /// this function simply checks the 'bloomed' and 'canBeHarvested' bools to check if the plant is ready to be harvested
    /// </summary>
    /// <returns>returns true if the plant is ready to be harvested</returns>
    public bool CanHarvest()
    {
        return (bloomed && canBeHarvested);
        
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
