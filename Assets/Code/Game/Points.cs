using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : Mannger
{
    public int Id;
    public int Place;//עיר מקבלת 2 כפר מקבל 1 גשר 0 כולם -1
    bool Isrobber;
    public bool IsEmpty;//true = ריק False = מלא
    public string PlayerColor;
    public List<GameObject> nextPoint;
    Animator Skin;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        Skin = GetComponent<Animator>();
        Isrobber = false;
        IsEmpty = true;
        coll = GetComponent<Collider2D>();
        PlayerColor = "";
        Place = -1;
    }

    // Update is called once per frame
    void Update()
    {




    }
    public void EnbleToEdit()
    {
        if (IsEmpty)
        {
            //string Change = (PlayerColor == "Blue") ? "IsBlue" : "IsRed";
            //Skin.SetBool(Change, false);
            Skin.SetBool("IsGreen", true);


        }
    }
    public void DisnbleToEdit()
    {
        Skin.SetBool("IsGreen", false);
    }
    public GameObject AddPLace(string color, int place)
    {
        Skin.SetBool("IsGreen", false);
        string Change = (color == "Blue") ? "IsBlue" : "IsRed";
        Skin.SetBool(Change, true);
        IsEmpty = false;
        PlayerColor = color;
        string Changep = (color == "Blue") ? "BlueLevel" : "RedLevel";
        this.Place = place;
        Skin.SetInteger(Changep, Place);
        return this.gameObject;

    }
    public GameObject AddPLace(int place)
    {
        return AddPLace(this.PlayerColor, place);
    }
    public bool IsInEdit
    {
        get { return Skin.GetBool("IsGreen"); }

    }



}
