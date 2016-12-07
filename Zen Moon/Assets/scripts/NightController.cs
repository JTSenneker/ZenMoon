using UnityEngine;
using System.Collections;

public class NightController : MonoBehaviour {

    public GameObject basicCreature;
    public float creatureMultiplier = 1.5f;
    int dayCount;
    int totalCreatures;

	void Start ()
    {
        dayCount = JDStaticVariables.dayCount;
	}
	
	void Update ()
    {
        totalCreatures = (int)Mathf.Ceil(dayCount * creatureMultiplier);

        for (int i = 0; i < totalCreatures; i++)
        {
            Instantiate(basicCreature);
        }
	}
}
