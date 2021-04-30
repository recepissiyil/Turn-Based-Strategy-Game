using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    BattleController battleController;
    IInitialHexes getInitialHexes = new InitialPos();
    //public delegate void StartNewTurn();
    //public static event StartNewTurn OnNewTurn;
    public delegate void StartNewRound();
    public static event StartNewRound OnNewRound;
    [SerializeField] GameOver gameOverPanel; //Panel prefab
    FieldManager parent; ////Accessing the HexFiels Object

    private void Start()
    {
        battleController = GetComponent<BattleController>();
        //a new turn is initialized by pressing the start button
        StartBTN.OnStartingBattle += InitializeNewTurn;
        parent = FindObjectOfType<FieldManager>();
    }
    public void InitializeNewTurn()
    {
        battleController.CleanField();
        battleController.DefineNewAtacker();//finds an attacking hero
        Hero currentAtacker = BattleController.currentAtacker;//gets local atacker (for parameters)
        GetStartingHex();
        if (currentAtacker.GetComponent<Enemy>() == null)//checks if it is a player’s turn
        {
            IInitialHexes getInitialHexes = new InitialPos();
            currentAtacker.PlayersTurn(getInitialHexes);//player starts his turn

        }
        //player starts its turn
        else
        {
            IInitialHexes getInitialHexes = new InitialPosAI();
            currentAtacker.GetComponent<Enemy>().Aisturnbegins(getInitialHexes);
        }
    }
    //returns the hex on which the attacking hero stands
    private void GetStartingHex()
    {
        BattleHex startingHex = BattleController.currentAtacker.GetComponentInParent<BattleHex>();
        startingHex.DefineMeAsStartingHex(); //changes the properties of the starting hex

    }
    public void TurnIsCompleted()
    {
       
        StartCoroutine(NextTurnOrGameOver());
    }
    public IEnumerator NextTurnOrGameOver()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);//pause length
        yield return wait;
        battleController.events.gameObject.SetActive(true);//enables click response
        List<Hero> allFighters = battleController.DefineAllFighters();
        if (IfThereIsAIRegiment(allFighters) && IfThereIsPlayerRegiment(allFighters))
        {
            NextTurnOrNextRound(allFighters);
        }
        else
        {

            battleController.CleanField();//clearing the battlefield from frames and numbers
            GameOver GameOver = Instantiate(gameOverPanel, parent.transform);
            GameOver.DefeatOrVictory(IfThereIsPlayerRegiment(allFighters));//displays a victory or defeat message
            RemoveAllHeroes(allFighters);

        }
    }
    private void RemoveAllHeroes(List<Hero> allFighters)//remove all heroes from the battlefield
    {
        foreach (Hero hero in allFighters)
        {
            Destroy(hero.gameObject);//remove every hero left on the battlefield
        }
    }
    bool IfThereIsAIRegiment(List<Hero> allFighters)
    {
        return allFighters.Exists(x => x.gameObject.GetComponent<Enemy>());
    }
     bool IfThereIsPlayerRegiment(List<Hero> allFighters)
    {
        return allFighters.Exists(x => !x.gameObject.GetComponent<Enemy>());
    }
    private void NextTurnOrNextRound(List<Hero> allFighters)
    {
        if (allFighters.Exists(x => x.heroData.InitiativeCurrent > 0))
        {
            InitializeNewTurn();
        }
        else
        {
            OnNewRound();
            InitializeNewTurn();
        }
    }
}
