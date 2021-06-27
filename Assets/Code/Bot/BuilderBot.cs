using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
enum Build
{
    Road = 0,
    Settlement = 1,
    City = 2
}
class BuilderBot : MonoBehaviour
{
    List<int[]> Options;
    Thread AllOps;

    public BuilderBot()
    {
        Options = new List<int[]>();
        AllOps = new Thread(AllOptionsRun);
        AllOps.Start();
    }
    public void PrintAllOptions()
    {
        foreach (var i in Options)
        {
            print("Road = " + i[(int)Build.Road] + " Settlement= " + i[(int)Build.Settlement] + " City= " + i[(int)Build.City]);
        }
        print("____________________________________");
    }
    List<int[]> AllOption(Player Bot, List<int[]> Options)
    {
        if (!CanAffordAll(Bot))
        {
            Options.Add(new int[3] { 0, 0, 0 });
            return Options;
        }
        else
        {

            if (Bot.CanBuy("Road"))
            {
                Bot.SubResoures(1, 0, 1, 0, 0);
                Options = AllOption(Bot, Options);

                Options[Options.Count - 1][(int)(Build.Road)]++;
                Bot.AddResoures(1, 0, 1, 0, 0);
            }
            if (Bot.CanBuy("Settlement"))
            {
                Bot.SubResoures(1, 0, 1, 1, 1);
                Options = AllOption(Bot, Options);

                Bot.AddResoures(1, 0, 1, 1, 1);
                Options[Options.Count - 1][(int)(Build.Settlement)]++;
            }
            if (Bot.CanBuy("City"))
            {
                Bot.SubResoures(0, 3, 0, 0, 2);
                Options = AllOption(Bot, Options);

                Bot.AddResoures(0, 3, 0, 0, 2);
                Options[Options.Count - 1][(int)(Build.City)]++;
            }
            return Options;
        }
    }
   static List<int[]> AllOptionPublic(Player Bot, List<int[]> Options)
    {
        if (!CanAffordAll(Bot))
        {
            Options.Add(new int[3] { 0, 0, 0 });
            return Options;
        }
        else
        {

            if (Bot.CanBuy("Road"))
            {
                Bot.SubResoures(1, 0, 1, 0, 0);
                Options = AllOptionPublic(Bot, Options);

                Options[Options.Count - 1][(int)(Build.Road)]++;
                Bot.AddResoures(1, 0, 1, 0, 0);
            }
            if (Bot.CanBuy("Settlement"))
            {
                Bot.SubResoures(1, 0, 1, 1, 1);
                Options = AllOptionPublic(Bot, Options);

                Bot.AddResoures(1, 0, 1, 1, 1);
                Options[Options.Count - 1][(int)(Build.Settlement)]++;
            }
            if (Bot.CanBuy("City"))
            {
                Bot.SubResoures(0, 3, 0, 0, 2);
                Options = AllOptionPublic(Bot, Options);

                Bot.AddResoures(0, 3, 0, 0, 2);
                Options[Options.Count - 1][(int)(Build.City)]++;
            }
            return Options;
        }
    }
   static bool CanAffordAll(Player Bot)
    {
        return Bot.CanBuy("Road") || Bot.CanBuy("Settlement") || Bot.CanBuy("City");
    }
    public void BuildStrategy(Player BotP, List<GameObject> Hex, int ScoreDiff, ref List<GameObject> Way, ref GameObject Start, ref GameObject End)
    {
        Options.Clear();
        Thread.Sleep(100);
        if (Options.Count > 0)
        {
            if (ScoreDiff < 2 && Way.Count > 0)
            {

                int Road = 0;

                foreach (int[] i in Options)
                {
                    if (Road < i[(int)Build.Road])
                        Road = i[(int)Build.Road];
                }
                BuildRoad(BotP, ref Way, Road, ref Start, ref End);
            }
            else
            {
                int Settlement = 0, City = 0, Road = 0;
                foreach (int[] i in Options)
                {
                    if ((City + Settlement) < (i[(int)Build.City] + i[(int)Build.City]))
                        Road = i[(int)Build.Road];
                    Settlement = i[(int)Build.Settlement];
                    City = i[(int)Build.City];
                }
                BuildRoad(BotP, ref Way, Road, ref Start, ref End);
                int[] Coppy = new int[5];
                int Find;
                for (int back = 0; back < Coppy.Length+1; back++)
                {
                    Array.Copy(Bot.Conter, Coppy, Bot.Conter.Length);
                    if (back < Coppy.Length)
                        Find = Select.QuickSelectPartition(Coppy, 0, Coppy.Length - 1, back);
                    else
                        Find = - 1;
                    foreach (GameObject i in BotP.MyPlace)
                    {
                        if (GraphServies.IfResorsesMach(Hex, i, Find))
                        {
                            Points This = i.GetComponent<Points>();
                            if (This.Place == 0 && Settlement > 0)
                            {

                                BotP.SubResoures(1, 0, 1, 1, 1);
                                BotP.UpdatePoint(i, 1);
                                Settlement--;
                            }
                            if (This.Place == 1 && City > 0)
                            {
                                if (City > 0)
                                {
                                    BotP.SubResoures(0, 3, 0, 0, 2);
                                    BotP.UpdatePoint(i, 2);
                                    City--;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public static bool MakeTrade(Player botNow, Player botAfter)
    {
        List<int[]> now = new List<int[]>();
        AllOptionPublic(botNow, now);
        List<int[]> after = new List<int[]>();
        AllOptionPublic(botAfter, after);
        if (AllCanBuy(after) > AllCanBuy(now))
            return true;
        if(AllCanBuy(after) == AllCanBuy(now)&& AllResueses(botAfter.GetResoures()) > AllResueses(botNow.GetResoures()))
        return true;

        return false;
    }
    public static int AllResueses(int[] Resurse)
    {
        int sum = 0;
        foreach (var item in Resurse)
        {
            sum += item;
        }
        return sum;
    }
    public static int AllCanBuy(List<int[]> all)
    {
        int sum = 0;
        foreach (var item in all )
        {
            foreach (var j in item)
            {
                sum += j;
            }   
        }
        return sum;
    }
    public void DropResources(Player Bot)
    {
        List<int[]> Option = new List<int[]>();
        while (Bot.GetResouresCount() > 7)
        {
            int index = 0;
            int count = 0;
            for (int i = 0; i < Bot.GetResoures().Length; i++)
            {
                Bot.SubResouresById(i, 1);
                Option = AllOption(Bot, Option);
                Bot.AddResouresById(i, 1);
                if (count < Option.Count && Bot.GetResoures()[i] > 1)
                {
                    index = i;
                    count = Option.Count;
                }
            }
            Bot.SubResouresById(index, 1);
        }
    }
    void AllOptionsRun()
    {
        while (true)
        {
            Options.Clear();
            Player NewBot = new Player(Bot.BotForTheard);
            Options = new List<int[]>(AllOption(NewBot, Options));
            Thread.Sleep(10);
        }
    }
    void BuildRoad(Player Bot, ref List<GameObject> Way, int Count, ref GameObject Start, ref GameObject End)
    {
        bool Swich = true;
        for (int i = 0; i < Count && Way.Count > 0; i++)
        {
            Bot.SubResoures(1, 0, 1, 0, 0);
            if (Swich)
            {

                Bot.AddPoint(Way[0], 0);
                Start = Way[0];
                Way.RemoveAt(0);
                if (End != null)
                    Swich = false;
            }
            else
            {
                Bot.AddPoint(Way[Way.Count - 1], 1);
                End = Way[Way.Count - 1];
                Way.RemoveAt(Way.Count - 1);
                Swich = true;
            }
        }
    }
}
