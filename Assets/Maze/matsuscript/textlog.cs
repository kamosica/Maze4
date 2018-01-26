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
}
