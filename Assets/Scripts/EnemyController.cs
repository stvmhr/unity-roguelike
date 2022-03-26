using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Comments && notes from past Steve!
    // remember THIS IS FOR THE ENEMY

    public Rigidbody2D rigidBody; // Enemys rigid body reference
    public float moveSpeed; // enemy moving speed
    public float rangeToChasePlayer; // how far away the player needs to be for the enemy to chase him
    private Vector3 moveDirection; // position versus the player
    public Animator animator; // references the enemy animator

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        #region Enemy chase logic

        // validate if the player is in chasing distance
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            // chase the player
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero; // the enemy won't move if the player get out of reach
        }

        moveDirection.Normalize(); // fixes the diagonal speed increase we don't want

        rigidBody.velocity = moveDirection * moveSpeed; // actually move the enemy

        // if in chasing mode switch the animations
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("isChasing", true);
        }
        else
        {
            animator.SetBool("isChasing", false);
        }

        #endregion
    }
}
