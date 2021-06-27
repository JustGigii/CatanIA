using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class BotRunServies : MonoBehaviour
{
    static List<GameObject> Visit;

    public static List<GameObject> GetPoints(List<int> index, GameObject Borad, List<GameObject> HexaList)
    {
        Visit = new List<GameObject>();
        List<GameObject> PointsHere = new List<GameObject>();
        GameObject found = GetPointRe(index, Borad, HexaList, PointsHere);
        while (found)
        {
            PointsHere.Add(found);
            Visit.Clear();
            found = GetPointRe(index, Borad, HexaList, PointsHere);
        }
        return PointsHere;
    }
    static GameObject GetPointRe(List<int> index, GameObject Borad, List<GameObject> HexaList, List<GameObject> here)
    {
        Points Point = Borad.GetComponent<Points>();

        GameObject toreturn = null;
        if (IsIdeal(Point, index, HexaList, here))
        {
            return Borad;
        }
        else if (!Isvisit(Borad))
        {

            Visit.Add(Borad);
            Point.EnbleToEdit();
            foreach (GameObject i in Point.nextPoint)
            {
                GameObject cheker = GetPointRe(index, i, HexaList, here);
                if (cheker != null)
                    return cheker;
            }
        }
        return toreturn;
    }
    static bool IsIdeal(Points Pointess, List<int> index, List<GameObject> HexaList, List<GameObject> here)
    {
        var CheakList = new List<int>(index);
        foreach (GameObject j in here)
        {
            if (j.name == Pointess.name)
                return false;
        }
        foreach (GameObject i in HexaList)
        {
            Hexagon hexa = i.GetComponent<Hexagon>();
            {
                foreach (GameObject j in hexa.HexPoints)
                {
                    Points Point = j.GetComponent<Points>();
                    if (Point.Id == Pointess.Id)
                    {
                        for (int n = 0; n < CheakList.Count; n++)
                        {
                            if (CheakList[n] == hexa.Resoures)
                                CheakList.RemoveAt(n);
                        }
                    }
                }
            }
        }
        return CheakList.Count == 0 ? true : false;
    }
    static bool Isvisit(GameObject ToCheak)
    {
        foreach (GameObject i in Visit)
        {
            if (i.name == ToCheak.name)
            {
                return true;
            }
        }
        return false;
    }
    static Dictionary<GameObject, GameObject> BFS(Dictionary<GameObject, GameObject> previous, GameObject start)
    {
        Queue<GameObject> queue = new Queue<GameObject>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {

            GameObject vertex = queue.Dequeue();
            Points Point = vertex.GetComponent<Points>();
            if (Point.Id == 999 || Point.Id == 998 && !Point.IsEmpty)
                continue;
            foreach (GameObject neighbor in Point.nextPoint)
            {
                Points point = neighbor.GetComponent<Points>();
                if (previous.ContainsKey(neighbor))
                    continue;
                previous[neighbor] = vertex;
                queue.Enqueue(neighbor);
            }

        }

        return previous;
    }
    public static List<GameObject> shortPath(GameObject start, GameObject end,string botColor)
    {
        bool ok= true;
        if(!start.GetComponent<Points>().IsEmpty || !end.GetComponent<Points>().IsEmpty)
        if (start.GetComponent<Points>().PlayerColor != botColor )
            return new List<GameObject>();
        Dictionary<GameObject, GameObject> previous = new Dictionary<GameObject, GameObject>();
        previous = BFS(previous, start);
        List<GameObject> path = new List<GameObject>();
        GameObject now = end;
        while (now.name != start.name)
        {
            ok = false;
            Points point = now.GetComponent<Points>();
            if (point.Id == 999 || point.Id == 998 ||(point.PlayerColor != botColor && point.PlayerColor != ""))
                break;

            path.Add(now);
            now = previous[now];
            ok = true;
        }
        if (ok)
        {
            path.Add(start);
            path.Reverse();
            return path;
        }
        else
            return new List<GameObject>();
    }
    public static List<GameObject> Koter(GameObject start)
    {
        Visit = new List<GameObject>();
        Queue<GameObject> queue = new Queue<GameObject>();
        List<GameObject> bestWay = new List<GameObject>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            GameObject vertex = queue.Dequeue();
            Visit.Add(vertex);
            Points Point = vertex.GetComponent<Points>();
            if ((Point.Id == 999 || Point.Id == 998) && !Point.IsEmpty && !Isvisit(vertex))
                continue;
            List<GameObject> NowWay = new List<GameObject>(BotRunServies.shortPath(start, vertex,Bot.BotForTheard.ColorP));
            if (bestWay.Count == 0 || NowWay.Count > bestWay.Count)
            {
                bestWay = NowWay;
            }
            foreach (GameObject neighbor in Point.nextPoint)
            {
                Points point = neighbor.GetComponent<Points>();
                queue.Enqueue(neighbor);
            }

        }
        return bestWay;
    }
}