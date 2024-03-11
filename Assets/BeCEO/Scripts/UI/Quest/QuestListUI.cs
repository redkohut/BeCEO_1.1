using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeCEO.Quests; 

namespace BeCEO.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        
        [SerializeField] QuestItemUI questPrefab;
        private QuestList questList;

        // Start is called before the first frame update
        void Start()
        {
            // отримуємо список квестів від головного персонажа
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.onUpdate += Redraw;
            Redraw();
        }

        private void Redraw()
        {
            transform.DetachChildren();
           
            foreach (QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(status);
            }
        }
    }

}
