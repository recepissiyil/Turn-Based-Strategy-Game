using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : BattleHex
{
    public override void MakeMeTargetToMove()
    {
        clickOnMe.ClearPreviousSelectionOfTargetHex();
    }
    public override void MakeMeAvailable()
    {
        currentState.color = new Color32(255, 255, 255, 0);
    }
    public override bool AvailableToGround()
    {
        return false;
    }
}
