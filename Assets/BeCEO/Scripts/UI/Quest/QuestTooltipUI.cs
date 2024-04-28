using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeCEO.Quests;
using TMPro;

namespace BeCEO.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Transform objectiveContainer;
        [SerializeField] private GameObject objectivePrefab;
        [SerializeField] private GameObject objectiveIncompletePrefab;
        [SerializeField] private TextMeshProUGUI rewardText;

        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();
            //objectiveContainer.DetachChildren();
            foreach (Transform item in objectiveContainer)
            {
                Destroy(item.gameObject);
            }

            foreach (var objective in quest.GetObjectives())
            {
                // взнаємо чи виконане завдання
                GameObject prefab = objectiveIncompletePrefab;

                if (status.IsObjectiveComplete(objective.reference))
                {
                    prefab = objectivePrefab;
                }

                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective.description;
            }
            rewardText.text = GetRewardText(quest);
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach (var reward in quest.GetRewards())
            {
                if (rewardText != "")
                {
                    rewardText += ", ";
                }
                if (reward.number > 1)
                {
                    rewardText += reward.number + " ";
                }
                rewardText += reward.item;
            }
            if (rewardText == "")
            {
                rewardText = "No reward";
            }
            rewardText += ".";
            return rewardText;
        }
    }

}
