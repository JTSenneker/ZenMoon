using UnityEngine;
using System.Collections;

/// <summary>
/// this class is for tracking the player's amount of zen in the HUD
/// </summary>
public class JDZenLength : MonoBehaviour {
    
	/// <summary>
    /// scale the bar according to the player's current zen
    /// </summary>
	void Update () {
        float scale = JDStaticVariables.zenTotal * .01f;
	transform.localScale = new Vector3 (scale,1,1);
	}
}
