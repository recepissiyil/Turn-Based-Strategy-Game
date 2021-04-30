using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroAttributes", menuName ="Fighter")]
public class CharAttributes : ScriptableObject
{
    [Header("Default Attributes")]
    public int velocity;
    public float initiative;
    public int hp;
    public int atack;
    public int resistance;
    public int stack;
 
    [SerializeField] int atackdistanse;

    [Header("General Attributes")]
    public bool isRanger;
    public bool isFlying;
    public Sprite heroSprite;
    public Hero heroSO;
    public bool isDeployed;

    [Header("Current Attributes")]
    float initiativeCurrent;
    public float InitiativeCurrent
    {
        get { return initiativeCurrent; }
        set { initiativeCurrent = value; }
    }
    int hpCurrent;
    public int HPCurrent
    {
        get { return hpCurrent; }
        set { hpCurrent = value; }
    }
    int atackCurrent;
    public int AtackCurrent
    {
        get { return atackCurrent; }
        set { atackCurrent = value; }
    }
    int resistanceCurrent;
    public int ResistanceCurrent
    {
        get { return resistanceCurrent; }
        set { resistanceCurrent = value; }
    }
    int stackCurrent;
    public int StackCurrent
    {
        get { return stackCurrent; }
        set//excludes negative variable value
        {
            if (value > 0) { stackCurrent = value; }
            else { stackCurrent = 0; }
        }
    }
    public int Atackdistanse
    {
        get//returns 1 for melee fighters, returns value of atackdistance for flying fighters
        {
            if (!isRanger) { return 1; }
            else { return atackdistanse; }
        }
        //cannot set another value
    }
    int velocityCurrent;
    public int CurrentVelocity
    {
        get { return velocityCurrent; }
        set { velocityCurrent = value; }
    }
    public void SetCurrentAttributes()//at the beginning of the battle sets the default values
    {
        hpCurrent = hp;
        atackCurrent = atack;
        resistanceCurrent = resistance;
        stackCurrent = stack;
        initiativeCurrent = initiative;
        velocityCurrent = velocity;
    }
    public void SetDefaultVelocityAndInitiative()
    {
        velocityCurrent = velocity;
        initiativeCurrent = initiative;
    }
    public int Calculatelosses()//calculates the losses suffered by the units during the battle
    {
        return stack - stackCurrent;
    }
}
