using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject enemyTarget;
    public GameObject enemyBullet;
    public float enemyFireRate;
    public float stopForTime;

    private float nextFireTime;
    private float nextStopTime;

    private List<Transform> waypoints = new List<Transform>();
    private int numberOfWaypoints;
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f; //If the distance between the enemy and the waypoint is less than this, then it has reached the waypoint
    private int lastWaypointIndex;

    private float movementSpeed = 2.5f;
    private float rotationSpeed = 2.0f;

    private int chosenDirection = 1;

    private int bulletNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        var waypointsObject = GameObject.Find("Waypoints");
        numberOfWaypoints = waypointsObject.GetComponent<Transform>().childCount;

        GameObject currentWaypoint;

        for (int i = 1; i <= numberOfWaypoints; i++)
        {
            currentWaypoint = GameObject.Find("Waypoint" + i);

            waypoints.Add(currentWaypoint.GetComponent<Transform>());
        }

        nextFireTime = Time.time + enemyFireRate;

        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex]; //Set the first target waypoint at the start so the enemy starts moving towards a waypoint
    }

    void Update()
    {
        // stop for a moment after reaching waypoint
        if (nextStopTime < Time.time)
        {
            float movementStep = movementSpeed * Time.deltaTime;
            float rotationStep = rotationSpeed;

            Vector3 directionToTarget = targetWaypoint.position - transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

            Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f); //Draws a ray forward in the direction the enemy is facing
            Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f); //Draws a ray in the direction of the current target waypoint

            float distance = Vector3.Distance(transform.position, targetWaypoint.position);
            CheckDistanceToWaypoint(distance);

            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
        }
    }

    /// <summary>
    /// Checks to see if the enemy is within distance of the waypoint. If it is, it called the UpdateTargetWaypoint function 
    /// </summary>
    /// <param name="currentDistance">The enemys current distance from the waypoint</param>
    void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            shoot();

            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    /// <summary>
    /// Increaes the index of the target waypoint. If the enemy has reached the last waypoint in the waypoints list, it resets the targetWaypointIndex to the first waypoint in the list (causes the enemy to loop)
    /// </summary>
    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }

        targetWaypoint = waypoints[targetWaypointIndex];
    }

    private void shoot()
    {
        if (nextFireTime < Time.time)
        {
            var initialPosition = transform.position;

            var initialRotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);

            Instantiate(enemyBullet, initialPosition, initialRotation);

            var scriptRef = enemyBullet.GetComponent<EnemyBulletMovement>();

            scriptRef.chosenDirection = Random.Range(1, 4);
            scriptRef.initialTransform = transform;
            scriptRef.initialWaypoint = targetWaypointIndex + 1;
            scriptRef.bulletNumber = bulletNumber++;

            nextFireTime = Time.time + enemyFireRate;
            nextStopTime = nextFireTime - stopForTime;
        }
    }
}