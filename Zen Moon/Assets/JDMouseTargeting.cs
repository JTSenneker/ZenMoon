using UnityEngine;
using System.Collections;

public class JDMouseTargeting : MonoBehaviour {

    public GameObject helper;
    public static GameObject mouseHelper;
    public static GameObject target;
   static Vector3 basePosition;

	// Use this for initialization
	void Start () {
        mouseHelper = helper;

        basePosition = new Vector3(5, -5, 5);

        //create the helper object, spawn it below the board right now
        mouseHelper = Instantiate(mouseHelper, basePosition, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        target = GetMouseTarget();
     
	}
    /// <summary>
    /// this function figures out what tile the mouse is hovering over
    /// </summary>
    /// <returns>Returns the tile the mouse is hovering over</returns>
   public static GameObject GetMouseTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 5);

        if (hit.collider != null)
        {
            mouseHelper.transform.position = hit.collider.transform.position;

           // if (Input.GetButtonDown("Fire1")) GetGroundTile(hit.collider.gameObject);

            //print("helper " + mouseHelper.transform.position);
            //print(hit.collider.name);
            //print(hit.collider.transform.position);
            return hit.collider.gameObject;
        }
        else mouseHelper.transform.position = basePosition;
        return null;
    }
    
 
    /// <summary>
    /// This function returns the status of the target tile
    /// </summary>
    /// <param name="target"> the tile we want the status of</param>
    /// <returns>return the status of the tile</returns>
  public static JDStaticVariables.tiles GetGroundTile(GameObject target)
    {
        //print(target.GetComponentInChildren<JDGroundClass>()._tileStatus);
        return target.GetComponentInChildren<JDGroundClass>()._tileStatus;
    }
}
