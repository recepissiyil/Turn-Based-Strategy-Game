using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    [SerializeField] DamagingFlyingObject arrow;//arrow's prefab
    [SerializeField] internal Vector3 initialPosCorrection;//adjusts arrow position
    IAttacking dealsDamage = new SimpleMeleeAttack();//simple attack behavior reference
    public override void DealsDamage(BattleHex target)
    {

    }

    public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PositionsForGround();
        return adjFinder;
    }
    public override void DefineTargets()
    {
        IDefineTarget wayToLookForTargets = new TargetPlayerRange();
        wayToLookForTargets.DefineTargets(this);
    }
    public override void HeroIsAtacking()//starts the attack
    {
        base.HeroIsAtacking();
        GetComponent<Animator>().SetTrigger("isAttacking");//activates animation
        InstantiateArrow();//instantiates an arrow
    }
    private void InstantiateArrow()//instantiates an arrow
    {
        //position where the arrow will appear
        Vector3 positionForArrow = new Vector3(transform.position.x,
                                 transform.position.y + initialPosCorrection.y, transform.position.z);
        Hero currentTarget = BattleController.currentTarget.GetComponentInChildren<Hero>();
  
        Quaternion rotation = CalcRotation.CalculateRotation(currentTarget); ;//determines the angle of rotation for the arrow
        DamagingFlyingObject Arrow = Instantiate(arrow, positionForArrow, rotation, transform);//instantiates an arrow object
        Arrow.FireArrow(dealsDamage);//fires the arrow
    }
}
