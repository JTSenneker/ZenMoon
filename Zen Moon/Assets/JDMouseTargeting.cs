using UnityEngine;
using System.Collections;

public class JDMouseTargeting : MonoBehaviour {

    public GameObject mouseHelper;
    Vector3 basePosition;

	// Use this for initialization
	void Start () {

        basePosition = new Vector3(5, -5, 5);

        //create the helper object, spawn it below the board right now
        mouseHelper = Instantiate(mouseHelper, basePosition, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue,5);

        if (hit.collider != null)
        {
            //mouseHelper.transform.position = hit.collider.transform.position;
            mouseHelper.transform.position = new Vector3(hit.collider.transform.position.x, 0, hit.collider.transform.position.z);
            //print("helper " + mouseHelper.transform.position);
            //print(hit.collider.name);
            //print(hit.collider.transform.position);
        }
        else transform.position = basePosition;


	}
}
