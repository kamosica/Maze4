using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    Animator ani;
	// Use this for initialization
	void Start () {
        ani=gameObject.GetComponent<Animator>();
        ani.SetBool("opened", false);
    }
	
	// Update is called once per frame
	void Update () {

	}
    public void Open()
    {
        ani.SetBool("opened", true);
    }
    public void close()
    {
        ani.SetBool("opened", false);
    }
}
