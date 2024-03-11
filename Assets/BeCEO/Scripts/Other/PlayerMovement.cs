// Code written by tutmo (youtube.com/tutmo)
// For help, check out the tutorial - https://youtu.be/PNWK5o9l54w

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ~~ 1. Controls All Player Movement
    // ~~ 2. Updates Animator to Play Idle & Walking Animations

    private float speed = 0.4f;
    private Rigidbody2D myRigidbody;
    private Vector3 playerMovement;
    private Animator animator;

    private Animator animatorDoor;
    private BoxCollider2D boxColliderDoor;

    // відкрити в двері
    private bool isNotDoorZone = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerMovement = Vector3.zero;
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        playerMovement.Normalize();

        UpdateAnimationAndMove();
    }

    private void UpdateAnimationAndMove()
    {
        if (playerMovement != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", playerMovement.x);
            animator.SetFloat("moveY", playerMovement.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + playerMovement * speed * Time.deltaTime);
    }


    // trigger enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Door"))
        {
            Debug.Log("EnterToTheRoom");

            // перевіримо чи це вихідні двері, якими можна перейти на іншу сцену
            var door = collision.GetComponent<Door>();

            if (door.isEntranceDoor)
            {
                door.ShowSplashScreen();
            }

            animatorDoor = collision.GetComponent<Animator>();
            animatorDoor.Play("Door1Open");

            if (false)
            {
                Debug.Log("EnterToTheRoom");

                animatorDoor = collision.GetComponent<Animator>();
                boxColliderDoor = collision.gameObject.GetComponent<BoxCollider2D>();

                animatorDoor.Play("Door1Open");
                boxColliderDoor.enabled = false;

                // поставити flag
                isNotDoorZone = false;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag.Equals("Door"))
        {
            

            Debug.Log("ExitFromTheRoom");

            animatorDoor = collision.GetComponent<Animator>();
            animatorDoor.Play("Door1Close");
            /*if (isInDoorZone)
            {
                Debug.Log("ExitFromTheRoom");

                animatorDoor = collision.GetComponent<Animator>();
                boxColliderDoor = collision.gameObject.GetComponent<BoxCollider2D>();

                animatorDoor.Play("Door1Close");
                boxColliderDoor.enabled = true;

                // поставити flag
                isInDoorZone = false;
            }*/

            

            if (false)
            {
                Debug.Log("ExitFromTheRoom");

                animatorDoor = collision.GetComponent<Animator>();
                boxColliderDoor = collision.gameObject.GetComponent<BoxCollider2D>();

                animatorDoor.Play("Door1Close");
                boxColliderDoor.enabled = true;

                // поставити flag
                isNotDoorZone = true;
            }
            
        }        
    }
}
