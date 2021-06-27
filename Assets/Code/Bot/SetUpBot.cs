using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
class SetUpBot : MonoBehaviour
{
    int[] Resurses;
    List<GameObject> Way;
    GameObject LongStart;
    bool LongRoadMode;
    //Thread thread;
  //  bool openTheard;
    public SetUpBot()
    {
        Resurses = new int[] { 0, 0, 0, 0, 0 };
        Way = new List<GameObject>();
        LongRoadMode = false;
        //thread = new Thread(LongPath);
        //openTheard = false;
        LongStart = null;
    }

    public List<GameObject> MakeSetUP(Player Bot, GameObject Borad, List<GameObject> HexaList,ref GameObject start, ref GameObject end)
    {
        List<int> index = new List<int>();
        if (Bot.Count < 4)
        {
            SerchNewRoad(index, Bot, Borad, HexaList);
            LongStart = Way[Way.Count - 1];
            if (Way.Count < 4)
            {
                Bot.AddPoint(Way[Way.Count - 1], 1);
                Bot.AddPoint(Way[0], 1);
                start = Way[0];
                if (Way.Count == 3)
                 Bot.AddPoint(Way[1], 0);
                LongRoadMode = true;
                LongPath();
                end = Way[Way.Count - 1];
                Way.RemoveAt(0);
                while (Bot.Count < 4 && Way != null)
                {
                    Bot.AddPoint(Way[0], 0);
                    Way.RemoveAt(0);
                }
                return Way;
            }
            else
            {
                Bot.AddPoint(Way[Way.Count - 1], 1);
                Bot.AddPoint(Way[0], 1);
                Bot.AddPoint(Way[Way.Count - 2], 0);
                Bot.AddPoint(Way[1], 0);
                start = Way[1];
                end = Way[Way.Count - 1];
                Way.RemoveAt(1);
                Way.RemoveAt(0);
                Way.RemoveAt(Way.Count - 2);
                Way.RemoveAt(Way.Count - 1);
                return Way;
            }
        }
        else
        {
            Way = null;
            LongRoadMode = true;
            end = null;
        }
        return null;
    }

    void LongPath()
    {
        Way = new List<GameObject>(BotRunServies.shortPath(LongStart,GameObject.Find("PointGray 48"),Bot.BotForTheard.ColorP));
       // openTheard = false;
       // thread.Abort();
    }
    void SerchNewRoad(List<int> index, Player Bot, GameObject Borad, List<GameObject> HexaList)
    {
        List<GameObject> AllStart = new List<GameObject>();
        List<GameObject> AllEnd = new List<GameObject>();
        index = new List<int>();
        index.Add((int)Resouresed.Weat);
        index.Add((int)Resouresed.Clay);
        AllStart = BotRunServies.GetPoints(index, Borad, HexaList);
        Resurses[(int)Resouresed.Weat] = 1;
        Resurses[(int)Resouresed.Clay] = 1;
        index.Clear();
        index.Add((int)Resouresed.Wood);
        index.Add((int)Resouresed.Sheep);
        index.Add((int)Resouresed.Stone);
        Resurses[(int)Resouresed.Wood] = 1;
        Resurses[(int)Resouresed.Sheep] = 1;
        Resurses[(int)Resouresed.Stone] = 1;
        AllEnd = BotRunServies.GetPoints(index, Borad, HexaList);
        ShortRoad(AllStart, AllEnd);
    }
    void ShortRoad(List<GameObject> AllStart, List<GameObject> AllEnds)
    {
        List<GameObject> bestWay = new List<GameObject>();
        foreach (GameObject i in AllStart)
        {
            foreach (GameObject j in AllEnds)
            {
                List<GameObject> NowWay = new List<GameObject>(BotRunServies.shortPath(i, j,Bot.BotForTheard.ColorP));
                if ((bestWay.Count == 0 || NowWay.Count < bestWay.Count)&&NowWay.Count>1)//&& CheakIfTheRoadEmpty(NowWay))
                {
                    bestWay = NowWay;
                }
            }
        }
        Way = bestWay;
        
        
    }
    public bool CheakIfTheRoadEmpty(List<GameObject> Road)
    {
        int i = 0;
        bool ok = true;
        while (i < Road.Count && ok)
        {
            Points points = Road[i].GetComponent<Points>();
            if (!points.IsEmpty)
                ok = false;
        }
        return ok;

    }

}
