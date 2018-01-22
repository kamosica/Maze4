using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.Networking;

//決まった場所を周回する敵
public class EnemyMove : NetworkBehaviour
{

    public Transform goal;
    Vector3 start;
    public NavMeshAgent agent;

    public LayerMask mask;

    bool isChase = false;//プレイヤーを追いかけるかどうか

    GameObject Player_obj;//プレイヤーのオブジェクト

    float chase_range = 5.0f;   //追跡範囲

    Vector2[] agent_pos = new Vector2[4];

    public GameObject Maze_obj;
    Maze_Create Maze_scr;
    int[,] maze_arrey;
    int pos_X = 0;
    int pos_Y = 0;

    float timer = 0;

    //[SyncVar]
    Vector3 AgentPositon;   //目標の座標

    void SetAgentPositon()
    {
        agent.destination = AgentPositon; 
    }

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

        //agent_pos[0] = new Vector2(8,1);
        //agent_pos[1] = new Vector2(8, 4);
        //agent_pos[2] = new Vector2(1, 4);
        //agent_pos[3] = new Vector2(1, 1);
        Rand_agent();

        // ゴールを設定。
       AgentPositon = new Vector3(agent_pos[0].x, 1.5f, agent_pos[0].y);

       //Debug.Log("X" + agent_pos[0].x + " Y" + agent_pos[0].y);
    }

    //[ServerCallback]
    void Update()
    {
        if (!isServer)
        {
            return;
        }

        timer += Time.deltaTime;

        if (isChase == false)
        {
            if (IsPosition(agent_pos[0].x, agent_pos[0].y))
            {
               AgentPositon = new Vector3(agent_pos[1].x, transform.position.y, agent_pos[1].y);
            }
            else if (IsPosition(agent_pos[1].x, agent_pos[1].y))
            {
               AgentPositon = new Vector3(agent_pos[2].x, transform.position.y, agent_pos[2].y);
            }
            else if (IsPosition(agent_pos[2].x, agent_pos[2].y))
            {
               AgentPositon = new Vector3(agent_pos[3].x, transform.position.y, agent_pos[3].y);
            }
            else if (IsPosition(agent_pos[3].x, agent_pos[3].y))
            {
               AgentPositon = new Vector3(agent_pos[0].x, transform.position.y, agent_pos[0].y);
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


        //Debug.DrawRay(transform.position, transform.forward, Color.red, 3, false);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * 10000, out hit, Mathf.Infinity))
        {

           if(hit.collider.tag =="Player")
            {
                isChase = true;
                Player_obj = hit.collider.gameObject;
            }
        }

        SetAgentPositon();
    }

    //敵が目標座標に移動したかどうかの判定
    bool IsPosition(float x, float y)
    {
        bool isposX = transform.position.x > x - 0.1f && transform.position.x < x + 0.1f;
        bool isposY = transform.position.z > y - 0.1f && transform.position.z < y + 0.1f;

        if (isposX == true && isposY == true)
        {
            return true;
        }

        return false;
    }

    //目標をランダムで設定
    void Rand_agent()
    {
        for (int i = 0; i < 4; i++)
        {
            while (true)
            {
                pos_X = Random.Range(0, maze_arrey.GetLength(0));
                pos_Y = Random.Range(0, maze_arrey.GetLength(1));

                //Debug.Log("0 X" + pos_X + "Y" + pos_Y);

                if (maze_arrey[pos_X, pos_Y] == 0)
                {
                    agent_pos[i] = new Vector2(pos_Y, pos_X);

                    break;
                }
            }
        }
    }

}
