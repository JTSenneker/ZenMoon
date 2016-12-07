using UnityEngine;
using System.Collections;

/// <summary>
/// Detects collision using ray casting
/// </summary>
public class CollisionController : MonoBehaviour
{
    /// <summary>
    /// The amount within the box collider that the ray cast starts in
    /// </summary>
    const float skinWidth = .015f;
    /// <summary>
    /// The amount of rays casted in the horizontal direction
    /// </summary>
    public int horizontalRayCount = 4;
    /// <summary>
    /// The amount of rays casted in the vertical direction
    /// </summary>
    public int verticalRayCount = 4;
    /// <summary>
    /// The layers that the collision detection is on
    /// </summary>
    public LayerMask layerMask;

    /// <summary>
    /// The spacing between each horizontal ray
    /// </summary>
    float horizontalRaySpacing;
    /// <summary>
    /// The spacing between each vertical ray
    /// </summary>
    float verticalRaySpacing;

    /// <summary>
    /// The box collider on the object
    /// </summary>
    BoxCollider2D col;
    /// <summary>
    /// The origin points of each ray cast
    /// </summary>
    RaycastOrigins raycastOrigins;

    /// <summary>
    /// The description for each origin point which is the four coordinates
    /// </summary>
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

	/// <summary>
    /// Instantiates variables
    /// </summary>
	void Start ()
    {
        col = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
	}

    /// <summary>
    /// Alters the speed according to how far the player is from the collider
    /// </summary>
    /// <param name="speed">The speed the player is going</param>
    public void Move(ref Vector3 speed)
    {
        UpdateRaycastOrigins();

        if (speed.x != 0)
        {
            HorizontalCollisons(ref speed);
        }
        if (speed.y != 0)
        {
            VerticalCollisons(ref speed);
        }
    }

    /// <summary>
    /// Alters the speed based on the horizontal distance the player is from the collider
    /// </summary>
    /// <param name="speed">How fast the player is moving</param>
    void HorizontalCollisons(ref Vector3 speed)
    {
        float directionX = Mathf.Sign(speed.x);
        float rayLength = Mathf.Abs(speed.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            //If direction X is negative then set ray origin to bottom left otherwise set to bottom right
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, layerMask);

            //Testing code
            //Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength);

            if (hit)
            {
                speed.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
            }
        }
    }

    /// <summary>
    /// Alters the speed based on the vertical distance the player is from the collider
    /// </summary>
    /// <param name="speed">How fast the player is moving</param>
    void VerticalCollisons(ref Vector3 speed)
    {
        float directionY = Mathf.Sign(speed.y);
        float rayLength = Mathf.Abs(speed.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            //If direction Y is negative then set ray origin to bottom left otherwise set to top left
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, layerMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength);

            if (hit)
            {
                speed.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
            }
        }
    }

    /// <summary>
    /// moves the rays starting points based on where the box collider is
    /// </summary>
    void UpdateRaycastOrigins()
    {
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    /// <summary>
    /// Clamps the ray counts to a minimum of 2 rays and finds how much space there should be between the rays
    /// </summary>
    void CalculateRaySpacing()
    {
        Bounds bounds = col.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
