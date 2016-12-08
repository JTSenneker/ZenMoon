using UnityEngine;
using System.Collections;

public class JDTargetingInteractions : MonoBehaviour
{

    public GameObject target;
    public GameObject rice;
    public GameObject corn;
    public GameObject leek;
    public GameObject daikon;
    public GameObject fence;

    public GameObject shippingBox;

    JDStaticVariables.tiles targetStatus;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    //syntax: tile status check, then player inventory
    void Update()
    {

        if (Input.GetButtonDown("Pick"))
        {
            if (JDStaticVariables.usingMouse)
            {
                target = JDMouseTargeting.target;
                if (target != null)
                {
                    if (target.tag == "Ground")
                    {
                        targetStatus = target.GetComponentInChildren<JDGroundClass>()._tileStatus;

                        //if planting crops

                        /*                  if((JDStaticVariables.playerInventory == JDStaticVariables.inventory.cornSeed) || 
                                             (JDStaticVariables.playerInventory == JDStaticVariables.inventory.leek) ||
                                             (JDStaticVariables.playerInventory == JDStaticVariables.inventory.riceSeed) ||
                                             (JDStaticVariables.playerInventory == JDStaticVariables.inventory.daikonSeed))
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
                            //if planting riceSeed
                            if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.riceSeed)
                            {
                                //set the tile as 'seeded'
                                target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                                //create the plant object
                                plant = Instantiate(rice, target.transform.position, Quaternion.identity) as GameObject;
                                //set how long the plant needs to grow
                                plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.riceGrowthTime;
                                //set how much the plant is worth
                                plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.riceMoneyValue;
                                //tell the tile that it is occupied with something
                                target.GetComponentInChildren<JDGroundClass>().occupiedWith = plant;
                                //set whether the plant sticks around after it's harvested
                                plant.GetComponentInChildren<JDPlantClass>().destroyOnHarvest = false;
                                //tell the plant which tile it's being planted on
                                plant.GetComponentInChildren<JDPlantClass>().plantedTile = target;
                            }

                            //if planting leek
                            if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.leekSeed)
                            {
                                target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                                plant = Instantiate(leek, target.transform.position, Quaternion.identity) as GameObject;
                                plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.leekGrowthTime;
                                plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.leekMoneyValue;
                                target.GetComponentInChildren<JDGroundClass>().occupiedWith = plant;
                                plant.GetComponentInChildren<JDPlantClass>().plantedTile = target;
                            }

                            //if planting cornSeed
                            if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.cornSeed)
                            {
                                target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                                plant = Instantiate(corn, target.transform.position, Quaternion.identity) as GameObject;
                                plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.cornGrowthTime;
                                plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.cornMoneyValue;
                                target.GetComponentInChildren<JDGroundClass>().occupiedWith = plant;
                                plant.GetComponentInChildren<JDPlantClass>().destroyOnHarvest = false;
                                plant.GetComponentInChildren<JDPlantClass>().plantedTile = target;
                            }

                            //if planting daikonSeed
                            if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.daikonSeed)
                            {
                                target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.seeded;
                                plant = Instantiate(daikon, target.transform.position, Quaternion.identity) as GameObject;
                                plant.GetComponentInChildren<JDPlantClass>().growthTime = JDStaticVariables.daikonGrowthTime;
                                plant.GetComponentInChildren<JDPlantClass>().moneyValue = JDStaticVariables.daikonMoneyValue;
                                target.GetComponentInChildren<JDGroundClass>().occupiedWith = plant;
                                plant.GetComponentInChildren<JDPlantClass>().plantedTile = target;
                            }
                        }//end of tilledDirtcheck

                        if (targetStatus == JDStaticVariables.tiles.seeded)
                        {
                            //print("tryingtoharvest");
                            if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.scythe)
                            {
                                // print("usingScythe");
                                // print((target.gameObject.GetComponentInChildren<JDGroundClass>().occupiedWith == rice));
                                // print(target.gameObject.GetComponentInChildren<JDGroundClass>().occupiedWith.name == "Rice(Clone)");
                                //if (target.gameObject.GetComponentInChildren<JDGroundClass>().occupiedWith == rice)

                                //if harvesting rice
                                if (target.gameObject.GetComponentInChildren<JDGroundClass>().occupiedWith.name == rice.name + "(Clone)")
                                {
                                    //print("GOT RICE?");
                                    //check if the plant can be harvested
                                    if (target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().CanHarvest())
                                    {
                                        //run the planted plant's 'harvest' function'
                                        target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().Harvest();

                                        //load the plant into the player's inventory
                                        JDStaticVariables.playerInventory = JDStaticVariables.inventory.ricePlant;
                                        print("harvested rice");
                                    }
                                }
                                //if harvesting leek
                                if (target.gameObject.GetComponentInChildren<JDGroundClass>().occupiedWith.name == leek.name + "(Clone)")
                                {
                                    //print("GOT RICE?");
                                    //check if the plant can be harvested
                                    if (target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().CanHarvest())
                                    {
                                        //run the planted plant's 'harvest' function'
                                        target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().Harvest();

                                        //load the plant into the player's inventory
                                        JDStaticVariables.playerInventory = JDStaticVariables.inventory.leekPlant;
                                        print("harvested leek");
                                    }
                                }
                                //if harvesting daikon
                                if (target.gameObject.GetComponentInChildren<JDGroundClass>().occupiedWith.name == daikon.name + "(Clone)")
                                {
                                    //print("GOT RICE?");
                                    //check if the plant can be harvested
                                    if (target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().CanHarvest())
                                    {
                                        //run the planted plant's 'harvest' function'
                                        target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().Harvest();

                                        //load the plant into the player's inventory
                                        JDStaticVariables.playerInventory = JDStaticVariables.inventory.daikonPlant;
                                        print("harvested daikon");
                                    }
                                }
                                //if harvesting corn
                                if (target.gameObject.GetComponentInChildren<JDGroundClass>().occupiedWith.name == corn.name + "(Clone)")
                                {
                                    //print("GOT RICE?");
                                    //check if the plant can be harvested
                                    if (target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().CanHarvest())
                                    {
                                        //run the planted plant's 'harvest' function'
                                        target.GetComponentInChildren<JDGroundClass>().occupiedWith.GetComponentInChildren<JDPlantClass>().Harvest();

                                        //load the plant into the player's inventory
                                        JDStaticVariables.playerInventory = JDStaticVariables.inventory.cornPlant;
                                        print("harvested corn");
                                    }
                                }
                            }//end of scythe check
                        }//end of seeded check


                        //base dirt
                        if (targetStatus == JDStaticVariables.tiles.dirt)
                        {
                            //if holding hoe
                            if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.hoe)
                            {
                                target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.tilledDirt;
                            }
                            //if holding fence
                            if(JDStaticVariables.playerInventory == JDStaticVariables.inventory.fence)
                            {
                                GameObject newFence = fence;
                                newFence = Instantiate(corn, target.transform.position, Quaternion.identity) as GameObject;
                                target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.fence;


                                target.GetComponentInChildren<JDGroundClass>().occupiedWith = newFence;    
                                newFence.GetComponentInChildren<JDFenceClass>().plantedTile = target;

                            }
                        }

                        //if seeded ground
                        if (targetStatus == JDStaticVariables.tiles.seeded)
                        {

                            //if want to water
                            if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.water)
                            {
                                //can only water seeded ground
                                target.GetComponentInChildren<JDGroundClass>()._tileStatus = JDStaticVariables.tiles.watered;
                            }
                        }

 
                            print(targetStatus);
                    }//end of ground check

                    if (target.gameObject == shippingBox)
                    {
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.ricePlant)
                        {
                            JDStaticVariables.shippingStock.Add(rice);
                        }
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.leekPlant)
                        {
                            JDStaticVariables.shippingStock.Add(leek);
                        }
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.daikonPlant)
                        {
                            JDStaticVariables.shippingStock.Add(daikon);
                        }
                        if (JDStaticVariables.playerInventory == JDStaticVariables.inventory.cornPlant)
                        {
                            JDStaticVariables.shippingStock.Add(corn);
                        }
                        //print("Shipping stuff");
                    }
                }//end of null check
            }
        }
    }
}
