using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Door_Collider : NetworkBehaviour
{

    public GameObject Angou_Menu;
    public GameObject Mondai_prefab;

    public GameObject Mondai_obj;

    [SyncVar]
    float PosY = 1300.0f;


    // Use this for initialization
    void Start () {
        Angou_Menu = GameObject.FindGameObjectWithTag("AngouCanvas");
        CmdCreateMondai();
        Mondai_obj = GameObject.FindGameObjectWithTag("Mondai");
    }
	
	// Update is called once per frame
	void Update () {
        //Mondai_obj.transform.localPosition = new Vector3(0.0f, PosY, 0.0f);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("ANGOU");
                Cmdsetactive(130.0f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            Cmdsetactive(1300.0f);
        }
    }

    [Command]
    void CmdCreateMondai()
    {
        //Debug.Log("Mondai");
        GameObject obj = (GameObject)Instantiate(Mondai_prefab, new Vector3(0.0f,0.0f,0.0f), new Quaternion(0.0f,0.0f,0.0f,0.0f));
        obj.transform.parent = Angou_Menu.transform;
        obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        obj.transform.localPosition = new Vector3(0.0f, PosY, 0.0f);

        NetworkServer.Spawn(obj);
    }

    [Command]
    void Cmdsetactive(float y)
    {
        PosY = y;
        Mondai_obj.transform.localPosition = new Vector3(0.0f, PosY, 0.0f);

        //Debug.Log("Cmdset");
    }
}
