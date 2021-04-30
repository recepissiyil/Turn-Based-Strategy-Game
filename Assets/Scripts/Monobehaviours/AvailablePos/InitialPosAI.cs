using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPosAI : MonoBehaviour, IInitialHexes
{
    List<BattleHex> initialHexes = new List<BattleHex>();//collects neighbouring hexes for evaluated hex
    public List<BattleHex> GetNewInitialHexes()
    {
        initialHexes.Clear();// empty the array before filling it again
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            if (hex.isNeighboringHex & !hex.isIncluded
                && ifThereIsPlayersRegiment(hex))//eliminates unnecessary hexes
            {
                initialHexes.Add(hex);
            }
        }
        return initialHexes;
    }

    //checks if the initial hex is occupied by a player’s squad
    private bool ifThereIsPlayersRegiment(BattleHex evaluatedHex)
    {
        bool AIPosfalse = true;
        //if the object of type Hero does not contain a class of Enemy type
        if (evaluatedHex.GetComponentInChildren<Hero>() != null &&
            evaluatedHex.GetComponentInChildren<Enemy>() == null)
        {
            evaluatedHex.DefineMeAsPotencialTarget();
            AIPosfalse = false;
        }
        return AIPosfalse;
    }
}
