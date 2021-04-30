using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public CurrentProgress currentProgress;//stores the Bar Object
    public List<CharIcon> heroIcons;//stores all icon prefabs
    List<CharAttributes> regimentsSO = new List<CharAttributes>();//stores all scriptable objects of heroes
    ScrollRect scrollRect;//used as parent object for icons
    [SerializeField] CharIcon iconPrefab; //icon prefab

    // to change the text depending on the result of the battle
    [SerializeField] internal TMPro.TextMeshProUGUI VicOrDefeat;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        CallHeroIcons();//instantiates all icons of heroes who participated in the battle
    }

    //changes the text depending on the result of the battle
    public void DefeatOrVictory(bool victory)
    {
        if (victory)//if the victory is won
        {
            VicOrDefeat.text = "Victory";
        }
        else
        {
            VicOrDefeat.text = "Defeat";
        }
    }

    //instantiates all icons of heroes who participated in the battle
    public void CallHeroIcons()//places heroes icons in storage
    {
        regimentsSO = currentProgress.heroesOfPlayer;//access to player's regiments
        Transform parentOfIcons = scrollRect.content.transform;//defines the parent object for all icons
        for (int i = 0; i < regimentsSO.Count; i++)
        {
            if (regimentsSO[i].isDeployed)
            {
                CharIcon fighterIcon = Instantiate(iconPrefab, parentOfIcons);//instantiate and return an icon
                fighterIcon.FillIconWhenGameIsOver(regimentsSO[i]);//fills the icon with
                //data taken from scriptable object
            }
        }
    }
}
