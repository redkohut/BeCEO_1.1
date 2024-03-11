using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeCEO.Quests;
using TMPro;

namespace BeCEO.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text progress;

        private QuestStatus status;
        public void Setup(QuestStatus status)
        {
            /***
             * Коли заповняємо список всіх квестів
             * то потрібно заповнити в кожному item поля інформації та прогресу
             * і ця інформація береться з ScriptableObject Quest
             * ***/
            this.status = status;
            title.text = status.GetQuest().GetTitle();
            progress.text = status.GetCompletedCount() + "/" + status.GetQuest().GetObjectiveCount();
        }

        public QuestStatus GetQuestStatus()
        {
            return status;
        }
    }

}
