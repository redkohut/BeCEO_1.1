using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using BeCEO.Core;


namespace BeCEO.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] string playerName;
        // посилання
        [SerializeField] private Dialogue testDialogue;
        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        // щоб спрацьовував трігер подій
        NPCConversant currentConversant;

        public event Action onConversationUpdated;

        


/*        IEnumerator Start()
        {
            yield return new WaitForSeconds(5);
            StartDialogue(testDialogue);
        }*/
        public void StartDialogue(NPCConversant newConversant, Dialogue newDialogue)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;

            currentNode = currentDialogue.GetRootNode();

            // добавимо трігери подій
            TriggerEnterAction();

            onConversationUpdated(); // ЗАПУСК ТРІГЕРА НА ПОДІЮ ОБНОВЛЕННЯ ДІАЛОГУ
        }

        public void Quit()
        {
            currentDialogue = null;
            
            TriggerExitAction();

            currentConversant = null;

            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }
        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public string GetCurrentConversantName()
        {
            if (isChoosing)
            {
                return playerName;
            }
            else
            {
                return currentConversant.GetNPCName();
            }
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode));

        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            
            TriggerEnterAction();

            isChoosing = false;
            Next();
        }
        public void Next()
        {
            // взнаємо наступний ChildNode в системі діалогу (чи то відповдіь, чи то розповідь)
            // і ставимо чекпоінт
            int countOfPlayerResposes = FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).Count();
            if (countOfPlayerResposes > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }
            // від себе добавлю, шоб можна було вкінці відповідь давати
    /*        if (countOfPlayerResposes == 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }*/
            DialogueNode[] children = FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).ToArray();

            int randomIndex = UnityEngine.Random.Range(0, children.Count());

            TriggerExitAction();

            currentNode = children[randomIndex];

            TriggerEnterAction();

            onConversationUpdated();
        }

        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }
        private void TriggerEnterAction()
        {
            if  (currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "")
            {
                return;
            }

            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}
