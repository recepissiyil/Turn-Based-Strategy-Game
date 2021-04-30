using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HexState { inactive, active };
public class BattleHex : MonoBehaviour
{
    public int horizontalCoordinate;
    public int verticalCoordinate;
    public HexState battleHexState;
    public bool isSecondLevel = false;
    public ClickOnMe clickOnMe;
    public Image Landscape;
    public Distance distanceText;//access to DistanceText Object
    public DeploymentPos deploymentPos;//access to DeploymentPos Object
    [SerializeField] protected Image currentState;
    public bool isStartingHex = false;
    public bool isNeighboringHex = false;//helps define a hex as neighbouring to evaluated
    public bool isIncluded = false;//helps define a hex as available position
    public bool potencialTarget;//helps identify potential targets
    public bool lookingForTarget;//helps identify potential targets 

    private void Awake()
    {
        clickOnMe = GetComponent<ClickOnMe>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MakeMeActive()//sets active state to this hex
    {
        battleHexState = HexState.active;
    }
    public void MakeMeInActive()//sets inactive state to this hex
    {
        if (battleHexState != HexState.active)//excludes manually modified active hexes
        {
            Landscape.color = new Color32(120, 120, 120, 250);//sets new color to an inactive hex
        }

    }
    public virtual void MakeMeAvailable()
    {
        currentState.sprite = clickOnMe.fieldManager.availableToMove;//sets the white frame to a hex
        currentState.color = new Color32(255, 255, 255, 255);
    }
    public virtual void MakeMeTargetToMove()//defines a hex as selected position
    {
        clickOnMe.isTargetToMove = true;
        BattleController.targetToMove = this;
        currentState.sprite = clickOnMe.fieldManager.availableAsTarget;//sets the green frame to a hex
    }
    public void DefineMeAsStartingHex()//defines this hex as starting position
    {
        distanceText.distanceFromStartingPoint = 0;
        isStartingHex = true;
        distanceText.stepsToGo = 1;//to get out from deserts and swamps
    }
    public virtual bool AvailableToGround()
    {
        return true;
    }
    public void MakeMeDeploymentPosition()//displays the hex as a potential position for the hero
    {
        deploymentPos.GetComponent<PolygonCollider2D>().enabled = true;//enables collider (and clicking)
        deploymentPos.GetComponent<Image>().color = new Color32(255, 255, 255, 255);//displays a checkmark
    }
    public void CleanUpDeploymentPosition()//hides a checkmark, disables collider
    {
        deploymentPos.GetComponent<PolygonCollider2D>().enabled = false;// disables collider(and clicking)
        deploymentPos.GetComponent<Image>().color = new Color32(255, 255, 255, 0);//hides a checkmark
    }
    internal void DefineMeAsPotencialTarget()//defines hex as a potential target for attack
    {
        currentState.color = new Color(255, 0, 0, 255);//displays red frame 
        potencialTarget = true;
    }
    public void SetDefaultValue()//returns the hex to the initial state
    {
        isStartingHex = false;
        isNeighboringHex = false;
        isIncluded = false;
        lookingForTarget = false;
        distanceText.GetComponent<Text>().color = new Color32(255, 255, 255, 0);//hides numbers
        currentState.color = new Color32(255, 255, 255, 0);//hides the frame
        Landscape.color = new Color32(255, 255, 255, 255);//hides the optimal path
        distanceText.distanceFromStartingPoint = distanceText.defaultDistance;
        distanceText.stepsToGo = distanceText.defaultstepsToGo;
        potencialTarget = false;
    }
}
    
