using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfItIsTargetRange : IEvaluateHex
{
    public bool EvaluateHex(BattleHex evaluatedHex)
    {
        return evaluatedHex.battleHexState
                   == HexState.active//exclude inactive hexes 
                   && !evaluatedHex.isStartingHex//exclude starting hex 
                   && !evaluatedHex.lookingForTarget;//exclude previously checked hex
        
    }


}
