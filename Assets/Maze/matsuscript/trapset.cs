using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public GameObject FireTrap;
    private GameObject SetPrefab;
    private Vector3 clickPosition;
    [SerializeField]
    private GameObject pa;
    public Text textcu;
    public Text textne;
    public Text textbl;
    public Text textfi;
    Inventory invent;
    //public Camera ca;
    //Maze_Create Maze_scr;
    //int[,] maze_arrey;
    //int pos_X = 0;
    //int pos_Y = 0;
    Set tset;

    public AudioClip audioClip1;

    private AudioSource audioSource;

    private Vector3 position;
    private Vector3 screenToWorldPointPosition;
    public GameObject pins;

    enum Set
    {
        CUTTER,
        NEEDLE,
        BLADE,
        SPEAR,
        FIRE,
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
        Fireremain = 2;
        tset = Set.None;
        invent = GameObject.Find("Invents").GetComponent<Inventory>();
        textcu.text = invent.Cutters+"";
        textne.text = invent.Needles + "";
        textbl.text = invent.Blades + "";
        textfi.text = invent.Fires + "";
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
            //Spear();
            Fire();
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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //return;

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

                audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.clip = audioClip1;
                audioSource.Play();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            position = Input.mousePosition;
            // Z軸修正
            position.z = 10f;
            // マウス位置座標をスクリーン座標からワールド座標に変換する
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Pin")
                {
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    GameObject pin = Instantiate(pins, screenToWorldPointPosition, pins.transform.rotation);
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
                    textcu.text = invent.Cutters + "";
                    GameObject obj= (GameObject)Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
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
                    textne.text = invent.Needles + "";
                    GameObject obj= (GameObject)Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
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
                    textbl.text = invent.Blades + "";
                    GameObject obj= (GameObject)Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
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
                    GameObject obj= (GameObject)Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
                    NetworkServer.Spawn(obj);
                }
                else
                {
                    Debug.Log("在庫がありません");
                }
                break;
            case Set.FIRE:
                if (Fireremain > 0)
                {
                    invent.Fires -= 1;
                    textfi.text = invent.Fires + "";
                    GameObject obj = (GameObject)Instantiate(SetPrefab, clickPosition, SetPrefab.transform.rotation);
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
    public void Fire()
    {
        SetPrefab = FireTrap;
        tset = Set.FIRE;
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
