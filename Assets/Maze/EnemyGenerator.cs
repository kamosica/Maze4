using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyGenerator : NetworkBehaviour
{

    public GameObject [] Enemy_prefab = new GameObject[4];

    int Max_Enemy = 8;
    float Wave_Time = 23.0f;
    float w_timer;

    // Use this for initialization
    void Start () {

        CmdCreateEnemy();
        w_timer = Wave_Time;
    }
	
	// Update is called once per frame
	void Update () {

        w_timer -= Time.deltaTime;
        if(w_timer < 0)
        {
            CmdCreateRandomEnemy();
            w_timer = Wave_Time;
        }
	}

    [Command]
    void CmdCreateRandomEnemy()
    {
        GameObject[]  tagObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (tagObjects.Length == Max_Enemy) return;

        int iRandNum = Random.Range(0, 4);
        switch (iRandNum)
        {
            case 0:
                GameObject obj = (GameObject)Instantiate(Enemy_prefab[0], new Vector3(1.0f, 0.6f, 1.0f), transform.rotation);
                NetworkServer.Spawn(obj);
                return;
            case 1:
                GameObject obj1 = (GameObject)Instantiate(Enemy_prefab[1], new Vector3(1.0f, 0.6f, 19.0f), transform.rotation);
                NetworkServer.Spawn(obj1);
                return;
            case 2:
                GameObject obj2 = (GameObject)Instantiate(Enemy_prefab[2], new Vector3(35.0f, 0.6f, 19.0f), transform.rotation);
                NetworkServer.Spawn(obj2);
                return;
            case 3:
                GameObject obj3 = (GameObject)Instantiate(Enemy_prefab[3], new Vector3(35.0f, 0.6f, 1.0f), transform.rotation);
                NetworkServer.Spawn(obj3);
                return;
        }
    }


    [Command]
    void CmdCreateEnemy()
    {
        //for (int i = 0; i < 4; i++)
        //{
        //    GameObject obj = (GameObject)Instantiate(Enemy_prefab[i], new Vector3(1.0f, 1.5f, 1.0f), transform.rotation);
        //    NetworkServer.Spawn(obj);
        //}

        GameObject obj = (GameObject)Instantiate(Enemy_prefab[0], new Vector3(1.0f, 0.6f, 1.0f), transform.rotation);
        NetworkServer.Spawn(obj);
        GameObject obj1 = (GameObject)Instantiate(Enemy_prefab[1], new Vector3(1.0f, 0.6f, 19.0f), transform.rotation);
        NetworkServer.Spawn(obj1);
        GameObject obj2 = (GameObject)Instantiate(Enemy_prefab[2], new Vector3(35.0f, 0.6f, 19.0f), transform.rotation);
        NetworkServer.Spawn(obj2);
        GameObject obj3 = (GameObject)Instantiate(Enemy_prefab[3], new Vector3(35.0f, 0.6f, 1.0f), transform.rotation);
        NetworkServer.Spawn(obj3);

        //GameObject obj3 = (GameObject)Instantiate(Enemy_prefab[3], new Vector3(1.0f, 10.6f, 1.0f), transform.rotation);
        //NetworkServer.Spawn(obj3);
        //obj3.transform.position = new Vector3(35.0f, 10.6f, 1.0f);
        //Debug.Log("OBJ3 X" + obj3.transform.position.x + " Z" + obj3.transform.position.z);

        //for (int i = 1; i < 19; i++)
        //{
        //    GameObject obj3 = (GameObject)Instantiate(Enemy_prefab[3], new Vector3(1.0f, 0.6f, i), transform.rotation);
        //    NetworkServer.Spawn(obj3);
        //}

        //for (int i = 1; i < 35; i++)
        //{
        //    GameObject obj3 = (GameObject)Instantiate(Enemy_prefab[3], new Vector3(i, 0.6f, 1.0f), transform.rotation);
        //    NetworkServer.Spawn(obj3);
        //}
    }
}
