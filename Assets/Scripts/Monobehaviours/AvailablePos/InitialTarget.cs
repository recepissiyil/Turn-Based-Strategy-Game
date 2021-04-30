using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTarget : IInitialHexes
{
    List<BattleHex> initialHexes = new List<BattleHex>();//collects neighbouring hexes for evaluated hex
    public List<BattleHex> GetNewInitialHexes()
    {
        initialHexes.Clear();// empty the array before filling it again
        foreach (BattleHex hex in FieldManager.activeHexList)
        {
            if (hex.lookingForTarget)//eliminates unnecessary hexes
            {
                initialHexes.Add(hex);
            }
        }
        return initialHexes;
    }
}
