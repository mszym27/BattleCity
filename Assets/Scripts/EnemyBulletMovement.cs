using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    public float movementSpeed = 5;
    public int chosenDirection;
    public int initialWaypoint;
    public float lifeTime = 5;
    public GameObject target;
    public Transform initialTransform;

    public int bulletNumber;

    private const float BoardConstraintSouth = -9.5f;
    private const float BoardConstraintNorth = 3.5f;
    private const float BoardConstraintWest = -6.5f;
    private const float BoardConstraintEast = 6.5f;

    private float minTime;
    private Transform targetTransform;
    private float minDistance = 0.1f;

    private float logTime = 0;
    private Vector3 targetVector;

    void Start()
    {
        StartCoroutine(Wait());

        lifeTime = Time.time + 0.85f;

        targetVector = new Vector3(initialTransform.position.x, initialTransform.position.y, initialTransform.position.z);

        if (chosenDirection == 1)
        {
            targetVector.z = 50;
        }
        if (chosenDirection == 2)
        {
            targetVector.x = 50;
        }
        if (chosenDirection == 3)
        {
            targetVector.z = -50;
        }
        if (chosenDirection == 4)
        {
            targetVector.x = -50;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
    }

    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        float rotationStep = 2.0f;

        Vector3 directionToTarget = targetVector - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

        Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f); //Draws a ray forward in the direction the enemy is facing
        Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f); //Draws a ray in the direction of the current targetTransform waypoint

        float distance = Vector3.Distance(transform.position, targetVector);

        transform.position = Vector3.MoveTowards(transform.position, targetVector, movementStep);

        if (lifeTime < Time.time)
        {
            Destroy(gameObject); // destroy the bullet
        }

        // just in case despawns projectile a short distant before hitting the map boundary
        if (transform.position.z < BoardConstraintSouth
            || transform.position.z > BoardConstraintNorth
            || transform.position.x < BoardConstraintWest
            || transform.position.x > BoardConstraintEast)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Enemy") // as in original game their bullets will pass through other enemy tanks
        {
            if (other.gameObject.tag != "Indestructible")
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject); // destroy the bullet
        }
    }
}