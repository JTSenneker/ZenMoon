using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// this class sets the 'day' textbox's text to the correct value
/// </summary>
public class JDDayTextSet : MonoBehaviour {
    /// <summary>
    /// the text box
    /// </summary>
    Text text;

/// <summary>
/// we get the component of the text variable
/// </summary>
	void Start () {
        text = GetComponent<Text>();
	}
	
	/// <summary>
    /// this updates the text in the text box
    /// </summary>
	void Update () {
        text.text = "Day: " + JDStaticVariables.dayCount;
	}
}
