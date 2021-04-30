using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Hero
{
    IAttacking dealsDamage = new SimpleMeleeAttack();//simple attack behavior reference
    public override void DealsDamage(BattleHex target)
    {
        //    //launches damage methods
        dealsDamage.HeroIsDealingDamage(this, BattleController.currentTarget);
    }
public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PositionsForGround();
        return adjFinder;
    }
    public override void DefineTargets()
    {
        IDefineTarget wayToLookForTargets = new TargetPlayerMelee();
        wayToLookForTargets.DefineTargets(this);
    }
    public override void HeroIsAtacking()//starts the attack
    {
        base.HeroIsAtacking();
        GetComponent<Animator>().SetTrigger("isAttacking");
    }
}
