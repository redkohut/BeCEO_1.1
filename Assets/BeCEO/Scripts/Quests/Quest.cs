using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BeCEO.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "BeCEO/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private List<string> objectives = new List<string>();


        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objectives;
        }

        public bool HasObjective(string objective)
        {
            return objectives.Contains(objective);
        }
    }

}
