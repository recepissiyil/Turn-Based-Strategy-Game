using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnMe : MonoBehaviour, IPointerClickHandler
{
    BattleHex hex;
    public bool isTargetToMove = false;//becomes true when the hex is clicked
    public FieldManager fieldManager;
    BattleController battleController;

    void Awake()
    {
        hex = GetComponent<BattleHex>();
        fieldManager = FindObjectOfType<FieldManager>();
        battleController = FindObjectOfType<BattleController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hex.potencialTarget)
        {
            battleController.events.gameObject.SetActive(false);//disables click response
            BattleController.currentTarget = this.GetComponentInChildren<Hero>();
            BattleController.currentAtacker.HeroIsAtacking();
            return;
        }
        if (!isTargetToMove)
        SelectTargetToMove();
       else
        {
            BattleController.currentAtacker.GetComponent<Move>().StartsMoving();
        }
    }

    private void SelectTargetToMove()
    {
        ClearPreviousSelectionOfTargetHex();
        if (hex.isNeighboringHex)
        {
            hex.MakeMeTargetToMove();
            BattleController.currentAtacker.GetComponent<OptimalPath>().MatchPath();
        }
    }

    public void ClearPreviousSelectionOfTargetHex()//Cancels previous selection
    {
        foreach (BattleHex hex in FieldManager.activeHexList)//looks for selected hex in active hexes list
        {
            if (hex.clickOnMe.isTargetToMove == true)//evaluates hex if it is target
            {
                hex.GetComponent<ClickOnMe>().isTargetToMove = false;//overrides boolean
                hex.MakeMeAvailable();// sets white frame
            }
            hex.Landscape.color = new Color32(250, 250, 250, 250);
        }
    }
}



