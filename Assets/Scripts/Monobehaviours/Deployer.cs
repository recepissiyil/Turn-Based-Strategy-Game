using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deployer : MonoBehaviour
{
    public static CharIcon readyForDeploymentIcon;//stores information about the icon that the player clicked on
    List<BattleHex> enemiesPositions = new List<BattleHex>();//stores hexes to deploy enemies
    List<CharAttributes> enemiesToDeploy = new List<CharAttributes>();// stores enemies' scriptable objects
    static StorageMNG storage;
    int enemiesNum;// to count the number of enemy objects 

    void Start()
    {
        ActivatePositionsForRegiments();
        storage = FindObjectOfType<StorageMNG>();
        enemiesToDeploy = storage.currentProgress.enemies;//fill the list with enemies' scriptable objects
        enemiesNum = enemiesToDeploy.Count();//count the number of enemy units
        PlaceEnemies();//places enemy units on the battlefield
    }
    //DeployRegiment method instantiates the hero on the battlefield
    public static void DeployRegiment(BattleHex parentObject)//hero appears on parentObject
    {
        Hero regiment = readyForDeploymentIcon.charAttributes.heroSO;// gets the hero prefab
        Hero fighter = Instantiate(regiment, parentObject.Landscape.transform);//instantiates the hero and
        fighter.GetComponent<Move>().ManageSortingLayer(parentObject);
        //returns a hero object
        parentObject.CleanUpDeploymentPosition();//hides the checkmark and disables the collider
        readyForDeploymentIcon.HeroIsDeployed();//marks the icon in gray
        readyForDeploymentIcon = null;//clears a variable to prevent the hero from reappearing
        storage.GetComponent<StartBTN>().ControlStartBTN();//enables the start button
    }
    void ActivatePositionsForRegiments() // displays the checkmark and enables the collider
    {
        foreach (BattleHex hex in FieldManager.allHexesArray)
        {
            if (hex.deploymentPos.regimentPosition == PositionForRegiment.player)//if the variable of a hex is true then
                                                      //a hex is defined as a possible position
            {
                hex.MakeMeDeploymentPosition();//display the checkmark and enable the colliders
            }
        }
    }
    internal List<BattleHex> GetEnemiesPos()//returns a list with hexes for enemies
    {
        enemiesPositions.Clear();//clean the list before a new iteration
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            //check if the position is intended for an enemy unit
            if (hex.deploymentPos.regimentPosition == PositionForRegiment.enemy)
            {
                enemiesPositions.Add(hex);
            }
        }
        return enemiesPositions;
    }

    private void PlaceEnemies()//places enemy units on the battlefield
    {
        List<BattleHex> enemiesPositions = GetEnemiesPos();//collects all positions for enemies
        for (int i = 0; i < enemiesNum; i++)//use loop in order to exclude occupied positions
        {       
            int positionsNum = enemiesPositions.Count();//updates the number of unoccupied positions
            int randomIndex = UnityEngine.Random.Range(0, positionsNum - 1);//returns a random number that 
            //will become an element of the list
            Image landscape = enemiesPositions[randomIndex].Landscape;//parent object to use instantiate method
            InstantiateEnemy(enemiesToDeploy[i], landscape);//instantiates an enemy
            //prohibits re-occupying the hex
            enemiesPositions.RemoveAt(randomIndex);
        }
    }
    private void InstantiateEnemy(CharAttributes charAttributes, Image hexPosition)
    {
        Hero enemy = Instantiate(charAttributes.heroSO, hexPosition.transform);//instantiates an enemy
        enemy.gameObject.AddComponent<Enemy>();//adds Enemy script to a hero object defined as an enemy

        //attaches the AllPosForGroundAI script to a hero object defined as an enemy
        enemy.gameObject.AddComponent<AllPosForGroundAI>();
    }
}
