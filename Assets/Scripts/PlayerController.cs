using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Comments && notes from past Steve!
    // remember THIS IS FOR THE PLAYER
    // Player facing is being affected and managed in the Gun arm elements section (thanks past steve)

    #region Variables

    public static PlayerController instance; // set a static instance of the player that will be accessible from other scripts
    public float moveSpeed; // Set the speed that player can move
    private Vector2 moveInput; // keeps track of what the player is pressing
    public Rigidbody2D rigidBody; // Reference to the player parent element rigidbody
    public Transform gunArm; // reference to the gun arm element of the player
    private Camera mainCamera; // reference to the main camera
    public Animator animator; // reference to the animator
    public GameObject bulletToFire; // reference to the player bullet element
    public Transform firePoint; // reference from where the bullet will be launched from
    public float timeBetweenShots; // how long to wait between each shot
    private float shotCounter; // to use to track how fast shots should be fired

    #endregion

    private void Awake()
    {
        instance = this; // to make a single instance of the player
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // reference the Main Camera one time only
    }

    // Update is called once per frame
    void Update()
    {

        #region Character movement basics

        moveInput.x = Input.GetAxisRaw("Horizontal"); // Get x axis input (1 or -1)
        moveInput.y = Input.GetAxisRaw("Vertical"); // Get y axis input (1 or -1)

        moveInput.Normalize(); // fixes the diagonal speed increase we don't want

        rigidBody.velocity = moveInput * moveSpeed; // Make the character move

        #endregion

        #region Character animations

        // validation if we are moving and switching the animations
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        #endregion

        #region Gun arm elements

        Vector3 mousePosition = Input.mousePosition; // gets mouse position in the "world"
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition); // get the position of the player in the main camera

        // validate if the mouse is on the left side of the player to make the player switch on the Y axis
        // PLAYER SIDE SWITCH
        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // reverses the sprite
            gunArm.localScale = new Vector3(-1f, -1f, 1f); // reverses the gun arm
        }
        else
        {
            transform.localScale = Vector3.one; // same as new Vector3(1f, 1f, 1f) but is faster to type :P
            gunArm.localScale = Vector3.one; // everything is on the right side now
        }


        // Rotate gun arm
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y); // Calculate the offset between the mouse and player in the scene
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; // Return the angle of the offset that the gun will need to be pointed at

        gunArm.rotation = Quaternion.Euler(0, 0, angle); // change the angle of the gun arm

        #endregion

        #region Bullet section

        // validate if the left mouse button is clicked (TODO: SWITCH TO FIRE1 TO SUPPORT controller)
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation); // Instantiate de bullet
        }

        // add auto fire while holding the left mouse button
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime; // decrease the shotCounter every frame

            // if shotcounter is below or equals to 0 we SHOOT A BULLET!!!
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation); // Instantiate de bullet

                shotCounter = timeBetweenShots; // reset the shotCounter to the value of timeBetweenshots to rest the countdown between two bullets being fired
            }
        }

        #endregion
    }
}
