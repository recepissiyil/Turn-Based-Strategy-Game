using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : Hero
{
    [SerializeField] DamagingFlyingObject mageBall;//arrow's prefab
    [SerializeField] internal Vector3 initialPosCorrection;//adjusts arrow position
    IAttacking dealsDamage = new FreezingAttack();//simple attack behavior reference


    public override void DealsDamage(BattleHex target)
    {

    }
    public override IAdjacentFinder GetTypeOfHero()
    {
        IAdjacentFinder adjFinder = new PositionsForFlying();
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
        InstantiateBall();//instantiates an arrow
    }
    private void InstantiateBall()//instantiates an arrow
    {
        //position where the arrow will appear
        Vector3 positionForArrow = new Vector3(transform.position.x,
                                 transform.position.y + initialPosCorrection.y, transform.position.z);
        Hero currentTarget = BattleController.currentTarget.GetComponentInChildren<Hero>();

        Quaternion rotation = CalcRotation.CalculateRotation(currentTarget); ;//determines the angle of rotation for the arrow
        DamagingFlyingObject ball = Instantiate(mageBall, positionForArrow, rotation, transform);//instantiates an arrow object
        ball.FireArrow(dealsDamage);//fires the arrow
    }
}
