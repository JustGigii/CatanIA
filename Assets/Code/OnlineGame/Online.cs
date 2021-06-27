using System.Collections;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class Online : MonoBehaviour
{
    public GameObject GridView;
    private ScriptScope scope;
    private ScriptEngine engine;
    PopulateGrid GridServies;
    dynamic Server;
    public bool Playing;
    public string Color;
    public bool IsHost;
    // Start is called before the first frame update
    void Start()
    {
        PythonConnection();
        PopulateGrid GridServies = GridView.GetComponent<PopulateGrid>();
        GridServies.PopGrid(Server.PopHost());
        Playing = false;
        IsHost = false;

    }

    // Update is called once per frame
    void Update()
    {
        // print(Server.PopHost());
        if (Playing)
        {
            ManngerOnline.Wating = false;
            if(IsHost)
            {
                ManngerOnline.Command = ClickChecker.ClickSend(Color,ManngerOnline.Command);
                Server.Hostsend(ManngerOnline.Command);
            }
            else
            Server.SendMEss(ClickChecker.ClickSend(Color, ManngerOnline.Command));
         
        }

    }
    void PythonConnection()
    {
        engine = UnityPython.CreateEngine();
        scope = engine.CreateScope();
        var paths = engine.GetSearchPaths();
        paths.Add(Application.dataPath + "/Python/Lib");
        engine.SetSearchPaths(paths);
        dynamic source = engine.ExecuteFile(Application.dataPath + @"\Code\OnlineGame\HostClient.py");
        Server = source.Server();
        engine.ImportModule("ctypes");
    }
    void Connect(Text Text)
    {

    }
    public void StartHost()
    {
        Server.BeHost();
        IsHost = true;
         new Thread(addPlayer).Start();
    }
    void addPlayer()
    {
        while (true)
        {
         dynamic Client = Server.AddPlayer();
         new Thread(Hosting).Start(Client);
        }
    }
    void Hosting( dynamic Host)
    {
        while (true)
        {
         ManngerOnline.Command = Server.Host(Host);
        }
    }
   public void Connect(string Text)
    {
        Server.Connection(Text);
        new Thread(Recv).Start();
    }
    void Recv()
    {
        while (true)
        {
           ManngerOnline.Command =  Server.RecvFormServer();
        }
       
    }
    public dynamic ServerProp
    {
        get { return (Server != null) ? Server : null; }
    }
    
   
}
