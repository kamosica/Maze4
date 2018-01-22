using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtraAction : MonoBehaviour {
    Vector3 pos;
    Camera cam;
    Inventory invent;
    // Use this for initialization
    void Start () {
        cam = gameObject.GetComponent<Camera>();
        pos = Input.mousePosition;
        invent = GameObject.Find("Invents").GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update () {
        keyinput();
	}
    void keyinput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,3.0f))
            {
                //Debug.Log("shoot");
                if(hit.collider.tag=="Box")
                {
                    Debug.Log("treasurebox");
                    Box boxx = hit.collider.gameObject.GetComponent<Box>();
                    if (boxx.flg == false)
                    {
                        boxx.Open();
                        invent.Supply();
                    }
                }
            }
        }
    }
}
