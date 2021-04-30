using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPosForGroundAI : MonoBehaviour
{
    private int step;//counts iterations
    List<BattleHex> initialHexes = new List<BattleHex>();//collects neighbouring hexes for evaluated hex
    IEvaluateHex checkHex = new IfAILooksForAllTargets();//refer to interface to access the behavior we need 

    //looks for all positions available
    public void GetAvailablePositions(int stepsLimit, IInitialHexes getHexesToCheck, BattleHex startingHex)
    {
        GetAdjacentHexesExtended(stepsLimit, startingHex);//looks for hexes adjacent to starting hex. Flying unit for now
        //runs iterations to find all positions available. steps=number of iterations
        for (step = 2; step <= stepsLimit; step++)
        {
            initialHexes = getHexesToCheck.GetNewInitialHexes();//collects hexes ready for a new iteration
            foreach (BattleHex hex in initialHexes)
            {
                    GetAdjacentHexesExtended(stepsLimit, hex);// defines neighbouring hexes for each hex in the collection
            }
        }
    }
    public void GetAdjacentHexesExtended(int stepsLimit, BattleHex initialHex)
    {
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        foreach (BattleHex hex in neighboursToCheck)
        {
            //Compares the current value of the distanceFromStarting point variable with a new value
            if (hex.distanceText.EvaluateDistanceForGroundAI(initialHex, stepsLimit))
            {
                hex.isNeighboringHex = true;
                hex.distanceText.SetDistanceForGroundUnit(initialHex);//sets the distance from the starting hex
            }
        }
    }

}
