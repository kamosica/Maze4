using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

public class hira_shot : NetworkBehaviour {

    public GameObject bullet;
    public GameObject ca;
    [SerializeField]
    private Text remaining_bullets;
    [SerializeField]
    private GameObject Gun;
    private int magagine;
    private int submagine;
    private float interval=0.10f;
    private float time=0;
    private bool reloadflg;
	// Use this for initialization
	void Start () {
        magagine = 30;
        submagine = 90;
        reloadflg = false;
        remaining_bullets = GameObject.Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        remaining_bullets.text = magagine.ToString() + "/" + submagine.ToString();
        time += Time.deltaTime;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //transform.rotation = Quaternion.LookRotation(ray.direction);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
            //gameObject.transform.position=hit.point;
        //}
        keyInput();
	}
    //private IEnumerator shot(string s)
    //{
    //    GameObject obj = Instantiate(bullet,ca.transform.position, ca.transform.rotation);
    //    NetworkServer.Spawn(obj);
    //    yield return new WaitForSeconds(0.1f);
    //}
    void keyInput()
    {
        if (Input.GetKey(KeyCode.Z))
        {
                
        }
        if (Input.GetMouseButton(0))
        {
            //shot("s");
            if (magagine != 0&&reloadflg==false)
            {
                if (time >= interval)
                {
                    Cmdbullet();
                    time = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (magagine != 30)
            {
                reloadflg = true;
                Invoke("Reload", 1);
            }
            
        }
    }
    [Command]
    void Cmdbullet()
    {
        if (magagine > 0)
        {
            GameObject obj = Instantiate(bullet, ca.transform.position, ca.transform.rotation);
            NetworkServer.Spawn(obj);
            magagine--;
        }
    }
    void Reload()
    {
        if (magagine != 30 && magagine+submagine>=30)
        {
            
            int fullmag = 30;
            int soudan = fullmag - magagine;
            submagine = submagine - soudan;
            magagine += soudan;
            reloadflg = false;
        }
        else if (magagine != 30 && magagine+submagine < 30)
        {
            int nowmag = magagine;
            magagine = magagine + submagine;
            submagine = 0;
            reloadflg = false;
        }
    }
}
