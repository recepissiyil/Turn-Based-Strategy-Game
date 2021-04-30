using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharIcon : MonoBehaviour
{
    [SerializeField] internal Image heroImage;//stores a child object with a hero's sprite
    [SerializeField] internal Image backGround;//stores a child object with a background sprite
    [SerializeField] internal TMPro.TextMeshProUGUI stackText;//stores a child object with a number of stack
    [SerializeField] internal CharAttributes charAttributes;//access to hero data
    StorageMNG storage;
    string losses = "0";
    private void Start()
    {
        storage = GetComponentInParent<StorageMNG>();
        StorageMNG.OnClickOnGrayIcon += ReturnDefaultState; //subscribes the ReturnDefaultState method to an OnRemoveHero event
    }
    internal void FillIcon()
    {
        heroImage.sprite = charAttributes.heroSprite;
        stackText.text = charAttributes.stack.ToString();
        charAttributes.isDeployed = false;
    }
    //changes the sprite and displays losses
    internal void FillIconWhenGameIsOver(CharAttributes Attributes)
    {
        //displays the sprite of the hero who participated in the battle
        heroImage.sprite = Attributes.heroSprite;
        //displays losses
        if (Attributes.Calculatelosses() != 0)
        {
            losses = "- " + Attributes.Calculatelosses();
        }

        stackText.text = losses;
    }
    public void IconClicked()//responds to a click on a button
    {
        StorageMNG storage = GetComponentInParent<StorageMNG>();
        if (!charAttributes.isDeployed)//evaluates if the unit is already on the battlefield
        {
            storage.TintIcon(this);//marks a regiment to be placed on the battlefield
        }
        else
        {
            storage.RemoveRegiment(charAttributes);
            //storage.ReturnRegiment(this);//returns the hero to the storage
        }
    }
    public void HeroIsDeployed()
    {
        backGround.sprite = storage.deployedRegiment;
        charAttributes.isDeployed = true;
        
    }
    public void ReturnDefaultState(CharAttributes selectedCharAttributes)//sets light green background to the icon
    {
        if (selectedCharAttributes == charAttributes)//determines if the icon should respond to an event
        {
            backGround.sprite = GetComponentInParent<StorageMNG>().defaultIcon; //sets light green icon
            charAttributes.isDeployed = false;//defines the hero as available for deployment
        }
    }
}
