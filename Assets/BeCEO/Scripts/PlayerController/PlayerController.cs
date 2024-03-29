using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50f;
    private Vector2 movement;
    private string currentAnimation = "idle";
    private int currentAnimationState = 0;

    // get references to own components
    private Rigidbody2D rb;
    private Animator animator;

    

    [SerializeField] private GameObject panelInfo;
    [SerializeField] private TextMeshProUGUI textPanelInfo;


    // references to another scripts
    #region Door
    private Animator doorAnimator;
    private BoxCollider2D[] doorColliders;
    private Door doorScript;
    // bool
    private bool isInDoorTriggerZone = false;
    private bool isOpenDoor = false;
    #endregion
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // animation state
        currentAnimationState = 3;
    }
    private void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // GetKeyDown
        if (Input.GetKeyDown(KeyCode.Space) && isInDoorTriggerZone)
        {
            if (isOpenDoor)
            {
                doorScript.isState = false;
                doorAnimator.SetTrigger("DoorClose");
                doorColliders[1].enabled = true;
                isOpenDoor = false;
            }
            else
            {
                doorScript.isState = true;
                doorAnimator.SetTrigger("DoorOpen");
                doorColliders[1].enabled = false;
                isOpenDoor = true;
            }


        }

        movement.Normalize();
    }


    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        SetAnimationState();
        PlayerAnimation();
    }


    private void PlayerAnimation()
    {
        if (movement == Vector2.zero) 
        {
            currentAnimation = "idle";
        }
        else
        {
            currentAnimation = "run";
        }
        animator.Play(currentAnimation + currentAnimationState.ToString());
    }

    private void SetAnimationState()
    {
        if (movement.x > 0)
        {
            currentAnimationState = 0;
        }
        else if (movement.x < 0)
        {
            currentAnimationState = 2;
        }
        else if (movement.y < 0)
        {
            currentAnimationState = 3;
        }
        else if (movement.y > 0)
        {
            currentAnimationState = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            // set flag that we are in the triggerZone
            isInDoorTriggerZone = true;
            // show info panel
            panelInfo.SetActive(true);
            textPanelInfo.text = "[SPACE] to open door";
            // get ref
            doorAnimator = collision.GetComponentInParent<Animator>();
            doorColliders = collision.GetComponentsInParent<BoxCollider2D>();
            doorScript = collision.GetComponentInParent<Door>();
            // get state of door
            isOpenDoor = doorScript.isState;


            Debug.Log("State door: " + isOpenDoor);
            Debug.Log("IsTriggerZone: " + isInDoorTriggerZone);






        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            panelInfo.SetActive(false);
            // set flag that we are not in the triggerZone
            isInDoorTriggerZone = false;
        }
    }
}
