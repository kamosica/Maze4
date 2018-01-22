using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.Networking;


//ずっと追ってくる敵
public class EnemyMove3 : NetworkBehaviour
{

    public Transform goal;
    Vector3 start;
    public NavMeshAgent agent;

    public LayerMask mask;

    bool isChase = false;//プレイヤーを追いかけるかどうか

    public bool isPlayerChase = true; //プレイヤーを追いかける敵のフラグ
    float chase_range = 5.0f;   //追跡範囲

    GameObject Player_obj;//プレイヤーのオブジェクト

    public GameObject Maze_obj;
    Maze_Create Maze_scr;
    int[,] maze_arrey;
    int pos_X = 0;
    int pos_Y = 0;

    float timer = 0;

    // Use this for initialization
    void Start()
    {
        // 最初の位置を覚えておく
        start = transform.position;

        agent.enabled = true;
        // NavMeshAgentを取得して
        //agent = GetComponent<NavMeshAgent>();

        Maze_obj = GameObject.FindGameObjectWithTag("Maze");

        Maze_scr = Maze_obj.GetComponent<Maze_Create>();
        maze_arrey = Maze_scr.maze_arrey;

        //transform.position = new Vector3(35.0f, 0.0f, 1.0f);

        Debug.Log("X" + transform.position.x + " Z" + transform.position.z);

        //Rand_agent();
    }

    [ServerCallback]
    void Update()
    {
        if (!isServer)
        {
            return;
        }

        timer += Time.deltaTime;

        if (isChase == false)
        {
            Player_obj = GameObject.FindGameObjectWithTag("Player");

            if (Player_obj != null)
            {
                isChase = true;
            }
        }
        else if(isChase == true)
        {
            if (timer < 1) return;

            agent.destination = Player_obj.transform.position;

            timer = 0;

        }


        //transform.position = new Vector3(35.0f, 0.6f, 1.0f);

        //Debug.Log("X" + transform.position.x + " Z" + transform.position.z);

        //if (isPlayerChase == true)
        //{
        //    //プレイヤーを見つけるためにレイを飛ばす
        //    //Debug.DrawRay(transform.position, transform.forward, Color.red, 3, false);
        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position, transform.forward * 10000, out hit, Mathf.Infinity))
        //    {
        //        if (hit.collider.tag == "Player")
        //        {
        //            isChase = true;
        //            Player_obj = hit.collider.gameObject;
        //        }
        //    }
        //}
    }

    //目標をランダムで設定
    void Rand_agent()
    {
        while (true)
        {
            pos_X = Random.Range(0, maze_arrey.GetLength(0));
            pos_Y = Random.Range(0, maze_arrey.GetLength(1));

            //Debug.Log("0 X" + pos_X + "Y" + pos_Y);

            if (maze_arrey[pos_X, pos_Y] == 0)
            {
                agent.destination = new Vector3(pos_Y, 1.5f, pos_X);

                break;
            }
        }
    }

    //敵が目標座標に移動したかどうかの判定
    bool IsPosition(float x,float y)
    {
        bool isposX = transform.position.x > x - 0.1f && transform.position.x < x + 0.1f;
        bool isposY = transform.position.z > y - 0.1f && transform.position.z < y + 0.1f;

        if (isposX == true && isposY == true)
        {
            return true;
        }

        return false;
    }
}
