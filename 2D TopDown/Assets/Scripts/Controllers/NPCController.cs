using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class NPCController : MonoBehaviour
{
    public LayerMask obstacleLayer;

    [Header("Waypoint Settings")]
    public GameObject[] waypoints;
    public bool moveRandom;
    [Tooltip("Radius to generate random waypoints within")]
    public int wanderingRadius; // Radius of the circle to generate random waypoints in
    public float allowance;     // Distance the enemy must come within to "reach" the waypoint
    [Tooltip("Center of circle for random points to spawn in")]
    public GameObject randomCenter;

    private GameObject currentWaypoint;
    private int waypointIndex;  // index of current waypoint

    [Header("Enemy Settings")]
    public float moveSpeed = 1.5f;
    

    private void Start()
    {
        if (moveRandom) {
            currentWaypoint.transform.position = SelectRandomPoint();
        }
        else {
            waypointIndex = 0;
            currentWaypoint = waypoints[waypointIndex];
        }    
    }

    
    private void Update()
    {
        Patrol();
    }


    /// <summary>
    /// Patrols either by moving through the list of waypoints or moving to
    /// random points within a set circle
    /// </summary>
    private void Patrol() {
        if (moveRandom) {
            if (Physics2D.OverlapCircleAll(gameObject.transform.position, wanderingRadius, obstacleLayer).Length != 0) {
                currentWaypoint.transform.position = SelectRandomPoint();
            }
        }

        if (WaypointReached(gameObject.transform.position, currentWaypoint.transform.position, allowance)) {
            UpdateWaypoint();
        }

        // Move to waypoint
        gameObject.transform.position = Vector2.MoveTowards(
            gameObject.transform.position, currentWaypoint.transform.position,
            moveSpeed * Time.deltaTime);
    }


    /// <summary>
    /// Used to indicate when the enemy reaches the waypoint
    /// </summary>
    private bool WaypointReached(Vector2 pos, Vector2 target, float allowance) {
        return Vector2.Distance(pos, target) <= allowance;
    }


    /// <summary>
    /// Updates the waypoint index and cucles through the list of waypoints
    /// </summary>
    private void UpdateWaypoint() {
        waypointIndex++;
        waypointIndex = waypointIndex % waypoints.Length;   // Cycle from 0 to end
        currentWaypoint = waypoints[waypointIndex];
    }


    /// <summary>
    /// Selects a random point within a circle with given range
    /// </summary>
    /// <returns></returns>
    private Vector2 SelectRandomPoint() {
        var point = Random.insideUnitCircle * wanderingRadius;  // Get random point in circle
        point.y = 0;    // Cancel any change in y
        point += (Vector2)gameObject.transform.position;    //Add difference to current location

        return point;
    }
}
