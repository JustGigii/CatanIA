using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopulateGrid : MonoBehaviour
{
    public GameObject Prefub;
    GameObject[] Bot;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Bot = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ClearList()
    {
        if (Bot != null)
        {
            for (int i = 0; i < Bot.Length; i++)
            {
                Destroy(Bot[i]);
            }
            Bot = null;
        }
    }
    public void PopGrid(string set)
    {
        if (set != "Null")
        {
            string[] Table = set.Split('@');
            ClearList();
            Bot = new GameObject[Table.Length];
            for (int i = 0; i < Table.Length; i++)
            {
                Bot[i] = Instantiate(Prefub, transform);
                Text TextBox = Bot[i].GetComponentInChildren<Text>();
                TextBox.text = Table[i];
            }
        }
        
    }
}
    // return self.Server_socket.recv(1024)