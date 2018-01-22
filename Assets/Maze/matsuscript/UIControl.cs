using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
    [SerializeField]
    private Image Openui;
    [SerializeField]
    private GameObject TrapUnderUI;
    Animator ui1ani;
	// Use this for initialization
	void Start () {
        ui1ani = GameObject.Find("OpenUi").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //Mouseinput();
    }
    void Mouseinput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Openui)
            {
                OpenorClose();
            }
        }
    }
    bool s=true;
    public void OpenorClose()
    {
        //Debug.Log(s);
        //close posx-250 open posx385
        if (s == false)//開いていない時
        {
            Texture2D texture = Resources.Load("UI_close") as Texture2D;
            Openui.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            //Openui.rectTransform.anchoredPosition = new Vector2(385, -180);
            ui1ani.SetBool("open", true);
            s = true;
        }
        else//開いている時
        {
            Texture2D texture = Resources.Load("UI_open") as Texture2D;
            Openui.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            //Openui.rectTransform.anchoredPosition = new Vector2(-247, -180);
            ui1ani.SetBool("close", true);
            ui1ani.SetBool("open", false);
            s = false;
        }
    }
}
