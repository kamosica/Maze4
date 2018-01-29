using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class textlog : MonoBehaviour {
    Text maltilog;
    string[] message;
	// Use this for initialization
	void Start () {
        maltilog.text = "";
        Messagelog();
	}
	
	// Update is called once per frame
	void Update () {
        log();
	}
    void Messagelog()
    {
        message = new string[]
        {
            "設置するトラップを選択してください",
            "設置するトラップが選択されていません",
            "選択されたトラップの在庫がありません",
            ""
        };
    }
    void log()
    {
        maltilog.text = message[0];
    }
    public void notification(int s)
    {
        switch (s)
        {
            case 1:
                maltilog.text = message[0];
                break;
            case 2:
                maltilog.text = message[1];
                break;
            case 3:
                maltilog.text = message[2];
                break;
        }
    }
}
