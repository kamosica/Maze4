using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Damage : NetworkBehaviour {

    [SerializeField]
    private float damage=30;
    [SerializeField]
    private float hp = 3;
    [SyncVar]
    private Vector3 nowPos;

    private float time;
	// Use this for initialization
	void Start () {
        nowPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (gameObject.tag == "Trap")
        {
            if (time >= 60)//60秒後に壊れます
            {
                Destroyed();
            }
        }
        if (hp <= 0)
        {
            Destroyed();
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            Status stat = other.gameObject.GetComponent<Status>();
            stat.Damages(damage);
            hp -= 1;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("fire!");
            Status stat = other.gameObject.GetComponent<Status>();
            stat.Damages(damage);
        }
    }
    void Destroyed()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
