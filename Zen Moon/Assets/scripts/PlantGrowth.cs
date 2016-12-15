using UnityEngine;
using System.Collections;

/// <summary>
/// An enum of the possible seeds the player can plant
/// </summary>
public enum SeedType {
    daikon,
    leek,
    corn,
    rice
}

public class PlantGrowth : MonoBehaviour {
    /// <summary>
    /// This keeps track of what kind of seed this plant is growing from.
    /// </summary>
    public SeedType seedType = SeedType.daikon;

    /// <summary>
    /// These are the various animator controllers that the plants will use
    /// It will decide which one to use based on the seedType enum
    /// These are public so the designer can create add the controllers in engine
    /// </summary>
    public RuntimeAnimatorController cornAnimControl;
    public RuntimeAnimatorController daikonAnimControl;
    public RuntimeAnimatorController leekAnimControl;
    public RuntimeAnimatorController riceAnimControl;

    /// <summary>
    /// This is essentially the age of the plant.
    /// It will be used to keep track of how much the plant has grown
    /// It is public so the Game Controller can access it for saving purposes.
    /// </summary>
    public int plantGrowth;

    /// <summary>
    /// This is how much the plant's monetary value for when it is harvested
    /// </summary>
    public int moneyValue;
    
    /// <summary>
    /// this is a refence to the tile the plant is planted on.
    /// </summary>
    public GameObject plantedTile;
    
    /// <summary>
    /// This is the amount of time it takes for a plant to grow to a point where it can be harvested
    /// </summary>
    private int growthTime;

    /// <summary>
    /// This bool keeps track of whether or not this plant is renewable
    /// renewable plants will not have to be replanted after harvest.
    /// </summary>
    private bool renewable;

    /// <summary>
    /// This bool keeps track of whether or not this plant is harvest able
    /// </summary>
    private bool harvestable;
    /// <summary>
    /// This is the animator that will be used to switch the sprites according to the how
    /// much the plant has grown.
    /// </summary>
    private Animator anim;
	

    /// <summary>
    /// This will run when the seed is instantiated.
    /// What this will do is determine what kind of seed is planted, and set the AnimationController,
    /// moneyValue, growthTime and renewable variables to their proper values.
    /// It will also set any component references to their respective components
    /// </summary>
	void Start () {
        anim = GetComponent<Animator>();
        switch (seedType) {
            case SeedType.corn:
                anim.runtimeAnimatorController = cornAnimControl;
                moneyValue = 60;
                growthTime = 7;
                renewable = true;
                break;
            case SeedType.daikon:
                anim.runtimeAnimatorController = daikonAnimControl;
                moneyValue = 20;
                growthTime = 4;
                renewable = false;
                break;
            case SeedType.leek:
                anim.runtimeAnimatorController = leekAnimControl;
                moneyValue = 40;
                growthTime = 5;
                renewable = false;
                break;
            case SeedType.rice:
                anim.runtimeAnimatorController = riceAnimControl;
                moneyValue = 80;
                growthTime = 10;
                renewable = true;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        harvestable = (plantGrowth >= growthTime);

        /*SUMMARY
         * This will set the Growth paramater of the animator
         * so the plant will properly transition when it needs to
        */
        anim.SetInteger("Growth", plantGrowth);
	}

    public void Grow() {
        if (plantedTile.GetComponentInChildren<JDGroundClass>()._tileStatus == JDGroundClass.tiles.watered) {
            plantGrowth++;
        }
    }
    public void Harvest() {
        if (renewable) {
            switch (seedType) {
                case SeedType.corn:
                    plantGrowth = 4;
                    break;
                case SeedType.rice:
                    plantGrowth = 7;
                    break;
            }
        }else if(!renewable){
            Destroy(this.gameObject);
        }
    }
}
