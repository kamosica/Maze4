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

    public AudioClip audioClip_Damage;
    public AudioClip audioClip_GameOver;
    private AudioSource audioSource;

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
        if(Damageimg.color == Color.clear)
        {
            Damageimg.transform.localPosition = new Vector3(0.0f, 1000.0f, 0.0f);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (MutekiTimer > 0.0f) return;
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Trap")
        {
            Damage();
            Debug.Log("PlayerHit");
            status_scr.Damages(1.0f);
            MutekiTimer = MutekiTime;
            Damageimg.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = audioClip_Damage;
            audioSource.Play();

            if (status_scr.CharacterHP == 0)
            {
                GameOver();

                audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.clip = audioClip_GameOver;
                audioSource.Play();
            }
        }
    }

    void Damage()
    {
        this.Damageimg.color = new Color(0.5f, 0f, 0f, 0.5f);
    }

    void GameOver()
    {
        GameObject Sippai_Img = GameObject.Find("Sippai");
        iTween.MoveTo(Sippai_Img, iTween.Hash("y", 0.0f, "time", 5.0f, "isLocal", true));
        //Debug.Log("SEIKAI");


        GameObject Enter_txt = GameObject.Find("Enter");
        iTween.MoveTo(Enter_txt, iTween.Hash("y", -170.0f, "time", 5.0f, "isLocal", true));

        //フェードイン
        GameObject fade_obj = GameObject.Find("FadeManager");
        FadeManager fade = fade_obj.GetComponent<FadeManager>();
        GameObject Fade_Img = GameObject.Find("FadeImage");
        Fade_Img.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        fade.FadeImage = Fade_Img.GetComponent<Image>();
        fade.enableFade = true;
        fade.enableFadeOut = true;
    }
}
