using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public bool isMoving = false;//enables and disables motion
    Hero hero;//Hero component reference
    public List<Image> path;//Images of hexes included in Optimal path
    private int totalSteps;//number of hexes included in Optimal path
    private int currentStep;//list index defining the current target for movement
    Vector3 targetPos;// coordinates of the hex defined as the current target for movement
    float speedOfAnim = 2f;//determines the speed of movement
    internal bool lookingToTheRight = true;//determines the rotation of the hero
    SpriteRenderer heroSprite;//SpriteRenderer component reference
    BattleController battleController;
    void Start()
    {
        hero = GetComponent<Hero>();
        heroSprite = GetComponent<SpriteRenderer>();
        battleController = FindObjectOfType<BattleController>();
    }

    void Update()
    {
        if (isMoving)//starts moving if the value of the isMoving variable is true
            HeroIsMoving();
    }
    public void StartsMoving()
    {
        battleController.events.gameObject.SetActive(false);//disables click response
        battleController.CleanField();//sets the initial state to all active hexes
        currentStep = 0;//updates the variable value to start with the first hex of the optimal path
        totalSteps = path.Count - 1;//number of hexes included in optimal path, used as an index
        isMoving = true;//enables movement
        hero.GetComponent<Animator>().SetBool("IsMoving", true);//enables animation of movement
        ResetTargetPos();//switches the elements of the path list defining the next step
    }
    private void ResetTargetPos()
    {
        targetPos = new Vector3(path[currentStep].transform.position.x,
      path[currentStep].transform.position.y,
      transform.position.z);//defines next step changing the value of currentStep variable
        ControlDirection(targetPos);//controls the rotation of the hero
    }
    private void HeroIsMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos,
                        speedOfAnim * Time.deltaTime);//moves a hero in the given coordinates
        ManageSteps();
        ManageSortingLayer(path[currentStep].GetComponentInParent   <BattleHex>());
    }
    private void ManageSteps()//changes the value of the currentStep variable depending 
                              //on the distance to the current target
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.1f
      && currentStep < totalSteps)//compares the coordinates of hero's current position 
                                  //and the distance to current target position
        {
            currentStep++;//adds one to the value of the currentStep variable
            ResetTargetPos();//sets a new target hex
        }
        else if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            StopsMoving();//stops movement if the hero reaches the end point of movement
        }
    }
    private void StopsMoving()
    {
        isMoving = !isMoving;//reverses the value of a variable
        transform.parent = path[currentStep].transform;
        hero.GetComponent<Animator>().SetBool("IsMoving", false);//stops movement animation
        hero.heroData.CurrentVelocity = 0;
        hero.DefineTargets();
        battleController.events.gameObject.SetActive(true);//enables click response
    }
    internal void ControlDirection(Vector3 targetPos)
    {
        //compares the coordinates of the hero and the coordinates of the target hex
        //rotates the hero if necessary
        if (transform.position.x > targetPos.x && lookingToTheRight ||
            transform.position.x < targetPos.x && !lookingToTheRight)
        {
            heroSprite.flipX = !heroSprite.flipX;//rotates a sprite of the hero
            lookingToTheRight = !lookingToTheRight;//sets the opposite value for a variable
        }
    }
    internal void ManageSortingLayer(BattleHex targetHex)
    {
        heroSprite = GetComponent<SpriteRenderer>();
        Canvas canvasOfStack = GetComponentInChildren<Canvas>();
        int currentLayer = 16 - targetHex.verticalCoordinate;
        canvasOfStack.sortingOrder = currentLayer+1;
        heroSprite.sortingOrder = currentLayer;
        //hero.stack.GetComponent<Canvas>().sortingOrder = currentLayer;
    }
}
