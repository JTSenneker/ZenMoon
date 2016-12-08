using UnityEngine;
using System.Collections;

public class JDZenLength : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float scale = JDStaticVariables.zenTotal * .01f;
	transform.localScale = new Vector3 (scale,1,1);
	}
}
