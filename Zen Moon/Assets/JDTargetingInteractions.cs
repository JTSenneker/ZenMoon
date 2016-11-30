using UnityEngine;
using System.Collections;

public class JDTargetingInteractions : MonoBehaviour {

    public GameObject target;
    public GameObject rice;
    public GameObject corn;
    public GameObject leek;
    public GameObject daikon;

    JDStaticVariables.tiles targetStatus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    //syntax: tile status check, then player inventory
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            if (JDStaticVariables.usingMouse)
            {
                target = JDMouseTargeting.target;
                if (target != null)
                {
                    targetStatus = target.GetComponentInChildren<JDGroundClass>()._tileStatus;

                    //if planting crops

  /*                  if((JDStaticVariables.playerInventory == JDStaticVariables.inventory.corn) || 
                       (JDStaticVariables.playerInventory == JDStaticVariables.inventory.leek) ||
                       (JDStaticVariables.playerInventory == JDStaticVariables.inventory.rice) ||
                       (JDStaticVariables.playerInventory == JDStaticVariables.inventory.daikon))
                       {
                        //if the target is tilled
                        if (targetStatus == JDStaticVariables.tiles.tilledDirt)
                        {
                            target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                        }
                    }
                    */
                    //if tilled dirt
                    if (targetStatus == JDStaticVariables.tiles.tilledDirt)
                    {
                        //create plant object, since we're going to plant something
                        GameObject plant;
                        //if planting rice
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.rice)
                        {
                            target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                            plant = Instantiate(rice, target.transform.position, Quaternion.identity) as GameObject;
                            plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.riceGrowthTime;
                            plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.riceMoneyValue;
                            plant.GetComponentInChildren<JDPlantClass>().destroyOnHarvest = false;
                        }

                        //if planting leek
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.leek)
                        {
                            target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                            plant = Instantiate(leek, target.transform.position, Quaternion.identity) as GameObject;
                            plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.leekGrowthTime;
                            plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.leekMoneyValue;
                        }

                        //if planting corn
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.corn)
                        {
                            target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                            plant = Instantiate(corn, target.transform.position, Quaternion.identity) as GameObject;
                            plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.cornGrowthTime;
                            plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.cornMoneyValue;
                            plant.GetComponentInChildren<JDPlantClass>().destroyOnHarvest = false;
                        }        

                        //if planting daikon
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.daikon)
                        {
                            target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                            plant = Instantiate(daikon, target.transform.position, Quaternion.identity) as GameObject;
                            plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.daikonGrowthTime;
                            plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.daikonMoneyValue;
                        }
                    }


                        //base dirt
                        if (targetStatus == JDStaticVariables.tiles.dirt)
                    {
                        //if holding hoe
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.hoe)
                        {
                            target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.tilledDirt;
                        }
                    }




                    //watering seeded ground
                    if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.water)
                    {
                        //can only water seeded ground
                        if (targetStatus == JDStaticVariables.tiles.seeded)
                        {
                            target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.watered;
                        }

                    }


                    print(targetStatus);
                }//end of null check
            }
        }
	}
}
