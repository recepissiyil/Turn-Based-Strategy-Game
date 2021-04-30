using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfItIsTarget : MonoBehaviour, IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        //check whether the hero is on the hex or not and
        //whether this object contains the Enemy component
        if (BattleController.currentAtacker.GetComponent<Enemy>() == null)
        {
            return evaluatedHex.GetComponentInChildren<Enemy>() != null;
        }
        else
        {
            return evaluatedHex.GetComponentInChildren<Hero>() != null &&
            evaluatedHex.GetComponentInChildren<Enemy>() == null;
        }
    }

}
