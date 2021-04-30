using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    BattleController battleController;//BattleController class reference
    AllPosForGroundAI tocheckTheField;//AllPosForGroundAI component reference
  
    public List<BattleHex> PosToOccupy = new List<BattleHex>();//collects all the hexes that a troll can occupy
    List<BattleHex> allTargets = new List<BattleHex>();//the variable is used to sort player's regiments
    List<BattleHex> closeTargets = new List<BattleHex>();//collects player regiments located in the attack zone
    BattleHex hexToOccupy;//Hex chosen by the troll to occupy it
    AvailablePos availablePos;//component access
    Move move;//component access
    Hero hero;//component access
    private void Start()
    {
        battleController = FindObjectOfType<BattleController>();
        tocheckTheField = GetComponent<AllPosForGroundAI>();
        hero = GetComponent<Hero>();
        availablePos = GetComponent<AvailablePos>();
        move = GetComponent<Move>();
        move.lookingToTheRight = false;//to make the troll look in the right direction
    }
    public void Aisturnbegins(IInitialHexes getInitialHexes)
    {
        int stepsLimit = battleController.stepsToCheckWholeField;//gets current velocity of the atacker
        BattleHex startintHex = GetComponentInParent<BattleHex>();

        //starts looking for all player's units
        tocheckTheField.GetAvailablePositions(stepsLimit, getInitialHexes, startintHex);
        CollectAllPosToOccupy();
        AIMakesDecision();
    }
    List<BattleHex> CollectAllPosToOccupy()
    {
        PosToOccupy.Clear();
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            if (hex.distanceText.distanceFromStartingPoint <= hero.heroData.CurrentVelocity)
            {
                PosToOccupy.Add(hex);
            }
        }
        return PosToOccupy;
    }


    private List<BattleHex> CheckIfAttackIsAvailable()//checks if the player’s regiment is in the attack zone
    {
        int currentVelocity = BattleController.currentAtacker.heroData.CurrentVelocity;
        closeTargets.Clear();
        //assigns all player units to a list
        List<BattleHex> allTargets =battleController. IsLookingForPotentialTargets();
        foreach (BattleHex hex in allTargets)
        {
            //checks if the hex is in the attack zone
            //melee fighter can attack after moving, attack distance is 1 hex
            if (hex.distanceText.distanceFromStartingPoint <= currentVelocity + 1)
            {
                closeTargets.Add(hex);
            }
        }
        return closeTargets;
    }
    public BattleHex AISelectsTargetToAttack()//selects a priority target for attack
    {
        allTargets.Clear();//clear the list before filling
        if (CheckIfAttackIsAvailable().Count > 0)
        {
            //sort all player's regiments by health in ascending order
            allTargets = CheckIfAttackIsAvailable().
                         OrderBy(hero => hero.GetComponentInChildren<Hero>().heroData.HPCurrent).ToList();
        }
        else
        {
            //sort all player's regiments first by the distance to the target, then by HP property
            allTargets =battleController. IsLookingForPotentialTargets().OrderBy(hero => hero.distanceText.distanceFromStartingPoint).
                        ThenBy(hero => hero.GetComponentInChildren<Hero>().heroData.HPCurrent).ToList();
        }
        BattleController.currentTarget = allTargets[0].GetComponentInChildren<Hero>();
        return allTargets[0];
    }
    void AIIStartsMoving(BattleHex targetToAttack)//determines the distance from the attack target to each hex
    {
        battleController.CleanField();//clear properties of all hexes before new calculation
        targetToAttack.DefineMeAsStartingHex();//the starting hex is the hex occupied by the target of the attack
        int stepsLimit = battleController.stepsToCheckWholeField;//number of steps enough to check the entire battlefield
        IInitialHexes getInitialHexes = new InitialPos();//each hex validation rule

        //determines the distance from the attack target to each hex
        tocheckTheField.GetAvailablePositions(stepsLimit, getInitialHexes, targetToAttack);
        IAdjacentFinder adjFinder = BattleController.currentAtacker.GetTypeOfHero();//determines the type of movement
        AIDefinesPath(adjFinder);//determines the optimal path and starts moving
    }
    private BattleHex AISelectsPosToOcuppy()//defines the end point of movement
    {
        //sorts all the positions that the troll can occupy by the distance to attack target
        List<BattleHex> OrderedPos = PosToOccupy.OrderBy(s => s.distanceText.distanceFromStartingPoint).ToList();
        for (int i = 0; i < OrderedPos.Count; i++)
        {

            if (OrderedPos[i].GetComponentInChildren<Hero>() == null)
            {
                hexToOccupy = OrderedPos[i];
                //terminates execution of the for loop as soon as the hex closest to the target of the attack is determined
                break;
            }
        }

        return hexToOccupy;
    }
    void AIMakesDecision()//the computer chooses whether it should attack or move
    {
        BattleHex targetToAttack = AISelectsTargetToAttack();//assigns a priority target for attack
        //checks if the attack target is on one of the neighboring hexes
        if (targetToAttack.distanceText.distanceFromStartingPoint > 1)//if it is not...
        {
            AIIStartsMoving(targetToAttack);//the AI starts moving
        }
        else
        {
            hero.HeroIsAtacking();//attacks immediately
        }
    }
    void AIDefinesPath(IAdjacentFinder adjFinder)//determines the optimal path and starts moving
    {
        //assigns a value to the static variable used by the Move and Optimal Path classes
        BattleController.targetToMove = AISelectsPosToOcuppy();
        battleController.CleanField();// clear the entire battlefield from previous calculations
        IInitialHexes getInitialHexes = new InitialPos();//each hex validation rule
        int stepsLimit = hero.heroData.CurrentVelocity;
        BattleHex startingHex = BattleController.currentAtacker.GetComponentInParent<BattleHex>();
        startingHex.DefineMeAsStartingHex();//marks a new starting hex

        //select all the hexes that the troll can occupy
        availablePos.GetAvailablePositions(stepsLimit, adjFinder, getInitialHexes);
        GetComponent<OptimalPath>().MatchPath();//determine the optimal path
        move.StartsMoving();//troll begins to move
    }
}
