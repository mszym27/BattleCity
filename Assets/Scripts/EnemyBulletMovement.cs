using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    public float movementSpeed = 5;
    public int chosenDirection;
    public int initialWaypoint;
    public float lifeTime = 5;

    public int bulletNumber;

    private const float BoardConstraintSouth = -9.5f;
    private const float BoardConstraintNorth = 3.5f;
    private const float BoardConstraintWest = -6.5f;
    private const float BoardConstraintEast = 6.5f;

    private float minTime;
    private Transform target;
    private float minDistance = 0.1f;

    private float logTime = 0;

    void Start()
    {
        minTime = Time.time + 1;

        lifeTime = Time.time + 0.85f;

        //lifeTime

        var chosenTarget = GameObject.Find("Target" + initialWaypoint + "_" + chosenDirection);

        target = chosenTarget.GetComponent<Transform>();

        //Debug.Log("Target" + initialWaypoint + "_" + chosenDirection + " which is...");

        //if (chosenDirection == 1)
        //{
        //    Debug.Log("up");
        //}
        //if (chosenDirection == 2)
        //{
        //    Debug.Log("right");
        //}
        //if (chosenDirection == 3)
        //{
        //    Debug.Log("down");
        //}
        //if (chosenDirection == 4)
        //{
        //    Debug.Log("left");
        //}
        ////Debug.Log("Destroy " + lifeTime + " now " + Time.time);
    }

    void Update()
    {
        if (Time.time < minTime)
        {
            //Quaternion initialRotation = new Quaternion(0, -0.7f, 0, 0.7f);

            //// left
            //if (chosenDirection == 1)
            //{
            //    initialRotation = new Quaternion(0, -0.7f, 0, 0.7f);
            //}
            //// right
            //if (chosenDirection == 2)
            //{
            //    initialRotation = new Quaternion(0, 0.7f, 0, 0.7f);
            //}
            //// down
            //if (chosenDirection == 3)
            //{
            //    initialRotation = new Quaternion(0, -0.7f, 0, 0);
            //}
            //// up
            //if (chosenDirection == 4)
            //{
            //    initialRotation = new Quaternion(0, 0, 0, 1);
            //}

            //transform.position += initialRotation.ToEuler() * Time.deltaTime * movementSpeed;

            float movementStep = movementSpeed * Time.deltaTime;
            float rotationStep = 2.0f;

            Vector3 directionToTarget = target.position - transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

            Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f); //Draws a ray forward in the direction the enemy is facing
            Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f); //Draws a ray in the direction of the current target waypoint

            float distance = Vector3.Distance(transform.position, target.position);

            transform.position = Vector3.MoveTowards(transform.position, target.position, movementStep);

            if (lifeTime < Time.time)
            {
                Debug.Log("Destroyed");

                Destroy(gameObject); // destroy the bullet
            }

            if (logTime < Time.time)
            {
                Debug.Log("bullet" + bulletNumber + " reports");

                logTime = Time.time + 3;
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