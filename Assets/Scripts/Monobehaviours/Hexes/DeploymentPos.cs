using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PositionForRegiment { none, player, enemy };
public class DeploymentPos : MonoBehaviour
{
    public PositionForRegiment regimentPosition;//helps display potential position
    BattleHex parentHex;
    void Start()
    {
        parentHex = GetComponentInParent<BattleHex>();//finds the parent hex
        StartBTN.OnStartingBattle += DisableMe;
    }

    public void OnMouseDown()//is executed when the user has pressed the mouse button while over the Collider.
    {       
        //checks if the player clicked on the hex and if it is a potencial position
        if (Deployer.readyForDeploymentIcon != null && regimentPosition ==PositionForRegiment.player)
        {
            Deployer.DeployRegiment(parentHex);//deploys a regiment
        }
    }
    void DisableMe()
    {
        parentHex.CleanUpDeploymentPosition();
    }

}
