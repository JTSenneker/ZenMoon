using UnityEngine;
using System.Collections;

/// <summary>
/// Creates a boundary around the interactable ground
/// </summary>
public class CreateBoundary : MonoBehaviour
{
    /// <summary>
    /// The object for the boundary
    /// </summary>
    public GameObject boundary;
    /// <summary>
    /// The grid width
    /// </summary>
    public int gridWidth;
    /// <summary>
    /// The grid height
    /// </summary>
    public int gridHeight;

    /// <summary>
    /// Places the boundary around the ground
    /// </summary>
	void Start()
    {
        for (int i = 0; i < gridHeight + 1; i++)
        {
            Vector2 rightBoundPlace = new Vector2(-1, i);
            Vector2 leftBoundPlace = new Vector2(gridWidth, i);
            Instantiate(boundary, rightBoundPlace, Quaternion.identity);
            Instantiate(boundary, leftBoundPlace, Quaternion.identity);
        }
        for (int i = 0; i < gridWidth; i++)
        {
            Vector2 topBoundPlace = new Vector2(i, -1);
            Vector2 bottomBoundPlace = new Vector2(i, gridHeight + 1);
            Instantiate(boundary, topBoundPlace, Quaternion.identity);
            Instantiate(boundary, bottomBoundPlace, Quaternion.identity);
        }
    }
}
