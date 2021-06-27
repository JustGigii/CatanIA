using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TradeOpenerOnline : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Trade;
    public int TradeRed;
    public string TradeBlue;
    public List<GameObject> PointPort;
    bool ActvaiteTradePlayer;
    bool ActvaiteTradePort;
    bool[] AccseptTrade;
    Mannger Mannger;
    int one;
    int index;
    void Start()
    {
        index = -1;
        ActvaiteTradePort = false;
        ActvaiteTradePlayer = false;
        AccseptTrade = new bool[2];
        AccseptTrade[(int)Accsept.blue] = false;
        AccseptTrade[(int)Accsept.red] = false;
        Mannger = GameObject.Find("Mannger").GetComponent<Mannger>();
    }

    // Update is called once per frame
    void Update()
    {

        if (ManngerOnline.Command == this.name)
        {
            one = 1;
            if (TradeBlue == "")
            {
                Trade.active = true;
                ActvaiteTradePlayer = true;
            }

            else
            {
                foreach (GameObject i in PointPort)
                {
                    Points point = i.GetComponent<Points>();
                    if (point.PlayerColor == Mannger.NowPlay.ColorP)
                    {
                        ActvaiteTradePort = true;
                        Trade.active = true;
                    }
                }
                switch (TradeBlue)
                {
                    case "Weat":
                        index = (int)Resouresed.Weat;
                        break;
                    case "Stone":
                        index = (int)Resouresed.Stone;
                        break;
                    case "Wood":
                        index = (int)Resouresed.Wood;
                        break;
                    case "Clay":
                        index = (int)Resouresed.Clay;
                        break;
                    case "Sheep":
                        index = (int)Resouresed.Sheep;
                        break;
                    case "?":
                        index = 0;
                        break;

                }

            }

        }
        if (ManngerOnline.Command == "Exit")
            makezero();
        if (ManngerOnline.Command == "blueAcc")
            AccseptTrade[(int)Accsept.blue] = true;

        if (ManngerOnline.Command == "RedAcc" && Bot.BotForTheard == null) 
            AccseptTrade[(int)Accsept.red] = true;
        if (ActvaiteTradePlayer)
            PlayerTrade();
        if (ActvaiteTradePort)
            MakePort(index);
    }
    void PlayerTrade()
    {
      
        ClickChecker.TradeBlue(Trade.GetComponent<TradeMenger>().Blue, Mannger.Blue, AccseptTrade, ref one,ManngerOnline.Command);
        ClickChecker.TradeRed(Trade.GetComponent<TradeMenger>().Red, Mannger.Red, AccseptTrade, ref one,ManngerOnline.Command);
        if(AccseptTrade[(int)Accsept.blue]&&Bot.BotForTheard!= null)
        {
            Player BotAfter = new Player(Bot.BotForTheard);
            Player Human = new Player(Mannger.Blue);
            MakeVirtualTrade(0, Human, BotAfter);
            if (BuilderBot.MakeTrade(Bot.BotForTheard, BotAfter))
                AccseptTrade[(int)Accsept.red] = true;
            else
            {
                makezero();
                
            }
        }

        if (AccseptTrade[(int)Accsept.blue] && AccseptTrade[(int)Accsept.red])
        {
            MakeTrade(0);
        }
    }
    public void makezero()
    {
        Text[] Blue = Trade.GetComponent<TradeMenger>().Blue;
        Text[] Red = Trade.GetComponent<TradeMenger>().Red;
        for (int i = 0; i < Blue.Length; i++)
        {
            Red[i].text = "0";
            Blue[i].text = "0";
        }
        index = -1;
        Trade.active = false;
        ActvaiteTradePlayer = false;
        ActvaiteTradePort = false;
        AccseptTrade[(int)Accsept.blue] = false;
        AccseptTrade[(int)Accsept.red] = false;
        one = 1;
    }
    void MakeTrade(int num)
    {
        Text[] Blue = Trade.GetComponent<TradeMenger>().Blue;
        Text[] Red = Trade.GetComponent<TradeMenger>().Red;
        if (num == 0)
        {
            Mannger.Red.SubResoures(int.Parse(Red[(int)Resouresed.Wood].text), int.Parse(Red[(int)Resouresed.Stone].text), int.Parse(Red[(int)Resouresed.Clay].text), int.Parse(Red[(int)Resouresed.Sheep].text), int.Parse(Red[(int)Resouresed.Weat].text));
            Mannger.Blue.AddResoures(int.Parse(Red[(int)Resouresed.Wood].text), int.Parse(Red[(int)Resouresed.Stone].text), int.Parse(Red[(int)Resouresed.Clay].text), int.Parse(Red[(int)Resouresed.Sheep].text), int.Parse(Red[(int)Resouresed.Weat].text));
            Mannger.Blue.SubResoures(int.Parse(Blue[(int)Resouresed.Wood].text), int.Parse(Blue[(int)Resouresed.Stone].text), int.Parse(Blue[(int)Resouresed.Clay].text), int.Parse(Blue[(int)Resouresed.Sheep].text), int.Parse(Blue[(int)Resouresed.Weat].text)); ;
            Mannger.Red.AddResoures(int.Parse(Blue[(int)Resouresed.Wood].text), int.Parse(Blue[(int)Resouresed.Stone].text), int.Parse(Blue[(int)Resouresed.Clay].text), int.Parse(Blue[(int)Resouresed.Sheep].text), int.Parse(Blue[(int)Resouresed.Weat].text));
        }
        else
        {
            Mannger.Playing.SubResoures(int.Parse(Red[(int)Resouresed.Wood].text), int.Parse(Red[(int)Resouresed.Stone].text), int.Parse(Red[(int)Resouresed.Clay].text), int.Parse(Red[(int)Resouresed.Sheep].text), int.Parse(Red[(int)Resouresed.Weat].text));
        }
        makezero();
    }
    void MakeVirtualTrade(int num,Player Reg,Player bot)
    {
        Text[] Blue = Trade.GetComponent<TradeMenger>().Blue;
        Text[] Red = Trade.GetComponent<TradeMenger>().Red;
        if (num == 0)
        {
            bot.SubResoures(int.Parse(Red[(int)Resouresed.Wood].text), int.Parse(Red[(int)Resouresed.Stone].text), int.Parse(Red[(int)Resouresed.Clay].text), int.Parse(Red[(int)Resouresed.Sheep].text), int.Parse(Red[(int)Resouresed.Weat].text));
            Reg.AddResoures(int.Parse(Red[(int)Resouresed.Wood].text), int.Parse(Red[(int)Resouresed.Stone].text), int.Parse(Red[(int)Resouresed.Clay].text), int.Parse(Red[(int)Resouresed.Sheep].text), int.Parse(Red[(int)Resouresed.Weat].text));
            Reg.SubResoures(int.Parse(Blue[(int)Resouresed.Wood].text), int.Parse(Blue[(int)Resouresed.Stone].text), int.Parse(Blue[(int)Resouresed.Clay].text), int.Parse(Blue[(int)Resouresed.Sheep].text), int.Parse(Blue[(int)Resouresed.Weat].text)); ;
            bot.AddResoures(int.Parse(Blue[(int)Resouresed.Wood].text), int.Parse(Blue[(int)Resouresed.Stone].text), int.Parse(Blue[(int)Resouresed.Clay].text), int.Parse(Blue[(int)Resouresed.Sheep].text), int.Parse(Blue[(int)Resouresed.Weat].text));
        }
        else
        {
            Mannger.Playing.SubResoures(int.Parse(Red[(int)Resouresed.Wood].text), int.Parse(Red[(int)Resouresed.Stone].text), int.Parse(Red[(int)Resouresed.Clay].text), int.Parse(Red[(int)Resouresed.Sheep].text), int.Parse(Red[(int)Resouresed.Weat].text));
        }
    }
    void MakePort(int index)
    {
        Text[] Blue = Trade.GetComponent<TradeMenger>().Blue;
        for (int i = 0; i < Blue.Length; i++)
        {
            Blue[i].text = "0";
        }
        if (index == 0 && one > 0)
        {
            ClickChecker.TradeBlue(Trade.GetComponent<TradeMenger>().Blue, Mannger.NowPlay, AccseptTrade, ref one,ManngerOnline.Command);

        }
        else
        {
            Blue[index].text = "1";
        }
        if (TradeRed > 0)
        {
            ClickChecker.TradeRed(Trade.GetComponent<TradeMenger>().Red, Mannger.NowPlay, AccseptTrade, ref TradeRed, ManngerOnline.Command);

        }
        if (AccseptTrade[(int)Accsept.blue] || AccseptTrade[(int)Accsept.red])
        {
            MakeTrade(1);

        }

    }
}

