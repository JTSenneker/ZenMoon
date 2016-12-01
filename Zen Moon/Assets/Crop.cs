using UnityEngine;
using System.Collections;

public class Crop : MonoBehaviour {

	public enum CropType
    {
        daikon,
        leek,
        corn,
        rice
    }

    public CropType cropType;
}
