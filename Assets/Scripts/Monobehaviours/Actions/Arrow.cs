using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    internal Vector3 targetPosition;//The place where the enemy is
    [SerializeField] Vector3 targetPosAdj;//adjusting the point where the arrow hits the enemy
    internal bool ArrowFlies = false;//activates or deactivates the movement
    [SerializeField] float velocity;//flying velocity
    IAttacking dealsDamage = new SimpleMeleeAttack();//simple attack behavior reference

    void Update()
    {
        if (ArrowFlies) //activates or deactivates the movement
        {
            transform.position = Vector2.MoveTowards(transform.position,
                targetPosition, velocity * Time.deltaTime);//moves an object towards the target
             
            //stops movement if the object is very close to the target
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                ArrowFlies = false;//stops the movement

                //deals damage to the target
                Hero currentTarget = BattleController.currentTarget;
                dealsDamage.HeroIsDealingDamage(BattleController.currentAtacker, currentTarget);
                //currentTarget.GetComponent<Animator>().SetTrigger("IsTakingDamage");
                DestroyMe();//destroys an arrow after dealing damage to the target
            }
        }
    }
    public void FireArrow()//starts moving the arrow towards the target
    {
        Vector3 currentTargetPos = BattleController.currentTarget.transform.position;
        targetPosition = currentTargetPos + targetPosAdj;//clarifies target coordinates
        ArrowFlies = true;//starts moving the arrow
    }
    private void DestroyMe()
    {
        Destroy(gameObject);//destroys this arrow
    }
}
