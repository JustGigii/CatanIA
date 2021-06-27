using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Player BotPlayer;
    Mannger GameMannger;
    public List<GameObject> Way;
    //public GameObject Port;
    SetUpBot SetUp;
    BuilderBot Builder;
    GameObject start;
    GameObject end;
    static public int[] Conter;
    static public Player BotForTheard;
    // Start is called before the first frame update
    void Start()
    {
        SetUp = new SetUpBot();
        GameMannger = GetComponent<Mannger>();
        BotPlayer = GameMannger.Red;
        BotForTheard = BotPlayer;
        Way = null;
        Builder = new BuilderBot();
        Conter = new int[5] { 0, 0, 0, 0, 0 };
      }

    // Update is called once per frame
    void Update()
    {
        if (GameMannger.NumberOnCube == 7)
            Builder.DropResources(BotPlayer);
        if (GameMannger.Playing == BotPlayer)
        {
            GameMannger.NowBot = true;
            if (Way == null)
                Way = new List<GameObject>((SetUp.MakeSetUP(BotPlayer, GameMannger.Board, GameMannger.HexaList, ref start, ref end)));
            int diff = GameMannger.Blue.Score - BotPlayer.Score;
            Builder.BuildStrategy(BotPlayer,GameMannger.HexaList ,diff, ref Way, ref start, ref end);
            finishTurn();
        }
    }
    void finishTurn()
    {
        GameMannger.ChancePlayer();
        GameMannger.NowBot = false;
    }
}
