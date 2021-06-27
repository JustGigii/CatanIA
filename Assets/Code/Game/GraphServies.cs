using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class GraphServies
{
    static List<GameObject> Visit;
    public static void EditMode(GameObject green, bool enible)
    {
        Visit = new List<GameObject>();
        if (enible)
            EditModeRe(green);
        else
            EditModedisRe(green);

    }
    public static GameObject FindPoints(GameObject green, int id)
    {
        Visit = new List<GameObject>();
        return FindPointsrec(green, id);
    }
    public static GameObject FindPointsrec(GameObject green, int id)
    {
        Points Point = green.GetComponent<Points>();
        GameObject toreturn = null;
        if (Point.Id == id)
            return green;
        else if (!Isvisit(green))
        {
            Visit.Add(green);
            Point.EnbleToEdit();
            foreach (GameObject i in Point.nextPoint)
            {
                GameObject cheker = FindPointsrec(i, id);
                if (cheker != null)
                    toreturn = cheker;
            }
        }
        return toreturn;
    }
    static void EditModeRe(GameObject green)
    {
        Points Point = green.GetComponent<Points>();

        if (!Isvisit(green) && Point.IsEmpty)
        {
            Visit.Add(green);
            Point.EnbleToEdit();
            foreach (GameObject i in Point.nextPoint)
            {
                EditModeRe(i);
            }
        }
    }

    static void EditModedisRe(GameObject green)
    {
        Points Point = green.GetComponent<Points>();

        if (!Isvisit(green) && Point.IsEmpty)
        {
            Visit.Add(green);
            Point.DisnbleToEdit();
            foreach (GameObject i in Point.nextPoint)
            {
                EditModedisRe(i);
            }
        }
    }

    static bool Isvisit(GameObject ToCheak)
    {
        bool send = false;
        foreach (GameObject i in Visit)
        {
            if (i.name == ToCheak.name)
            {
                send = true;
            }
        }
        return send;
    }

    static public void EnbeleNieber(List<GameObject> gameObject, string color)
    {
        foreach (GameObject i in gameObject)
        {
            Points point = i.GetComponent<Points>();
            if (color == point.PlayerColor)
            {
                foreach (var J in point.nextPoint)
                {
                    Points Nieber = J.GetComponent<Points>();
                    if (Nieber.IsEmpty && Nieber.Place < 0)
                        Nieber.EnbleToEdit();
                }
            }

        }

    }
    static public void DisnbeleNieber(List<GameObject> gameObject)
    {
        foreach (GameObject i in gameObject)
        {
            Points point = i.GetComponent<Points>();

            foreach (var J in point.nextPoint)
            {
                Points Nieber = J.GetComponent<Points>();
                if (Nieber.IsEmpty)
                    Nieber.DisnbleToEdit();
            }

        }

    }

    static public void EnbeleUpgadeUnit(List<GameObject> gameObject, int ToUpGrade)
    {
        foreach (GameObject i in gameObject)
        {
            Points point = i.GetComponent<Points>();
            if (point.Place == ToUpGrade - 1)
            {
                point.IsEmpty = true;
                point.EnbleToEdit();
            }

        }
    }
    static public void DisableUpgadeUnit(List<GameObject> gameObject, int ToUpGrade)
    {
        foreach (GameObject i in gameObject)
        {
            Points point = i.GetComponent<Points>();
            if (point.Place == ToUpGrade - 1)
            {
                point.IsEmpty = false;
                point.DisnbleToEdit();
            }

        }
    }
    public static void AddResorses(List<GameObject> HexaObj, Player Red, Player Blue, int Number)
    {
        foreach (GameObject i in HexaObj)
        {
            Hexagon hexa = i.GetComponent<Hexagon>();
            if (hexa.number == Number)
            {
                foreach (GameObject j in hexa.HexPoints)
                {
                    if (hexa.Isdesert() == false)
                    {
                        Points Point = j.GetComponent<Points>();
                        if (Point.PlayerColor != "")
                        {
                            Player Adding = (Point.PlayerColor == "Red") ? Red : Blue;
                            switch (hexa.Resoures)
                            {
                                case 0:
                                    Adding.AddWood(Point.Place);
                                    break;
                                case 1:
                                    Adding.AddStone(Point.Place);
                                    break;
                                case 2:
                                    Adding.AddClay(Point.Place);
                                    break;
                                case 3:
                                    Adding.AddSheep(Point.Place);
                                    break;
                                case 4:
                                    Adding.AddWeet(Point.Place);
                                    break;
                            }
                        }
                    }
                }
            }
        }

    }
    public static void AddResorsesCounter(List<GameObject> HexaObj, ref int[] Resorse, int Number)
    {
        foreach (GameObject i in HexaObj)
        {
            Hexagon hexa = i.GetComponent<Hexagon>();
            if (hexa.number == Number)
            {
                foreach (GameObject j in hexa.HexPoints)
                {
                    if (hexa.Isdesert() == false)
                    {
                        Points Point = j.GetComponent<Points>();
                        if (Point.PlayerColor == Bot.BotForTheard.ColorP && hexa.Resoures > -1)
                        {
                            Resorse[hexa.Resoures] += Point.Place;
                        }
                    }
                }
            }
        }

    }
    public static bool IfResorsesMach(List<GameObject> HexaObj, GameObject Point, int Resorses)
    {
        if (Resorses == -1)
            return true;
        foreach (GameObject i in HexaObj)
        {
            Hexagon hexa = i.GetComponent<Hexagon>();
            foreach (GameObject j in hexa.HexPoints)
            {
                if (j.name == Point.name && Resorses == hexa.Resoures)
                    return true;
            }
        }
        return false;
    }

    public static void SevenEventEnble(List<GameObject> HexaObj)
    {
        foreach (GameObject i in HexaObj)
        {
            Points Point = i.GetComponent<Points>();
            Point.IsEmpty = true;
            Point.EnbleToEdit();
        }
    }


}

