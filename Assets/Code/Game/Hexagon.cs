using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
   public List<GameObject> HexPoints;
    public int number;
    public int Resoures;//משאבי משחק 0-עץ 1-אבן 2-חמר 3-כבשים 4-חיטה
    bool IsDesert;

    // Start is called before the first frame update
    void Start()
    {
        IsDesert = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeDesert()
    {
        this.IsDesert = true;
    }
    public void DISABLEMakeDesert()
    {
        this.IsDesert = false;
    }
    public bool Isdesert()
    {
        return this.IsDesert;
    }
}
