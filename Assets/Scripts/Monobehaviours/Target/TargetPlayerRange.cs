using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerRange : MonoBehaviour, IDefineTarget
{
    BattleHex initialHex;//Hex whose neighbors we are looking for
    List<BattleHex> neighboursToCheck;//collects neighbouring hexes that match the search criteria
    IEvaluateHex checkHex = new IfItIsTarget();//refers to the interface to access the behavior we need 
    IInitialHexes getInitialHexes = new InitialTarget();
    Turn turn;
    public void DefineTargets(Hero currentAtacker)
    {
        if (TargetsNearby(currentAtacker) == false)//check if there is an enemy nearby
        {
            TargetsAtAttackDistance(currentAtacker);
        }
    }
    //if the enemy is nearby, then mark it and stop looking for distant eneimes
    bool TargetsNearby(Hero currentAtacker)
    {
        bool targetNearby = false;//the variable lets you know if there is an enemy nearby
        initialHex = currentAtacker.GetComponentInParent<BattleHex>();//starting hex

        //collect neighboring hexes
        neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        if (neighboursToCheck.Count > 0)//check if the list is not empty
        {
            foreach (BattleHex hex in neighboursToCheck)
            {
                hex.DefineMeAsPotencialTarget();//mark potential target
                //hex.lookingForTarget = true;
            }
            targetNearby = true;
        }
        return targetNearby;
    }
    //if there are no enemies nearby, then continue searching
    void TargetsAtAttackDistance(Hero currentAtacker)
    {
        int stepsLimit = currentAtacker.heroData.Atackdistanse;//number of search levels
        BattleHex inititalHex = currentAtacker.GetComponentInParent<BattleHex>();//starting hex
        IAdjacentFinder adjFinder = new MarkTargets();//rule for checking the hex
        //check the entire attack area
        currentAtacker.GetComponent<AvailablePos>().GetAvailablePositions(stepsLimit, adjFinder, getInitialHexes);
        CheckIfItIsNewTurn();
    }
    private void CheckIfItIsNewTurn()
    {
        BattleController battleController = FindObjectOfType<BattleController>();
        if (battleController.IsLookingForPotentialTargets().Count == 0
            && BattleController.currentAtacker.heroData.CurrentVelocity == 0)
        {
            turn = FindObjectOfType<Turn>();
            turn.TurnIsCompleted();
        }
    }
}
