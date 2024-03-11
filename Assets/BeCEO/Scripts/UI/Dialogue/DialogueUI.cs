using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeCEO.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace BeCEO.UI
{
    public class DialogueUI : MonoBehaviour
    {
        private PlayerConversant playerConversant;
        [SerializeField] private TMP_Text textNPC;

        [SerializeField] private Button nextButton;
        [SerializeField] private GameObject NPCReponse;

        [SerializeField] private Transform choiceRoot;
        [SerializeField] private GameObject choicePrefab;

        [SerializeField] private Button quitButton;

        [SerializeField] private TMP_Text conversantName;
        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            // підписуємо на івент, шоб обновити UI, коли запускається новий діалог в PlayerConversant
            playerConversant.onConversationUpdated += UpdateUI;

            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.Quit());

            UpdateUI();
        }

        // Update is called once per frame
        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());

            if (!playerConversant.IsActive())
            {
                return;
            }

            conversantName.text = playerConversant.GetCurrentConversantName();

            // Взнаємо що йде наступне в діалогові: чи вибір відповіді персонажем, чи розмова NPC
            NPCReponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            if (playerConversant.IsChoosing())
            {
                // Виведення відповідей в розмові
                BuildChoiceList();
                
            }
            else
            {
                textNPC.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            choiceRoot.DetachChildren();

            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TMP_Text>();
                textComp.text = choice.GetText();
                // також треба добавити слухача (метод) на кожну кнопку, щоб контролювати діалогове дерево показу
                Button button = choiceInstance.GetComponentInChildren<Button>();
                // шоб передати індекст, використаємо лябмда-функцію
                // І КОЖНУ ІТЕРАЦІЮ ЦИКЛУ БУДЕ СТВОРЮВАТИСЯ РІЗНІ КНОПКИ З РІЗНИМИ ЛЯМБДА-ФУНКЦ
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                });
            }
        }
    }
}
