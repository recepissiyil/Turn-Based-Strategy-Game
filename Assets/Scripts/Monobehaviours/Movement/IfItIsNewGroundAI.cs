using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfItIsNewGroundAI : MonoBehaviour, IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        return evaluatedHex.battleHexState
                    == HexState.active//Excludes inactive hexes
                    && !evaluatedHex.isStartingHex//Excludes the starting position
                    && !evaluatedHex.isIncluded//excludes hexes that are already marked as valid
                    && evaluatedHex.AvailableToGround()//excludes water and mountains
                    && ifThereIsAI(evaluatedHex);//excludes hacks occupied by player's regiment

    }
    private bool ifThereIsAI(BattleHex evaluatedHex)
    {
        bool AIPosfalse = true;
        //if the hex is occupied by an object of type Hero, but without an Enemy component
        if (evaluatedHex.GetComponentInChildren<Hero>() != null &&
            evaluatedHex.GetComponentInChildren<Enemy>() == null)
        {
            AIPosfalse = false;//then we allow the troll to go through this hex
        }
        return AIPosfalse;
    }
}
