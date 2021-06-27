using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Mannger : MonoBehaviour
{
    static public int TurnCounter;
    public GameObject Board;
    public List<GameObject> HexaList;
    public GameObject[] Turn;
    public Text Winner;
    public Text[] RedUI;
    public Text[] BlueUI;
    public Text[] Cube;
    Player blue;
    Player red;
    Player playing;
    float y;
    GameObject Next;
    int Place;
    public int NumberOnCube;
    static public Player NowPlay;
    Bot GameBot;
    public bool NowBot;

    // Start is called before the first frame update
    void Start()
    {
        TurnCounter = 0;
        blue = new Player("Blue");
        red = new Player("Red");
        playing = blue;
        Turn[0].active = true;
        Turn[1].active = false;
        Place = -1;
        Next = GameObject.Find("Build");
        Next.active = false;
        int NumberOnCube = 0;
        NowBot = false;
    }

    // Update is called once per frame
    void Update()
    {
        NowPlay = playing;
        ShowResoures();
        if (!NowBot)
        {
            if (playing.Count <= 1)
            {

                GraphServies.EditMode(Board, true);
                Place = 1;
                GameObject Add = ClickChecker.CheakPoint(Board);
                if (Add != null)
                {
                    playing.AddPoint(Add, Place);
                    GraphServies.EditMode(Board, false);
                }
                Place = 0;
            }
            else
            {
                if (playing.Count == 3)
                    Place = 0;
                if (NumberOnCube == 7)
                {

                    if (red.NumberOfResoures() > 7 || blue.NumberOfResoures() > 7)
                    {
                        ClickChecker.SevenEventRed(red);
                        ClickChecker.SevenEventBlue(blue);
                    }
                    if (red.NumberOfResoures() <= 7 && blue.NumberOfResoures() <= 7)
                    {
                        NumberOnCube = -1;
                    }
                }
                else
                {
                    if (NumberOnCube == -1)
                    { Seven(); }
                    else
                    {
                        CheakAndAddPoints();
                        AddPoints();
                        ChancePlayer();
                    }
                }
            }
        }
    }

    public void ChancePlayer()
    {
        if (ClickChecker.Click(this.gameObject) || NowBot)
        {
            if (playing.Score >= 10)
            {
                Next.active = false;
                NowBot = false;
                Winner.text = "The Winner is " + playing.ColorP;
            }
            else
            {
                Next.active = false;
                Next.transform.position = new Vector3(Next.transform.position.x, 4.49f, Next.transform.position.z);
                Place = -1;
                playing = (playing.ColorP == blue.ColorP) ? red : blue;
                Turn[0].active = (Turn[0].active) ? false : true;
                Turn[1].active = (Turn[1].active) ? false : true;
                if (TurnCounter >= 1)
                {
                    Cube[0].text = Random.Range(1, 7).ToString();
                    Cube[1].text = Random.Range(1, 7).ToString();
                    NumberOnCube = int.Parse(Cube[1].text) + int.Parse(Cube[0].text);
                    GraphServies.AddResorses(HexaList, red, blue, NumberOnCube);
                    if(Bot.BotForTheard == null)
                    GraphServies.AddResorsesCounter(HexaList, ref Bot.Conter, NumberOnCube);
                }
                TurnCounter++;
            }
        }
    }
    public void CheakAndAddPoints()
    {

        if (ClickChecker.ClickByName("Road") && playing.CanBuy("Road") && TurnCounter >= 1)
        {
            Next.active = true;
            Place = 0;
            Next.transform.position = new Vector3(Next.transform.position.x, 3.7f, Next.transform.position.z);
        }
        else if (ClickChecker.ClickByName("Settlement") && playing.CanBuy("Settlement") && TurnCounter >= 1)
        {
            Next.active = true;
            Place = 1;
            Next.transform.position = new Vector3(Next.transform.position.x, 3.0f, Next.transform.position.z);
        }
        else if (ClickChecker.ClickByName("City") && playing.CanBuy("City") && TurnCounter >= 1)
        {
            Next.active = true;
            Place = 2;
            Next.transform.position = new Vector3(Next.transform.position.x, 2.5f, Next.transform.position.z);
        }
    }
    public void AddPoints()
    {
        GraphServies.EditMode(Board, false);
        if (Place != -1)
        {
            if (Place == 0)
            {
                GraphServies.EditMode(Board, false);
                GraphServies.EnbeleNieber(playing.MyPlace, playing.ColorP);

                GameObject Add = ClickChecker.CheakPoint(Board);
                if (Add != null)
                {
                    playing.AddPoint(Add, Place);
                    GraphServies.DisnbeleNieber(playing.MyPlace);
                    if (TurnCounter >= 2)
                        playing.SubResoures(1, 0, 1, 0, 0);
                    Place = -1;
                }
            }
            if (Place == 1 || Place == 2)
            {
                GraphServies.EditMode(Board, false);
                GraphServies.EnbeleUpgadeUnit(playing.MyPlace, Place);
                GameObject Add = ClickChecker.CheakPoint(Board);
                if (Add != null)
                {
                    playing.AddScore(Place);

                    playing.UpdatePoint(Add, Place);
                    GraphServies.DisableUpgadeUnit(playing.MyPlace, Place);
                    if (Place == 1 && TurnCounter >= 1)
                        playing.SubResoures(1, 0, 1, 1, 1);
                    else if (TurnCounter >= 2)
                        playing.SubResoures(0, 3, 0, 0, 2);
                    Place = -1;
                }
            }
        }
    }
    public void Seven()
    {
        GraphServies.SevenEventEnble(HexaList);
        GameObject Add = ClickChecker.CheakPoint(Board);
        if (Add != null)
        {
            Hexagon hex = Add.GetComponent<Hexagon>();
            hex.MakeDesert();
            NumberOnCube = -2;
        }
    }

    void ShowResoures()
    {
        int[] RedResoures = new int[5];
        RedResoures = red.GetResoures();
        RedUI[0].text = RedResoures[0].ToString();
        RedUI[1].text = RedResoures[1].ToString();
        RedUI[2].text = RedResoures[2].ToString();
        RedUI[3].text = RedResoures[3].ToString();
        RedUI[4].text = RedResoures[4].ToString();
        RedUI[5].text = red.Score.ToString();
        int[] BlueResoures = new int[5];
        BlueResoures = blue.GetResoures();
        BlueUI[0].text = BlueResoures[0].ToString();
        BlueUI[1].text = BlueResoures[1].ToString();
        BlueUI[2].text = BlueResoures[2].ToString();
        BlueUI[3].text = BlueResoures[3].ToString();
        BlueUI[4].text = BlueResoures[4].ToString();
        BlueUI[5].text = blue.Score.ToString();
    }
    public Player Red
    {
        get { return this.red; }
    }
    public Player Blue
    {
        get { return this.blue; }
    }
    public Player Playing
    {
        get { return this.playing; }
    }
}
