using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleController : MonoBehaviour
{
    public static BattleHex targetToMove;
    public static Hero currentAtacker;
    public static Hero currentTarget;//Stores information about who is being attacked
    List<Hero> allFighters = new List<Hero>(); //collects all fighters placed on the battlefield
    public int stepsToCheckWholeField;//number of iterations to check the entire battlefield
    public List<BattleHex> potentialTargets = new List<BattleHex>();//collects all player's regiments
    Turn turn;
    public EventSystem events;//to disable click response
    private void Start()
    {
        turn = GetComponent<Turn>();
        events = FindObjectOfType<EventSystem>();
    }

    //collects all fighters placed on the battlefield
    public List<Hero> DefineAllFighters()
    {
        allFighters = FindObjectsOfType<Hero>().ToList();
        return allFighters;
    }
    public void DefineNewAtacker()
    {
        //   sorts fighters by initiative value, in descending order
        List<Hero> allFighters = DefineAllFighters().
                                 OrderByDescending(hero => hero.heroData.InitiativeCurrent).ToList();
        //  the first element of the list has the biggest initiative value
        currentAtacker = allFighters[0];
        currentAtacker.heroData.InitiativeCurrent = 0;
    }
    public void CleanField()
    {
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            hex.SetDefaultValue();
        }
    }
    public void RemoveHeroWhenItIsKilled(Hero hero)
    {
        print("killed");
        Destroy(hero.gameObject);
        turn.TurnIsCompleted();
    }
   public List<BattleHex> IsLookingForPotentialTargets()//collects all player's regiments into a list
    {
        potentialTargets.Clear();
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            //checks if the hex is marked as occupied by a player’s regiment
            if (hex.potencialTarget)
            {
                potentialTargets.Add(hex);
            }
        }
        return potentialTargets;
    }

}
