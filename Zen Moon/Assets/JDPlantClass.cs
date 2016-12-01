﻿using UnityEngine;
using System.Collections;

public class JDPlantClass : MonoBehaviour {

    //how long it grows for
    public int growthTime;
    //when it was planted
    int plantedTime;
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


	// Use this for initialization
	void Start () {
        plantedTime = JDStaticVariables.dayCount;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //this is where we update the sprite according to it's status
        //unless we use the animator for that

        //this is where we check it's planted time, compared to it's growth time


   ///////still need to put in logic for utilizing the watered ground
   //most likely, if the ground from the planted tile is not watered, add 1 to planted time, so time effectively doesn't pass
   //for this plant

        if (JDStaticVariables.dayCount == plantedTime + growthTime && !bloomed)
        {
            bloomed = true;
            canBeHarvested = true;
            transform.localScale *= 2;
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
    public void Harvest()
    {
        if (destroyOnHarvest)
        {
            plantedTile.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.dirt;
            Destroy(gameObject);
        }
        else
        {
            canBeHarvested = false;
            plantedTime = JDStaticVariables.dayCount;
        }
    }
}
