using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Player Movement & Interaction
/// </summary>

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    [Header("Player Attributes")]
    public float defaultSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sprintSpeed;
    
    
    private Vector2 moveDirection;

    public Animator animator;


    private void Awake () {
        speed = defaultSpeed;
        moveDirection = Vector2.zero;
        
        if (instance == null) {
            instance = this;    // Set this instance as the instance reference
        }
        else if (instance != this) {
            Destroy(gameObject);    // Instance already set, destroy this
        }
        DontDestroyOnLoad(gameObject);  // Retain gameObject through scenes
	}
	
	// Update is called once per frame
	private void Update () {
        GetInput();
        Move();
        
	}


    public void Move() {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }


    /// <summary>
    /// Gets Keyboard Input
    /// </summary>
    private void GetInput() {
        float moveH = Input.GetAxisRaw("Horizontal");
        float moveV = Input.GetAxisRaw("Vertical");
        
        moveDirection = Vector2.zero;
        // Reset Animation to Idle
        animator.SetBool("North", false);
        animator.SetBool("South", false);
        animator.SetBool("West", false);
        animator.SetBool("East", false);

        // Move Up
        if (moveV == 1) {
                moveDirection += Vector2.up;
                animator.SetBool("North", true);

        }

        // Move Left
        else if (moveH == -1) {
                moveDirection += Vector2.left;
                animator.SetBool("West", true);     
        }

        // Move Down
        else if (moveV == -1) {
                moveDirection += Vector2.down;
                animator.SetBool("South", true);

        }

        // Move Right
        else if (moveH == 1) {
                moveDirection += Vector2.right;
                animator.SetBool("East", true);


        }

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = sprintSpeed;
            animator.speed = 1.1f;
        }
        // Return to walk
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = defaultSpeed;
            animator.speed = 1;

        }
    }

}
