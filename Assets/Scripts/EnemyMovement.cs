using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = new Vector2(moveSpeed, 0f);

        //myRigidbody.MovePosition(myRigidbody.position + movement * Time.deltaTime);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

    }
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.linearVelocity.x)), 1f);
    }

    /*void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }*/
    void OnTriggerEnter2D(Collider2D whatIHit)
    {
        /*if (whatIHit.CompareTag("Player"))
        {
            
            GameManager gameManager = whatIHit.GetComponent<GameManager>();

            GameManager.Instance.ProcessPlayerDeath();
        }*/
        bool Ground = GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));
        //&& (whatIHit.GetType() == typeof(BoxCollider2D
        if (Ground)
        {

            moveSpeed = -moveSpeed;
            Debug.Log("Current moveSpeed: " + moveSpeed);
            FlipEnemyFacing();


        }
        
        bool enemyTouchedPlayer = gameObject.GetComponent<CapsuleCollider2D>().IsTouching(whatIHit);
    }


    /*private void OnCollisionEnter2D(Collider2D collision)
    {
        PlayerMovementScript player = Collider2D.GetComponent<PlayerMovementScript>();
        GameManager gameSession = Object.FindFirstObjectByType<GameManager>();
        Debug.Log("Enemy is ok");
        if (player != null)
        {
            Debug.Log("Enemy is yes");
            if (player.transform.position.y > transform.position.y + GetComponent<Collider2D>().bounds.size.y / 2)
            {
                Debug.Log("Enemy destroyed");
                Destroy(gameObject);
            }
            else
            {
                gameSession.ProcessPlayerDeath();
            }
        }
    }*/
}
