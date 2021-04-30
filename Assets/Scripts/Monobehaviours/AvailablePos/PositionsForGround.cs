using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionsForGround : MonoBehaviour, IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsNewGround();//refer to interface to access the behavior we need 
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            if (hex.distanceText.EvaluateDistanceForGround(initialHex))
            {
                hex.isNeighboringHex = true;//defines the hex as adjacent to evaluted initial hex
                hex.distanceText.SetDistanceForGroundUnit(initialHex);
                hex.MakeMeAvailable();
            }
        }
    }
}
