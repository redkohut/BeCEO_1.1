// Code written by tutmo (youtube.com/tutmo)
// For help, check out the tutorial - https://youtu.be/PNWK5o9l54w

using UnityEngine;
using UnityEngine.SceneManagement;

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

    

    [Header("Pointer")]
    public GameObject pointer;
    public RectTransform arrow;

    private string nameCurrentScene;
    private Transform targetPosition;

    private Camera mainCamera;
    private float distanceCanvas;

    private void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }


        // взнаємо яка зараз сцена і виставимо значення напрямку
        string nameCurrentScene = SceneManager.GetActiveScene().name;

        if (nameCurrentScene == "Home")
            targetPosition = GameObject.Find("doorExit").transform;
        else if (nameCurrentScene == "City")
            targetPosition = GameObject.Find("PointerEnterToMetro").transform;
        else if (nameCurrentScene == "Metro")
            targetPosition = GameObject.Find("ExitZoneFromMetro").transform;
        else if (nameCurrentScene == "SchoolStation")
            targetPosition = GameObject.Find("DoorEnterToSchool").transform;
        else if (nameCurrentScene == "School")
            targetPosition = GameObject.Find("PlayersComputerZone").transform;
        else
        {
            targetPosition = null;
        }

        if (targetPosition != null)
        {
            arrow = GameObject.Find("ArrowDirection").GetComponent<RectTransform>();
            pointer.SetActive(true);
        }
        else pointer.SetActive(false);
    }

    private void FixedUpdate()
    {
        playerMovement = Vector3.zero;
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");

        playerMovement.Normalize();

        UpdateAnimationAndMove();

        if (targetPosition != null)
        {
            Vector3 fromPosition = Camera.main.transform.position;

            fromPosition.z = 0f;
            Vector3 dir = (targetPosition.position - fromPosition).normalized;

            float angle = GetAngleFromVectorFloat(dir);

            arrow.localEulerAngles = new Vector3(0, 0, angle); 

        }
    }
    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        // Використовуємо арктангенс, щоб отримати кут у радіанах
        float angleRad = Mathf.Atan2(dir.y, dir.x);

        // Конвертуємо радіани в градуси
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // Коригуємо від'ємні кути
        if (angleDeg < 0)
        {
            angleDeg += 360;
        }

        // Повертаємо кут
        return angleDeg;
    }
    public Vector3 UpdatePointerArrow(Transform targetTransform)
    {
        // Отримуємо напрямок від поточного об'єкту до цільового
        Vector3 direction = targetTransform.position - transform.position;

        // Нормалізуємо напрямок, щоб отримати вектор одиничної довжини
        direction.Normalize();

        return direction;
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

        if (collision.tag == "MetroEnter")
        {
            Debug.Log("EnterToTheSubway");
            Debug.Log(collision.name);
            Metro metro = collision.GetComponent<Metro>();
            metro.ShowSplashScreen();
        }
        // enter to wagon
        if (collision.tag == "EnterWagon")
        {
            Debug.Log("EnterToTheWagon");
            // need to generate randomly
            WagonMec metroInside = GameObject.Find("SUBWAY").GetComponent<WagonMec>();
            //WagonMec metroInside = collision.GetComponent<WagonMec>();
            metroInside.StartRiding();
        }
        // exit from subway
        if (collision.tag == "MetroExit")
        {
            Debug.Log("EnterToTheCity");
            // need to generate randomly
            WagonMec metroInside = GameObject.Find("SUBWAY").GetComponent<WagonMec>();
            //WagonMec metroInside = collision.GetComponent<WagonMec>();
            metroInside.ShowSplashScreen();
        }

        if (collision.tag == "SchoolEnter")
        {
            Debug.Log("EnterToTheSchool");

            SchoolEnter schoolEnter = collision.GetComponent<SchoolEnter>();
            schoolEnter.ShowSplashScreen();
        }

        if (collision.tag == "Computer")
        {
            SceneLoader.LoadScene("Computer");
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
