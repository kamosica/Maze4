using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.Networking;

//ランダム移動の敵
public class EnemyMove2 : NetworkBehaviour
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

    //[SyncVar]
    Vector3 AgentPositon;   //目標の座標


    // Use this for initialization
    void Start()
    {
        // 最初の位置を覚えておく
        start = transform.position;

        agent.enabled = true;
        // NavMeshAgentを取得して
        //agent = GetComponent<NavMeshAgent>();

        // ゴールを設定。
        AgentPositon = new Vector3(8.0f,1.5f,1.0f);

        Maze_obj = GameObject.FindGameObjectWithTag("Maze");

        Maze_scr = Maze_obj.GetComponent<Maze_Create>();
        maze_arrey = Maze_scr.maze_arrey;

        Rand_agent();

        //setNavMesh();
    }

    void SetAgentPositon()
    {
        agent.destination = AgentPositon;
    }


    //void setNavMesh()
    //{
    //    if (!isServer) agent.speed = 0;
    //}

    void Update()
    {
        if (!isServer)
        {
            return;
        }

        timer += Time.deltaTime;


        if (isChase == false)
        {
            if (IsPosition(pos_Y,pos_X))
            {
                Rand_agent();
            }
        }
        else if(isChase == true)
        {
            if (timer < 1) return;
            AgentPositon = Player_obj.transform.position;

            //プレイヤーと敵の距離が５より離れたら
            if (Vector2.Distance(transform.position, Player_obj.transform.position) > chase_range)
            {
                isChase = false;
                AgentPositon = new Vector3(8, 1.5f, 1);
            }
            timer = 0;

        }


        if (isPlayerChase == true)
        {
            //プレイヤーを見つけるためにレイを飛ばす
            //Debug.DrawRay(transform.position, transform.forward, Color.red, 3, false);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward * 10000, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Player")
                {
                    isChase = true;
                    Player_obj = hit.collider.gameObject;
                }
            }
        }

        SetAgentPositon();
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
                AgentPositon = new Vector3(pos_Y, 1.5f, pos_X);

                break;
            }
        }
    }

    //敵が目標座標に移動したかどうかの判定
    bool IsPosition(float x,float y)
    {
        bool isposX = transform.position.x > x - 0.3f && transform.position.x < x + 0.3f;
        bool isposY = transform.position.z > y - 0.3f && transform.position.z < y + 0.3f;

        if (isposX == true && isposY == true)
        {
            return true;
        }

        return false;
    }
}
