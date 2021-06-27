using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtuon : MonoBehaviour
{
    private Online Python;
    PopulateGrid GridServies;
    dynamic Server;
    
    // Start is called before the first frame update

    // Update is called once per frame
   public void ResetPop()
    {
        Python = GameObject.Find("OnlineObj").GetComponent<Online>();
            Server = Python.ServerProp;
        GridServies = Python.GridView.GetComponent<PopulateGrid>();
        GridServies.PopGrid(Server.PopHost());

        Python.Playing = true;
    }
    public void StartHost()
    {
        Python = GameObject.Find("OnlineObj").GetComponent<Online>();
        Python.StartHost();
        Python.Color = "Blue";
        DontDestroyOnLoad(GameObject.Find("OnlineObj"));
        Application.LoadLevel("1vs1Online");
        Python.Playing = true;
    }
    public void Connect()
    {
        Python = GameObject.Find("OnlineObj").GetComponent<Online>();
        Server = Python.ServerProp;
        Text Button = gameObject.GetComponentInChildren<Text>();
        Python.Connect(Button.text);
        Python.Color = "Red";
        DontDestroyOnLoad(GameObject.Find("OnlineObj"));
        Application.LoadLevel("1vs1Online");
        Python.Playing = true;
    }
}
