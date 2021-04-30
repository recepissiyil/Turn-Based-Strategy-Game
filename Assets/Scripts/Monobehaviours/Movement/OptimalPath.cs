using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptimalPath : MonoBehaviour
{
    public static List<BattleHex> optimalPath = new List<BattleHex>();//collects hexes in list
    public static BattleHex nextStep;//hex included in optimal path list
    public List<Image> landscapes = new List<Image>();//collects images of hexes included in optimal path
    BattleHex targetHex;//target position, clicked hex
    IAdjacentFinder AdjacentOption = new PosForPath();
    Move move;
    private void Start()
    {
        move = GetComponent<Move>();
    }
    //collects hexes in optimal path list and highlights them
    internal void MatchPath()
    {
        optimalPath.Clear();//clears the list before re-filling
        targetHex = BattleController.targetToMove;//first hex included in optimal path
        optimalPath.Add(targetHex);

        //defines the distance from target hex
            int steps = targetHex.distanceText.distanceFromStartingPoint;
        for (int i = steps; i > 1;)// iterates to find out all hexes to be included in optimal path
            {
            AdjacentOption.GetAdjacentHexesExtended(targetHex);//finds out hexes adjacent to targethex
            targetHex = nextStep;// when the hex is included in list it becomes a new target hex
            i -= nextStep.distanceText.MakeMePartOfOptimalPath();// decreases the i variably by stepsToGo value
            }
        ManagePath();
    }
    void ManagePath()//reverses the optimal path, fills the path with images of the hexes
    {
        landscapes.Clear();//clears the list before re-filling
        optimalPath.Reverse();//sorts list elements in the opposite order
        foreach (BattleHex hex in optimalPath)
        {
            landscapes.Add(hex.Landscape);//fills the list with images
        }
        move.path = landscapes;//sends information regarding the optimal path to the Move class
    }
}
