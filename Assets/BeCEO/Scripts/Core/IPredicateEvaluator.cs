using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeCEO.Core
{
    public interface IPredicateEvaluator 
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}
