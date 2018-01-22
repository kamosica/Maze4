﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class trapset : NetworkBehaviour {

    private int Cutterremain;
    private int Needleremain;
    private int Bladeremain;
    private int Spearremain;
    private int Fireremain;
    public GameObject CutterTrap;
    public GameObject NeedleTrap;
    public GameObject BladeTrap;
    public GameObject SpearTrap;
    private GameObject SetPrefab;
    private Vector3 clickPosition;
    [SerializeField]
    private GameObject pa;
    Inventory invent;
    //public Camera ca;
    //Maze_Create Maze_scr;
    //int[,] maze_arrey;
    //int pos_X = 0;
    //int pos_Y = 0;
    Set tset;
    enum Set
    {
        CUTTER,
        NEEDLE,
        BLADE,
        SPEAR,
        None
    }

	// Use this for initialization
	void Start () {
        if (!isServer)
        {
            pa.SetActive(false);
            //gameObject.SetActive(false);
            GetComponent<trapset>().enabled = false;
        }
        Cutterremain = 5;
        Needleremain = 5;
        Bladeremain = 5;
        Spearremain = 5;
        Fireremain = 0;
        tset = Set.None;
        invent = GameObject.Find("Invents").GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () {
        if (invent)
        {
            Cutterremain = invent.Cutters;
            Needleremain = invent.Needles;
            Bladeremain = invent.Blades;
            Spearremain = invent.Spears;
            Fireremain = invent.Fires;
        }
        keyInput();
    }
    void keyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cutter();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Needle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Blade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Spear();
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Vector3でマウスがクリックした位置座標を取得する
        //    clickPosition = Input.mousePosition;
        //    // Z軸修正
        //    clickPosition.z = 19.5f;
        //    // オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
        //    // ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
        //    Instantiate(SetPrefab, Camera.main.ScreenToWorldPoint(clickPosition), SetPrefab.transform.rotation);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
            clickPosition.z = 19.5f;
            clickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
            float cx = Mathf.RoundToInt(clickPosition.x);
            float cy = Mathf.RoundToInt(clickPosition.z);
            clickPosition = new Vector3(cx, 0.5f, cy);
            Debug.Log(clickPosition);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Maze")
                {
                    Setting();
                }
            }
        }
    }
    void Setting()
    {
        switch (tset)
        {
            case Set.CUTTER:
                if (Cutterremain > 0)
                {
                    //Cutterremain -= 1;
                    invent.Cutters -= 1;
                    GameObject obj=Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
                    NetworkServer.Spawn(obj);
                }
                else
                {
                    Debug.Log("在庫がありません");
                }
                break;
            case Set.NEEDLE:
                if (Needleremain > 0)
                {
                    //Needleremain -= 1;
                    invent.Needles -= 1;
                    GameObject obj=Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
                    NetworkServer.Spawn(obj);
                }
                else
                {
                    Debug.Log("在庫がありません");
                }
                break;
            case Set.BLADE:
                if (Bladeremain>0)
                {
                    //Bladeremain -= 1;
                    invent.Blades -= 1;
                    GameObject obj=Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
                    NetworkServer.Spawn(obj);
                }
                else
                {
                    Debug.Log("在庫がありません");
                }
                break;
            case Set.SPEAR:
                if (Spearremain > 0)
                {
                    //Spearremain -= 1;
                    invent.Spears -= 1;
                    clickPosition.y = 1f;
                    GameObject obj=Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
                    NetworkServer.Spawn(obj);
                }
                else
                {
                    Debug.Log("在庫がありません");
                }
                break;
            case Set.None:
                Debug.Log("トラップを選んでください");
                break;
        }
    }
    public void Cutter()
    {
        SetPrefab = CutterTrap;
        tset = Set.CUTTER;
    }
    public void Needle()
    {
        SetPrefab = NeedleTrap;
        tset = Set.NEEDLE;
    }
    public void Blade()
    {
        SetPrefab = BladeTrap;
        tset = Set.BLADE;
    }
    public void Spear()
    {
        SetPrefab = SpearTrap;
        tset = Set.SPEAR;
    }
    public void Supply()
    {
        //Cutterremain += 5;
        //Needleremain += 5;
        //Bladeremain += 5;
        //Spearremain += 5;
        invent.Cutters += 5;
        invent.Needles += 5;
        invent.Blades += 5;
        invent.Spears += 5;
    }
}
