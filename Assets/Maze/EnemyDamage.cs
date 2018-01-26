using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public int EnemyHP = 2;
    public float MutekiTimer = 1.0f;
    public GameObject EnemyObj;
    Renderer rend;
    private int _frame = 0;

    // Use this for initialization
    void Start () {
        rend = EnemyObj.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {



        if (MutekiTimer > 0.0f)
        {
            MutekiTimer -= Time.deltaTime;

            _frame++;

            //30フレーム毎に表示・非表示を繰り返す
            if (_frame / 5 % 2 == 0)
            {

                rend.enabled = false;
            }
            else
            {
                rend.enabled = true;
            }
        }
        else
        {
            rend.enabled = true;

        }

        if (EnemyHP == 0)
        {
            Destroy(gameObject);
        }

	}
}
