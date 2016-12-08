using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JDDayTextSet : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Day: " + JDStaticVariables.dayCount;
        //print(text.text);
	}
}
