using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingAttack : MonoBehaviour, IAttacking
{
    DamageCounter damageController = new DamageCounter();//access to damage calculation
    int targetStack;//final numbers after the attack
    public void HeroIsDealingDamage(Hero atacker, Hero Target)
    {
        //calculates the final number of units in the regiment of the attacked hero
        targetStack = damageController.CountTargetStack(atacker, Target);
        int currentInt = Target.heroData.StackCurrent;
        Freeze(Target);
        //assigns a new value to the number of units of the attacked hero
        Target.heroData.StackCurrent = targetStack;
        Target.stack.StartCoroutine(Target.stack.CountDownToTargetStack(currentInt, targetStack));
    }

    void Freeze(Hero Target)
    {
        print(2323);
        Target.heroData.InitiativeCurrent = 0;
        Target.GetComponent<SpriteRenderer>().color = new Color32(5, 255, 255, 255);
    }
}
