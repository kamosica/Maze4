using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovetoCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        KeyInput();
	}
    void KeyInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (gameObject.transform.position.z<21&&gameObject.transform.position.z >= 0)
            {
                gameObject.transform.position += new Vector3(0, 0, 0.5f);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (gameObject.transform.position.x >6 && gameObject.transform.position.x <= 30){
                gameObject.transform.position += new Vector3(-0.5f, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (gameObject.transform.position.z <= 21 && gameObject.transform.position.z > 0)
            {
                gameObject.transform.position += new Vector3(0, 0, -0.5f);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (gameObject.transform.position.x >= 6 && gameObject.transform.position.x < 30)
            {
                gameObject.transform.position += new Vector3(0.5f, 0, 0);
            }
        }
        float val=Input.GetAxis("Mouse ScrollWheel");
        if (val>0.0f)
        {
            if (Camera.main.orthographicSize >= 5)
            {
                Camera.main.orthographicSize -= 1;
            }
        }
        else if (val < 0.0f)
        {
            if (12>= Camera.main.orthographicSize&& Camera.main.orthographicSize >= 4)
            {
                Camera.main.orthographicSize += 1;
            }
        }
    }
}
