using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTargets : IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsTargetRange();// to access the behavior we need 
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        //collect hexes that meet the criteria defined by the IfItIsTargetRange rule
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);

        foreach (BattleHex hex in neighboursToCheck)
        {
            hex.lookingForTarget = true;//defines the hex as adjacent to evaluted  hex
            if (hex.GetComponentInChildren<Enemy>() != null)
            {
                hex.DefineMeAsPotencialTarget();//marks hex as a potential target
            }
        }
    }
}
