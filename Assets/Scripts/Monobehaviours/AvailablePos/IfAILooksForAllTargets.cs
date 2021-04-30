using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfAILooksForAllTargets : MonoBehaviour, IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        return evaluatedHex.battleHexState
                    == HexState.active//Excludes inactive hexes
                    && !evaluatedHex.isStartingHex//Excludes the starting position
                    && evaluatedHex.AvailableToGround();//excludes water and mountains
    }

}
