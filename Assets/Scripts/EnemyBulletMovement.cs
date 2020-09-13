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

        Debug.Log("bullet" + bulletNumber + "targetVector was " + targetVector);

        ////1: up - z = 50
        ////2: right - x = 50
        ////3: down - z = -50
        ////4: left - x = -50

        if (chosenDirection == 1)
        {
            Debug.Log("should be up");
            targetVector.z = 50;
        }
        if (chosenDirection == 2)
        {
            Debug.Log("should be right");
            targetVector.x = 50;
        }
        if (chosenDirection == 3)
        {
            Debug.Log("should be down");
            targetVector.z = -50;
        }
        if (chosenDirection == 4)
        {
            Debug.Log("should be left");
            targetVector.x = -50;
        }

        Debug.Log("bullet" + bulletNumber + "targetVector is " + targetVector);

        //Instantiate(target, targetVector, transform.rotation);

        ////var chosenTarget = GameObject.Find("targetTransform" + initialWaypoint + "_" + chosenDirection);

        //targetTransform = target.GetComponent<Transform>();

        //Debug.Log("targetTransform" + initialWaypoint + "_" + chosenDirection + " which is...");

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
        Debug.Log("bullet" + bulletNumber + " fired from " + initialTransform.position + " reports that the target is: " + targetVector);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
    }

    void Update()
    {
        ////if (chosenDirection == 0 || initialTransform == null)
        ////{
        ////    Destroy(gameObject);
        ////}

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

        Vector3 directionToTarget = targetVector - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

        Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f); //Draws a ray forward in the direction the enemy is facing
        Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f); //Draws a ray in the direction of the current targetTransform waypoint

        float distance = Vector3.Distance(transform.position, targetVector);

        transform.position = Vector3.MoveTowards(transform.position, targetVector, movementStep);

        if (lifeTime < Time.time)
        {
            //Debug.Log("Destroyed");

            Destroy(gameObject); // destroy the bullet
        }

        //if (logTime < Time.time)
        //{
        //    Debug.Log("bullet" + bulletNumber + " reports");

        //    logTime = Time.time + 3;
        //}

        // just in case despawns projectile a short distant before hitting the map boundary
        if (transform.position.z < BoardConstraintSouth
            || transform.position.z > BoardConstraintNorth
            || transform.position.x < BoardConstraintWest
            || transform.position.x > BoardConstraintEast)
        {
            Destroy(gameObject);
        }
    }

    //private void OnDestroy()
    //{
    //    // cleans up target if it hadn't been done already
    //    if (initialTransform != null)
    //    {
    //        Destroy(target);
    //    }
    //}

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