using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {

    Status status_scr;
    Image Damageimg;
    float MutekiTimer = 0.0f;
    public float MutekiTime = 7.0f;

    public GameObject PlayerLight;
    private int _frame = 0;

    // Use this for initialization
    void Start () {
        status_scr = gameObject.GetComponent<Status>();

        Damageimg = GameObject.Find("Damage_Red").GetComponent<Image>();
        Damageimg.color = Color.clear;
    }
	
	// Update is called once per frame
	void Update () {

        if (MutekiTimer > 0.0f)
        {
            MutekiTimer -= Time.deltaTime;


            _frame++;

            //30フレーム毎に表示・非表示を繰り返す
            if (_frame / 10 % 2 == 0)
            {
                PlayerLight.GetComponent<Light>().intensity = 0;
            }
            else
            {
                PlayerLight.GetComponent<Light>().intensity = 4;
            }
        }
        else
        {
            PlayerLight.GetComponent<Light>().intensity = 4;
        }
        this.Damageimg.color = Color.Lerp(this.Damageimg.color, Color.clear, Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if (MutekiTimer > 0.0f) return;
        if(other.gameObject.tag == "Enemy")
        {
            Damage();
            Debug.Log("PlayerHit");
            status_scr.Damages(1.0f);
            MutekiTimer = MutekiTime;
        }
    }

    void Damage()
    {
        this.Damageimg.color = new Color(0.5f, 0f, 0f, 0.5f);
    }
}
