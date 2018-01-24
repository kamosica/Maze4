using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour {

    Status status_scr;

	// Use this for initialization
	void Start () {
        status_scr = gameObject.GetComponent<Status>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            status_scr.Damages(1.0f);
        }
    }
}
