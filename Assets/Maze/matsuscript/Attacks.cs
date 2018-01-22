using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour {
    public float attack=34;
    public float Additional_damage;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Status stat = col.gameObject.GetComponent<Status>();
            stat.Damages(attack);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("fire!");
            Status stat = other.gameObject.GetComponent<Status>();
            stat.Damages(attack);
        }
    }
    }
