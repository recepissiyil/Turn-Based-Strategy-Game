using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public int distanceFromStartingPoint;//counts distance from starting hex
    public int stepsToGo;//determines the number of steps to go through the hex
    public int defaultDistance;//Default value of the distanceFromStartingPoint variable
    public int defaultstepsToGo;//Default value of the stepsToGo variable
    BattleHex hex;
    Text distanceText; //refers to the text component of the same object

    private void Start()
    {
        hex = GetComponentInParent<BattleHex>();
        distanceText = GetComponent<Text>();
    }
    //sets distance from starting hex and displays it
    public void SetDistanceForGroundUnit(BattleHex initialHex)
    {
        //add a step to the previous step to get diastance from starting point
        distanceFromStartingPoint = initialHex.distanceText.distanceFromStartingPoint
                    + initialHex.distanceText.stepsToGo;
        //display new value of the distanceFromStartingPoint
        //DisplayDistanceText();
    }
    public void SetDistanceForFlyingUnit(BattleHex initialHex)
    {
        stepsToGo = 1;
        //add a step to the previous step to get diastance from starting point
        distanceFromStartingPoint = initialHex.distanceText.distanceFromStartingPoint + stepsToGo;
        //display new value of the distanceFromStartingPoint
        //DisplayDistanceText();

    }
    private void DisplayDistanceText()
    {
        distanceText.text = distanceFromStartingPoint.ToString();
        distanceText.color = new Color32(255, 255, 255, 255);
    }


    public bool EvaluateDistance(BattleHex initialHex)//compares distances between two hexes
    {
        return distanceFromStartingPoint + stepsToGo ==
                initialHex.distanceText.distanceFromStartingPoint;
    }
    public int MakeMePartOfOptimalPath()//includes this hex into optimal path list, 
    //returns number of steps to go through the hex
    {
        OptimalPath.optimalPath.Add(hex);
        hex.Landscape.color = new Color32(150, 150, 150, 255);
        return stepsToGo;
    }
    public bool EvaluateDistanceForGround(BattleHex initialHex)
    {
        //distance to reach initial hex and get out of it
        int currentDistance = initialHex.distanceText.distanceFromStartingPoint
                              + initialHex.distanceText.stepsToGo;
        int stepsLimit = BattleController.currentAtacker.heroData.CurrentVelocity;//velocity of a hero
        //default value of distanceFromStartingPoint is 20 to set the shortest path
        return distanceFromStartingPoint > currentDistance &&
                stepsLimit >= currentDistance;//to evaluate if the velocity is enough to reach this hex
    }
    public bool EvaluateDistanceForGroundAI(BattleHex initialHex, int stepsLimit)
    {
        //distance to reach initial hex and get out of it
        int currentDistance = initialHex.distanceText.distanceFromStartingPoint
                              + initialHex.distanceText.stepsToGo;
        //default value of distanceFromStartingPoint is 20 to set the shortest path
        return distanceFromStartingPoint > currentDistance &&
                stepsLimit >= currentDistance;//to evaluate if the velocity is enough to reach this hex
    }

}
