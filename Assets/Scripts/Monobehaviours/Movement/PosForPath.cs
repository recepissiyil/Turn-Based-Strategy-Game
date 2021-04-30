using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosForPath : MonoBehaviour, IAdjacentFinder
{
    IEvaluateHex checkHex = new IfItIsOptimalPath();//select option to find out the optimal path
    public void GetAdjacentHexesExtended(BattleHex initialHex)
    {
        //collect hexes to select new link of optimal path chain
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            //compare distances between two hexes
            if (hex.distanceText.EvaluateDistance(initialHex))
            {
                OptimalPath.nextStep = hex;//save the hex included in optimal path
                break;//since we have to stop looking for other hexes
            }
        }
    }
}
