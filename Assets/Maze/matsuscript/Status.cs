using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Status : NetworkBehaviour {
    public float CharacterHP;
    [SerializeField]
    private float maxhp;
    private float time;
    private float Reg;
    private float HPReg;//体力自動回復量
    enum Health
    {
        Onfire,
        Poizon,
        None,
    }
    Health health;
    [SerializeField]
    private GameObject pa;
    [SerializeField]
    private GameObject pa2;
    private Slider HPgauge;
    private Text hptext;

    public Image life1;
    public Image life2;
    public Image life3;

    // Use this for initialization
    void Start () {
        HPReg = 1;
        if (gameObject.tag == "Player")
        {
            CharacterHP = maxhp;
            //pa.SetActive(false);
            //HPgauge = GameObject.Find("HP").GetComponent<Slider>();
            //HPgauge.value = CharacterHP;
            //HPgauge.maxValue = maxhp;
            //hptext = GameObject.Find("HPText").GetComponent<Text>();
            //hptext.text = (int)CharacterHP + "/" + maxhp;
        }
        if (isLocalPlayer)
        {
            //UIs();
        }
        health = Health.None;
	}
	
	// Update is called once per frame
	void Update () {
        //if (gameObject.tag == "Player")
        //{
        //    HPgauge.value = CharacterHP;
        //    hptext.text = (int)CharacterHP + "/" + maxhp;
        //}
        time += Time.deltaTime;
        if (time >= 15 && gameObject.tag == "Player"&&CharacterHP!=100)
        {//体力自動回復
            Reg -= Time.deltaTime;
            if (Reg <= 0.0)
            {
                Reg = 1.0f;
                //CharacterHP += HPReg;
                //HPgauge.value = CharacterHP;
               // hptext.text = (int)CharacterHP + "/" + maxhp;
            }
        }
        if (health==Health.Onfire||health==Health.Poizon)
        {
            State_abnormality();
        }
        if (CharacterHP <= 0)
        {
            Dead();
        }
	}
    public void Damages(float damage)
    {
        time = 0;
        CharacterHP -= 1;//damage
        //HPgauge.value = CharacterHP;
        //hptext.text = (int)CharacterHP + "/" + maxhp;
        if (CharacterHP == 2)
        {
            life3.color = Color.black;
        }
        if (CharacterHP == 1)
        {
            life2.color = Color.black;
        }
        if (CharacterHP == 0)
        {
            life1.color = Color.black;
        }
    }
    public void Healing(float heal)//外的回復
    {
        CharacterHP += heal;
    }
    public void Dead()
    {
        CharacterHP = 0;
        //Destroy(gameObject);
    }
    float fire;
    float ti;
    void State_abnormality()//状態異常
    {
        switch (health)
        {
            case Health.None:
                break;
            case Health.Onfire:
                 fire -= Time.deltaTime;
                ti += Time.deltaTime;
                if (ti <= 5)
                {
                    if (fire <= 0.0f)
                    {
                        fire = 1.0f;
                        CharacterHP -= 2;
                    }
                }
                else
                {
                    ti = 0;
                    health = Health.None;
                }
                break;
            case Health.Poizon:
                break;
        }
    }
    [Client]
    void UIs()
    {
        pa.SetActive(true);
        //pa2.SetActive(false);
    }
}
