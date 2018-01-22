using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour {
    public float HealingHP;
	// Use this for initialization
	void Start () {
        HealingHP = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Status stat = col.gameObject.GetComponent<Status>();
            stat.Healing(HealingHP);
        }
    }
}
