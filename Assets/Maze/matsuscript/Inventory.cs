using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {

    public int Cutters;
    public int Needles;
    public int Blades;
    public int Spears;
    public int Fires;
    // Use this for initialization
    void Start () {
        Cutters = 5;
        Needles = 5;
        Blades = 5;
        Spears = 5;
        Fires = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Supply()
    {
        Cutters += 5;
        Needles += 5;
        Blades += 5;
        Spears += 5;
        Fires += 1;
    }
}
