using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player
{
    string color;
    int score;
    int[] Resoures;//משאבי משחק 0-עץ 1-אבן 2-חמר 3-כבשים 4-חיטה
    List<GameObject> myPlace;
    static public object PointReceve;

    public Player(string color)
    {
        ColorP = color;
        this.score = 0;
        this.Resoures = new int[] { 0, 0, 0, 0, 0 };
        this.myPlace = new List<GameObject>();
        PointReceve = null;
    }
    public Player(Player pl)
    {
        ColorP = pl.ColorP;
        this.score = pl.Score;
        this.Resoures = new int[5];
        Array.Copy(pl.Resoures, this.Resoures, 5);
        this.myPlace =(pl.myPlace);
    }
    public string ColorP
    {

        set { this.color = value; }
        get { return this.color; }
    }
    public int Score
    {
        get { return this.score; }
    }
    public void AddResoures(int wood, int Stone, int clay, int sheep, int Weet)
    {
        this.Resoures[0] += wood;
        this.Resoures[1] += Stone;
        this.Resoures[2] += clay;
        this.Resoures[3] += sheep;
        this.Resoures[4] += Weet;
    }
    public void AddResouresById(int index, int Quantity)
    {
        this.Resoures[index] += Quantity;
    }
    public void AddWood(int wood)
    {
        AddResoures(wood, 0, 0, 0, 0);
    }
    public void AddStone(int Stone)
    {
        AddResoures(0, Stone, 0, 0, 0);
    }
    public void AddClay(int Clay)
    {
        AddResoures(0, 0, Clay, 0, 0);
    }
    public void AddSheep(int sheep)
    {
        AddResoures(0, 0, 0, sheep, 0);
    }
    public void AddWeet(int Weet)
    {
        AddResoures(0, 0, 0, 0, Weet);
    }
    public void SubResoures(int wood, int Stone, int clay, int sheep, int Weet)
    {
        AddResoures(wood * -1, Stone * -1, clay * -1, sheep * -1, Weet * -1);
    }
    public void SubResouresById(int index, int Quantity)
    {
        AddResouresById(index, -Quantity);
    }
   public int[] GetResoures()
    {
        return Resoures;
    }
    public int GetResouresCount()
    {
        int sum = 0;
        for (int i = 0; i < this.Resoures.Length; i++)
        {
            sum += this.Resoures[i];
        }
        return sum;
    }
    public bool AddScore(int Score)
    {//טענת כניסה: מקבלת כמות נקודות
     //טענת יצאה: מחזיר True עם כמות הנקודות גובה מ10 אחרת  false
        this.score += Score;
        if (this.score >= 10)
            return true;
        return false;
    }
    public void AddPoint(GameObject pointobj, int place)
    {
        Points Points = pointobj.GetComponent<Points>();
        Points.IsEmpty = false;
        Points.AddPLace(this.color, place);
        this.myPlace.Add(pointobj);
        AddScore(place);
    }

    public void UpdatePoint(GameObject pointobj, int place)
    {
        Points Points = pointobj.GetComponent<Points>();
        Points.IsEmpty = false;
        score -= Points.Place;
        score += place;
        Points.AddPLace(this.color, place);

    }
    public List<GameObject> MyPlace
    {
        get { return this.myPlace; }
    }
    public int Count
    {
        get { return myPlace.Count; }
    }

    public bool CanBuy(string Buy)
    {
        bool send = false;
        switch (Buy)
        {
            case "Road":
                send = Resoures[0] >= 1 && Resoures[2] >= 1;
                break;
            case "Settlement":
                send = Resoures[0] >= 1 && Resoures[2] >= 1 && Resoures[3] >= 1 && Resoures[4] >= 1;
                break;
            case "City":
                send = Resoures[1] >= 3 && Resoures[4] >= 2;
                break;
        }
        return send;
    }
    public int NumberOfResoures()
    {
        int sum = 0;
        foreach (int i in this.Resoures)
        {
            sum += i;
        }
        return sum;
    }
}
