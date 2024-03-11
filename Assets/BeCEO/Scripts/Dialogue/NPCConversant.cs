using BeCEO.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BeCEO.Dialogue
{
    public class NPCConversant : MonoBehaviour
    {
        [SerializeField] private string NPCName;

        [SerializeField] private Dialogue dialogue = null;
        // Start is called before the first frame updateç â î

        public string GetNPCName()
        {
            return NPCName;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("PlayerDialogueZone"))
            {
                var playerConversant = collision.GetComponentInParent<PlayerConversant>(); ;
                playerConversant.StartDialogue(this, dialogue);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag.Equals("PlayerDialogueZone"))
            {
                var playerConversant = collision.GetComponentInParent<PlayerConversant>();
                playerConversant.Quit();
            }
        }
    }
}

