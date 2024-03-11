using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeCEO.UI.Tooltips;
using UnityEditor.SceneManagement;
using BeCEO.Quests;


namespace BeCEO.UI.Quests
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
            tooltip.GetComponent<QuestTooltipUI>().Setup(status);
        }
    }

}
