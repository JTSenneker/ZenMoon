using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the camera's position and what it focuses on
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// The camera's focus during the day
    /// </summary>
    public GameObject dayTarget;
    /// <summary>
    /// The camera's focus during the night
    /// </summary>
    public GameObject nightTarget;
    /// <summary>
    /// The camera's current focus
    /// </summary>
    GameObject target;

    /// <summary>
    /// Sets the camera's target to the day target
    /// </summary>
    void Start()
    {
        target = dayTarget;
    }

    /// <summary>
    /// moves the camera based on which target it should be focusing on
    /// </summary>
	void Update ()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
	}

    /// <summary>
    /// Switches the target to the day target
    /// </summary>
    public void switchDay()
    {
        nightTarget.transform.position = new Vector3(4, 4, transform.position.z);
        target = dayTarget;
    }

    /// <summary>
    /// Switches the target to the night target
    /// </summary>
    public void switchNight()
    {
        target = nightTarget;
    }
}
