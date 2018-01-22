using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bulletmove : NetworkBehaviour {
    float speed = 500;
    public Rigidbody rb;
    public int Damage=33;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        rb.AddForce(transform.forward * speed);
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
