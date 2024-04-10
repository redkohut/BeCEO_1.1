using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Start is called before the first frame update
    private enum NPCType
    {
        walkX,
        walkY
    }
    [SerializeField] private NPCType type;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    public float npcSpeed = 0.4f;

    private bool movingToEnd = true; // Flag to track direction of movement

    private bool changeState = true;

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveNPC();
    }

    public void MoveNPC()
    {// Determine the target position based on the current direction of movement
        Vector3 targetPosition = movingToEnd ? endPosition.position : startPosition.position;
        bool stateAnim = movingToEnd ? true : false;

        if (stateAnim && changeState)
        {
            changeState = false;
            anim.Play("walk1");
        }
        else if (!stateAnim && changeState)
        {
            changeState = false;
            anim.Play("walk2");
        }
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, npcSpeed * Time.deltaTime);

        // Check if the NPC has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // If reached the target position, change direction
            movingToEnd = !movingToEnd;
            changeState = true; 
        }

    }
}
