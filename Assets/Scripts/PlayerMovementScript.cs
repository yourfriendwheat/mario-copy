using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] AudioClip JumpSFX;
    [SerializeField] [Range(0f, 1f)] float jumping = .5f;

    bool isAlive = true;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    //AudioSource jumpSFX;


    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        //jumpSFX = GetComponent<AudioSource>();

    }

    
    void FixedUpdate()
    {
        if(!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        //Die();
        //CheckForEnemyCollision();
    }


    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
            if (value.isPressed)
            {
            //AudioSource.PlayClipAtPoint(JumpSFX, Camera.main.transform.position);
            PlayClip(JumpSFX, jumping);
            myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
            }
        
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    void Run()
    {
        
            
            Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.linearVelocity.y);
            myRigidbody.linearVelocity = playerVelocity;


            bool playerHasHorizotalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("IsRunning", playerHasHorizotalSpeed);
        
        /*else if(!isAlive)
        {
            Debug.Log("I am the storm");
            runSpeed = 0;
            jumpSpeed = 0;
        }*/
        

    }

    void FlipSprite()
    {
        bool playerHasHorizotalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizotalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }


    private bool hasTriggeredDeath = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool FeetTouchHazard = GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Hazards"));

        if (FeetTouchHazard && !hasTriggeredDeath)
        {
            hasTriggeredDeath = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().ProcessPlayerDeath();
        }
    }


    /*public void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            // Get the collider that's causing the death
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(myFeetCollider.bounds.center, myFeetCollider.bounds.size, 0f, LayerMask.GetMask("Enemies"));
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemies"))
                {
                    // Process enemy collision only once
                    if (!isAlive)
                    {
                        return;
                    }
                       

                    isAlive = false;  // Prevent multiple death calls
                    myAnimator.SetTrigger("Dying");

                    if (playerInput != null)
                    {
                        playerInput.actions.Disable();  // Disable all input actions
                    }

                    GameManager gameSession = Object.FindFirstObjectByType<GameManager>();
                    if (gameSession != null)
                    {
                        gameSession.ProcessPlayerDeath();
                    }
                    else
                    {
                        Debug.LogError("GameSession not found in the scene!");
                    }
                    break;
                }
            }
        }
    }*/

    /*void CheckForEnemyCollision()
    {
        if (myRigidbody.linearVelocity.y < 0) // Only check when falling
        {
            // Use OverlapCircle to detect enemies directly below the player's feet
            Collider2D enemyCollider = Physics2D.OverlapCircle(myFeetCollider.bounds.center, 0.1f, LayerMask.GetMask("Enemies"));

            if (enemyCollider != null)
            {
                // If falling with enough downward speed, destroy the enemy
                if (myRigidbody.linearVelocity.y < -5f) // You can adjust this threshold for the fall speed
                {
                    DestroyEnemy(enemyCollider);
                    AddPoints(10); // Add points when the enemy is destroyed
                }
            }
        }
    }*/

    /*void DestroyEnemy(Collider2D enemyCollider)
    {
        // Destroy the enemy object
        Destroy(enemyCollider.gameObject);
    }*/

    /*void AddPoints(int points)
    {
        // Assuming you have a GameManager with a method to add points
        GameManager gameSession = Object.FindFirstObjectByType<GameManager>();
        if (gameSession != null)
        {
            gameSession.EarnScore(points);
        }
        else
        {
            Debug.LogError("GameSession not found in the scene!");
        }
    }*/


}
