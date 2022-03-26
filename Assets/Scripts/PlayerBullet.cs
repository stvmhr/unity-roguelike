using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float moveSpeed = 7.5f; // sets the speed that the bullet will move at
    public Rigidbody2D rigidBody; // references to the bullet RigidBody2D
    public GameObject impactEffect; // reference the bullet impact effect

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = transform.right * moveSpeed; // the bullet will continu to move to the right
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation); // instantiate the impact effect on collision
        Destroy(gameObject); // Destroy the bullet on collision
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
