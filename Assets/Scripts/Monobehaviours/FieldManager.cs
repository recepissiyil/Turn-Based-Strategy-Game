using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public RowManager[] allRows;
    public static BattleHex[,] allHexesArray;// contains all Hexes in the BattleField
    int allRowsLength;
    public static List<BattleHex> activeHexList = new List<BattleHex>();
    public Sprite availableAsTarget; //green frame
    public Sprite notAavailable; //enemy, red frame
    public Sprite availableToMove; //white frame
 
    void Awake()
    {
 
        allRows = GetComponentsInChildren<RowManager>();
        allRowsLength = allRows.Length;
        for (int i = 0; i<allRowsLength;i++)
        {
            allRows[i].allHexesInRow = allRows[i].GetComponentsInChildren<BattleHex>();
        }
        CreateAllHexesArray();       
    }
    private void Start()
    {
        IdentifyHexes();
    }
    private void CreateAllHexesArray()//creates coordinate system
    {
        int heightOfArray = allRows.Length;
        int widthOfArray = allRows[0].allHexesInRow.Length;
        allHexesArray = new BattleHex[widthOfArray, heightOfArray];

      for (int i=0;i<heightOfArray;i++)
        {
            for (int j = 0; j < widthOfArray; j++)
            {
                allHexesArray[j, i] = allRows[heightOfArray- i -1].
                                      allHexesInRow[widthOfArray - j - 1];
                allHexesArray[j, i].verticalCoordinate = i+1;
                allHexesArray[j, i].horizontalCoordinate = j + 1;

            }
        }
        
    }
    private void IdentifyHexes()//highligths inactive and sets new value to active hexes
    {
        //compares coordinates of a hex with the distance from the center (0, 0, z)
     foreach (BattleHex hex in allHexesArray)
        {
            if (Mathf.Abs(hex.transform.position.x) > 11.3f |
                Mathf.Abs(hex.transform.position.y) > 6.2f)
            {
                hex.MakeMeInActive();
            }
            else
            {
                hex.MakeMeActive();
            }
        }
        CreateActiveHexList();
    }
    //creates list filled with active hexes
    private void CreateActiveHexList()
    {
        //looks for active hexes in all hexes
            foreach (BattleHex hex in allHexesArray)
            {
                if (hex.battleHexState == HexState.active)
                {
                    //adds an active hex to the list
                    activeHexList.Add(hex);
                }
            }
          }
}
