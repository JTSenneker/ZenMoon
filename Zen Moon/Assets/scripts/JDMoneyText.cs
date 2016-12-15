using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// this class sets the money value to be the correct amount in the UI
/// </summary>
public class JDMoneyText : MonoBehaviour {
    /// <summary>
    /// the text of the text box
    /// </summary>
    Text text;
    /// <summary>
    /// get the text component of the textbox
    /// </summary>
	void Start () {
        text = GetComponent<Text>();
	}
    /// <summary>
    /// we set the text box to display the amount of money we have
    /// </summary>
    void Update () {
        text.text = "Yen: " + JDStaticVariables.moneyTotal;
	
	}
}
