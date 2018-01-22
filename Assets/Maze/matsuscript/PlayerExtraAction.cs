using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtraAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,3.0f))
            {
                if(hit.collider.tag=="Box")
                {
                    Box boxx = hit.collider.gameObject.GetComponent<Box>();
                    boxx.Open();
                }
            }
        }
    }
}
