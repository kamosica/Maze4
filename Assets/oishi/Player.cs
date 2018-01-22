using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    public GameObject Sphere;

    public GameObject Camera1;
    public GameObject Camera2;

    int PlayerID;

    void Start()
    {
        Camera1 = GameObject.FindGameObjectWithTag("MainCamera");
        Camera2 = GameObject.FindGameObjectWithTag("Camera");

        if (isLocalPlayer == true)
        {
            PlayerID = 1;
            Camera1.GetComponent<Camera>().depth = 1;
            Camera2.GetComponent<Camera>().depth = 0;
        }
        else if (isLocalPlayer == false)
        {
            PlayerID = 2;
            Camera2.GetComponent<Camera>().depth = 1;
            Camera1.GetComponent<Camera>().depth = 0;
        }
    }

    void Update()
    {
        if (isLocalPlayer == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                CmdSphere();
            }

            Move();
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * 0.2f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += transform.forward * -0.2f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += transform.right * -0.2f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right * 0.2f;
        }
    }

    [Command]
    void CmdSphere()
    {
        GameObject obj = (GameObject)Instantiate(Sphere, transform.position, transform.rotation);
        NetworkServer.Spawn(obj);
    }
}