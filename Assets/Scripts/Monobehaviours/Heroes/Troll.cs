using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Hero
{
    IAttacking dealsDamage = new SimpleMeleeAttack();//simple attack behavior reference
    public override void DealsDamage(BattleHex target)
    {
        dealsDamage.HeroIsDealingDamage(this, BattleController.currentTarget);
    }
    public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PosForGroundAI();
        return adjFinder;
    }
    public override void DefineTargets()
    {
        BattleHex initialHex = GetComponentInParent<BattleHex>();//hex occupied by the troll
        IEvaluateHex checkHex = new IfItIsTarget();//neighboring hexes validation rule

        //collect potencial targets
        List<BattleHex> neighboursToCheck = NeighboursFinder.GetAdjacentHexes(initialHex, checkHex);
        if (neighboursToCheck.Count > 0)//if there is a target on the adjacent hex
        {
            HeroIsAtacking();//attacks the target
        }
        else { turn.TurnIsCompleted(); }//turn is completed
    }
    public override void HeroIsAtacking()//starts the attack
    {
        base.HeroIsAtacking();//Executes code specified in the parent class method
        //launches the isAttacking animation clip
        GetComponent<Animator>().SetTrigger("isAttacking");
    }

}
