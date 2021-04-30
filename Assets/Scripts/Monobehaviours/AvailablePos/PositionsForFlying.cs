using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsForFlying : MonoBehaviour,IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsNewFlying();//refer to interface to access the behavior we need 
    //individualizes the search for positions for a flying regiments
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex,checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            hex.isNeighboringHex = true;//defines the hex as adjacent to evaluted initial hex
            hex.distanceText.SetDistanceForFlyingUnit(initialHex);
            hex.MakeMeAvailable();
        }
    }
}
