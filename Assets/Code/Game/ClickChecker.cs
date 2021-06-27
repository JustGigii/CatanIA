using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
enum Resouresed
{
    Wood ,
    Stone ,
    Clay  ,
    Sheep  ,
    Weat  ,
}
class ClickChecker: MonoBehaviour
{
    public static bool Click(GameObject obj)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.name == obj.name)
            {
                return true;
            }
        }
        return false;
    }
    public static string ClickSend(string Color,string Command)
    {
        if (Input.GetMouseButtonDown(0) && ManngerOnline.NowPlay.ColorP == Color&& !ManngerOnline.Wating)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                return hit.collider.gameObject.name;
            }
        }
        return Command;
    }
    public static bool ClickByName(string objName)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.name == objName)
            {
                return true;
            }
        }
        return false;
    }
    public static void SevenEventRed(Player Red)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "7Event")
            {
                switch (hit.collider.name)
                {
                    case "ClayR":
                        if(Red.GetResoures()[2]!=0)
                        Red.AddClay(-1);
                        break;
                    case "woodR":
                        if (Red.GetResoures()[0] != 0)
                            Red.AddWood(-1);
                        break;
                    case "stoneR":
                        if (Red.GetResoures()[1] != 0)
                            Red.AddStone(-1);
                        break;
                    case "weatR":
                        if (Red.GetResoures()[4] != 0)
                            Red.AddWeet(-1);
                        break;
                    case "sheepR":
                        if (Red.GetResoures()[3] != 0)
                            Red.AddSheep(-1);
                        break;
                }
            }
        }
    }
    public static void SevenEventBlue(Player Blue)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "7Event")
            {
                switch (hit.collider.name)
                {
                    case "Clayb":
                        if (Blue.GetResoures()[2] != 0)
                            Blue.AddClay(-1);
                        break;
                    case "woodb":
                        if (Blue.GetResoures()[0] != 0)
                            Blue.AddWood(-1);
                        break;
                    case "stoneb":
                        if (Blue.GetResoures()[1] != 0)
                            Blue.AddStone(-1);
                        break;
                    case "weatb":
                        if (Blue.GetResoures()[4] != 0)
                            Blue.AddWeet(-1);
                        break;
                    case "sheepb":
                        if (Blue.GetResoures()[3] != 0)
                            Blue.AddSheep(-1);
                        break;

                }
            }
        }
    }
    public static void SevenEventRed(Player Red, string Command)
    {
                switch (Command)
                {
                    case "ClayR":
                        if (Red.GetResoures()[2] != 0)
                            Red.AddClay(-1);
                        break;
                    case "woodR":
                        if (Red.GetResoures()[0] != 0)
                            Red.AddWood(-1);
                        break;
                    case "stoneR":
                        if (Red.GetResoures()[1] != 0)
                            Red.AddStone(-1);
                        break;
                    case "weatR":
                        if (Red.GetResoures()[4] != 0)
                            Red.AddWeet(-1);
                        break;
                    case "sheepR":
                        if (Red.GetResoures()[3] != 0)
                            Red.AddSheep(-1);
                        break;
                }       
        
    }
    public static void SevenEventBlue(Player Blue, string Command)
    {
                switch (Command)
                {
                    case "Clayb":
                        if (Blue.GetResoures()[2] != 0)
                            Blue.AddClay(-1);
                        break;
                    case "woodb":
                        if (Blue.GetResoures()[0] != 0)
                            Blue.AddWood(-1);
                        break;
                    case "stoneb":
                        if (Blue.GetResoures()[1] != 0)
                            Blue.AddStone(-1);
                        break;
                    case "weatb":
                        if (Blue.GetResoures()[4] != 0)
                            Blue.AddWeet(-1);
                        break;
                    case "sheepb":
                        if (Blue.GetResoures()[3] != 0)
                            Blue.AddSheep(-1);
                        break;

                }
            }
    public static void TradeRed(Text[] Resoures, Player Player,bool[] Acssept, ref int Trade)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "Trade")
            {
              
                Acssept[0] = false;
                Acssept[1] = false;
                int[] Resouress = Player.GetResoures();
                switch (hit.collider.name)
                {

                    case "ClayR":
                        if (Resouress[(int)Resouresed.Clay] > int.Parse(Resoures[(int)Resouresed.Clay].text))
                            Resoures[(int)Resouresed.Clay].text = (int.Parse(Resoures[(int)Resouresed.Clay].text) + 1).ToString();
                        Trade--;
                        break;
                    case "woodR":
                        if (Resouress[(int)Resouresed.Wood] > int.Parse(Resoures[(int)Resouresed.Wood].text))
                            Resoures[(int)Resouresed.Wood].text = (int.Parse(Resoures[(int)Resouresed.Wood].text) + 1).ToString();
                        Trade--;
                        break;
                    case "stoneR":
                        if (Resouress[(int)Resouresed.Stone] > int.Parse(Resoures[(int)Resouresed.Stone].text))
                            Resoures[(int)Resouresed.Stone].text = (int.Parse(Resoures[(int)Resouresed.Stone].text) + 1).ToString();
                        Trade--;
                        break;
                    case "weatR":
                        if (Resouress[(int)Resouresed.Weat] > int.Parse(Resoures[(int)Resouresed.Weat].text))
                            Resoures[(int)Resouresed.Weat].text = (int.Parse(Resoures[(int)Resouresed.Weat].text) + 1).ToString();
                        Trade--;
                        break;
                    case "sheepR":
                        if (Resouress[(int)Resouresed.Sheep] > int.Parse(Resoures[(int)Resouresed.Sheep].text))
                            Resoures[(int)Resouresed.Sheep].text = (int.Parse(Resoures[(int)Resouresed.Sheep].text) + 1).ToString();
                        Trade--;
                        break;
                }
            }
        }
    }
    public static void TradeBlue(Text[] Resoures, Player Player, bool[] Acssept, ref int Trade)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "Trade")
            {
                Trade--;
                Acssept[0] = false;
                Acssept[1] = false;
                int[] Resouress = Player.GetResoures();
                switch (hit.collider.name)
                {
                    case "Clayb":
                        if (Resouress[(int)Resouresed.Clay] > int.Parse(Resoures[(int)Resouresed.Clay].text))
                            Resoures[(int)Resouresed.Clay].text = (int.Parse(Resoures[(int)Resouresed.Clay].text) + 1).ToString();
                        break;
                    case "woodb":
                        if (Resouress[(int)Resouresed.Wood] > int.Parse(Resoures[(int)Resouresed.Wood].text))
                            Resoures[(int)Resouresed.Wood].text = (int.Parse(Resoures[(int)Resouresed.Wood].text) + 1).ToString();
                        break;
                    case "stoneb":
                        if (Resouress[(int)Resouresed.Stone] > int.Parse(Resoures[(int)Resouresed.Stone].text))
                            Resoures[(int)Resouresed.Stone].text = (int.Parse(Resoures[(int)Resouresed.Stone].text) + 1).ToString();
                        break;
                    case "weatb":
                        if (Resouress[(int)Resouresed.Weat] > int.Parse(Resoures[(int)Resouresed.Weat].text))
                            Resoures[(int)Resouresed.Weat].text = (int.Parse(Resoures[(int)Resouresed.Weat].text) + 1).ToString();
                        break;
                    case "sheepb":
                        if (Resouress[(int)Resouresed.Sheep] > int.Parse(Resoures[(int)Resouresed.Sheep].text))
                            Resoures[(int)Resouresed.Sheep].text = (int.Parse(Resoures[(int)Resouresed.Sheep].text) + 1).ToString();
                        break;

                }
            }
        }
    }
    public static void TradeRed(Text[] Resoures, Player Player, bool[] Acssept, ref int Trade,string Command)
    {
            if (Command == "ClayR" || Command == "woodR" || Command == "stoneR" || Command == "weatR" || Command == "sheepR")
        {

                Acssept[0] = false;
                Acssept[1] = false;
                int[] Resouress = Player.GetResoures();
                switch (Command)
                {

                    case "ClayR":
                        if (Resouress[(int)Resouresed.Clay] > int.Parse(Resoures[(int)Resouresed.Clay].text))
                            Resoures[(int)Resouresed.Clay].text = (int.Parse(Resoures[(int)Resouresed.Clay].text) + 1).ToString();
                        Trade--;
                        break;
                    case "woodR":
                        if (Resouress[(int)Resouresed.Wood] > int.Parse(Resoures[(int)Resouresed.Wood].text))
                            Resoures[(int)Resouresed.Wood].text = (int.Parse(Resoures[(int)Resouresed.Wood].text) + 1).ToString();
                        Trade--;
                        break;
                    case "stoneR":
                        if (Resouress[(int)Resouresed.Stone] > int.Parse(Resoures[(int)Resouresed.Stone].text))
                            Resoures[(int)Resouresed.Stone].text = (int.Parse(Resoures[(int)Resouresed.Stone].text) + 1).ToString();
                        Trade--;
                        break;
                    case "weatR":
                        if (Resouress[(int)Resouresed.Weat] > int.Parse(Resoures[(int)Resouresed.Weat].text))
                            Resoures[(int)Resouresed.Weat].text = (int.Parse(Resoures[(int)Resouresed.Weat].text) + 1).ToString();
                        Trade--;
                        break;
                    case "sheepR":
                        if (Resouress[(int)Resouresed.Sheep] > int.Parse(Resoures[(int)Resouresed.Sheep].text))
                            Resoures[(int)Resouresed.Sheep].text = (int.Parse(Resoures[(int)Resouresed.Sheep].text) + 1).ToString();
                        Trade--;
                        break;
                }
        }
    }
    public static void TradeBlue(Text[] Resoures, Player Player, bool[] Acssept, ref int Trade,string Command)
    {
            if (Command == "Clayb"|| Command == "woodb" || Command == "stoneb" || Command == "sheepb" || Command == "weatb")
            {
                Trade--;
                Acssept[0] = false;
                Acssept[1] = false;
                int[] Resouress = Player.GetResoures();
                switch (Command)
                {
                    case "Clayb":
                        if (Resouress[(int)Resouresed.Clay] > int.Parse(Resoures[(int)Resouresed.Clay].text))
                            Resoures[(int)Resouresed.Clay].text = (int.Parse(Resoures[(int)Resouresed.Clay].text) + 1).ToString();
                        break;
                    case "woodb":
                        if (Resouress[(int)Resouresed.Wood] > int.Parse(Resoures[(int)Resouresed.Wood].text))
                            Resoures[(int)Resouresed.Wood].text = (int.Parse(Resoures[(int)Resouresed.Wood].text) + 1).ToString();
                        break;
                    case "stoneb":
                        if (Resouress[(int)Resouresed.Stone] > int.Parse(Resoures[(int)Resouresed.Stone].text))
                            Resoures[(int)Resouresed.Stone].text = (int.Parse(Resoures[(int)Resouresed.Stone].text) + 1).ToString();
                        break;
                    case "weatb":
                        if (Resouress[(int)Resouresed.Weat] > int.Parse(Resoures[(int)Resouresed.Weat].text))
                            Resoures[(int)Resouresed.Weat].text = (int.Parse(Resoures[(int)Resouresed.Weat].text) + 1).ToString();
                        break;
                    case "sheepb":
                        if (Resouress[(int)Resouresed.Sheep] > int.Parse(Resoures[(int)Resouresed.Sheep].text))
                            Resoures[(int)Resouresed.Sheep].text = (int.Parse(Resoures[(int)Resouresed.Sheep].text) + 1).ToString();
                        break;

                }
            }
        }
    public static GameObject CheakPoint(GameObject Board)
   {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "point")
            {
                int id = hit.collider.gameObject.GetComponent<Points>().Id;
                GameObject cheak = GraphServies.FindPoints(Board, id);
                Points points = cheak.GetComponent<Points>();
                return (points.IsEmpty && points.IsInEdit) ? cheak : null;
            }
        }
        return null;
    }
    public static GameObject CheakPoint(GameObject Board, string Command)
    {
            if (Command.IndexOf("PointGray")!= -1)
            {
                GameObject cheak = GameObject.Find(Command);
                Points points = cheak.GetComponent<Points>();
                return (points.IsEmpty && points.IsInEdit) ? cheak : null;
            }
        return null;
    }
    public static bool ClickByTag(string objName)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == objName)
            {
                return true;
            }
        }
        return false;
    }
}

