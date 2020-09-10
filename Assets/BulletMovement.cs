using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    //public Rigidbody rb;

    private const float BoardConstraintSouth = -9.5f;
    private const float BoardConstraintNorth = 3.5f;
    private const float BoardConstraintWest = -6.5f;
    private const float BoardConstraintEast = 6.5f;

    //wlasciwe granice
    //private enum BoardConstraints
    //{
    //    south = -10,
    //    north = 4,
    //    west = -7,
    //    east = 7
    //};

    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = this.GetComponent<Rigidbody>();
    //}

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;

        // despawns projectile a short distant before hitting the map boundary
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
        if (other.gameObject.tag != "Indestructible")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject); // destroy the bullet
    }
}
