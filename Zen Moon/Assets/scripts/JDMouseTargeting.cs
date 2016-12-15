using UnityEngine;
using System.Collections;

/// <summary>
/// this class uses the mouse to target tiles within a certain range of the player
/// </summary>
public class JDMouseTargeting : MonoBehaviour
{
    /// <summary>
    /// the gameobject we're using for targeting
    /// </summary>
    public GameObject helper;
    /// <summary>
    /// where the player is
    /// </summary>
    public Transform playerLocation;
    /// <summary>
    /// use this as the static variable for where the player is
    /// </summary>
    public static Transform player;
    /// <summary>
    /// the static variable of the mouse helper
    /// </summary>
    public static GameObject mouseHelper;
    /// <summary>
    /// what object we are currently targeting
    /// </summary>
    public static GameObject target;
/// <summary>
/// how far away the player can target squares with the mouse
/// </summary>
    public static float targetRange = 1.5f;

    /// <summary>
    /// we set the static variables and instantiate the helper
    /// </summary>
    void Start()
    {

        mouseHelper = helper;
        player = playerLocation;

        //create the helper object
        mouseHelper = Instantiate(mouseHelper, new Vector3(0,0,0), Quaternion.identity) as GameObject;
        //default the helper to not active, turns on when needed
        mouseHelper.SetActive(false);
    }

    void Update()
    {
        target = GetMouseTarget();

    }

    /// <summary>
    /// this function figures out what tile the mouse is hovering over
    /// </summary>
    /// <returns>Returns the gameObject the mouse is hovering over</returns>
    public static GameObject GetMouseTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 5);

        if (hit.collider != null)
        {
            Vector3 distance = new Vector3();
            distance = player.position - hit.collider.transform.position;
            distance = new Vector3(Mathf.Abs(distance.x), Mathf.Abs(distance.y), Mathf.Abs(distance.z));
            if (distance.x <= targetRange && distance.y <= targetRange && distance.z <= targetRange)
            {
                mouseHelper.SetActive(true);
                mouseHelper.transform.position = hit.collider.transform.position;
                return hit.collider.gameObject;
            }
            // else mouseHelper.transform.position = basePosition;
            else mouseHelper.SetActive(false);

            return null;

            /*
                RaycastHit2D hit2;
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit2 = Physics2D.Raycast(Camera.main.transform.position, ray2.direction);
                if(hit2.collider != null) {
                    Vector2 distance = new Vector2();
                    distance = player.position - hit2.collider.transform.position;
                    distance = new Vector2(Mathf.Abs(distance.x), Mathf.Abs(distance.y));
                        if (distance.x <= targetRange && distance.y <= targetRange)
                        {
                        mouseHelper.SetActive(true);
                            mouseHelper.transform.position = hit2.collider.transform.position;
                            return hit2.collider.gameObject;
                        }
                        else
                        mouseHelper.SetActive(false);
                        return null;
    */
        }
        // else mouseHelper.transform.position = basePosition;
        else mouseHelper.SetActive(false);
        return null;
    }


    /// <summary>
    /// This function returns the status of the target tile
    /// </summary>
    /// <param name="target"> the tile we want the status of</param>
    /// <returns>return the status of the tile</returns>
    public static JDGroundClass.tiles GetGroundTile(GameObject target)
    {
        return target.GetComponentInChildren<JDGroundClass>()._tileStatus;
    }
}
