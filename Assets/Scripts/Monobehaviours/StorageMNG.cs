using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageMNG : MonoBehaviour
{
    [SerializeField] internal CurrentProgress currentProgress;
    [SerializeField] CharIcon iconPrefab;
    List<CharAttributes> regimentIcons = new List<CharAttributes>();
    ScrollRect scrollRect;
    ////sprites  for the background
    [SerializeField] internal Sprite selectedIcon;
    [SerializeField] internal Sprite defaultIcon;
    [SerializeField] internal Sprite deployedRegiment;
    public static event Action<CharAttributes> OnRemoveHero;//event when a player removes a hero
    public delegate void DeleteHero(CharAttributes SOofHero);
    public static event DeleteHero OnClickOnGrayIcon;
    public CharIcon[] charIcons;//collects all icons

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        CallHeroIcons();

        //binds the DisableME method to the clocking on starting button
        StartBTN.OnStartingBattle += DisableMe;
        charIcons = GetComponentsInChildren<CharIcon>();//collects all icons
    }
    private void CallHeroIcons()//places heroes icons in storage
    {
        regimentIcons = currentProgress.heroesOfPlayer;//access to player's regiments
        Transform parentOfIcons = scrollRect.content.transform;//defines the parent object for all icons
        for (int i = 0; i < regimentIcons.Count; i++)
        {
            CharIcon fighterIcon = Instantiate(iconPrefab, parentOfIcons);//instantiate and return an icon
            fighterIcon.charAttributes = regimentIcons[i];//assigns scriptable object to an icon
            fighterIcon.FillIcon();//fills the icon with a sprite and the number of units.
            //data taken from scriptable object
        }
    }
    internal void TintIcon(CharIcon clickedIcon)//marks a regiment to be placed on the battlefield
    {
        CharIcon[] charIcons = GetComponentsInChildren<CharIcon>();//collects all icons
        foreach (CharIcon icon in charIcons)
        {
            if (!icon.charAttributes.isDeployed)
                icon.backGround.sprite = defaultIcon;//sets default background to the icon
        }
        clickedIcon.backGround.sprite = selectedIcon;//sets green background to the icon
        Deployer.readyForDeploymentIcon = clickedIcon;//Saves the selected icon in memory
    }

    private void RemoveHero(Hero hero)//removes the selected hero
    {
        BattleHex parentHex = hero.GetComponentInParent<BattleHex>();//to access the MakeMeDeploymentPosition method
        parentHex.MakeMeDeploymentPosition();//returns the hex to the starting position
        Destroy(hero.gameObject);//removes the hero
    }
    public void RemoveRegiment(CharAttributes SOHero)//calls OnRemoveHero event
    {
       OnClickOnGrayIcon(SOHero);
    }
    private void DisableMe()//disables the storage
    {
        gameObject.SetActive(false);
    }

}
