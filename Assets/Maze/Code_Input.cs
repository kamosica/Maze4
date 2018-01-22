using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Code_Input : NetworkBehaviour
{

    int Answer = 0;

    public GameObject AnswerText_obj;
    Text AnswerText_text;

    public GameObject MondaiImage1;
    public GameObject MondaiImage2;
    public GameObject MondaiImage3;

    public GameObject MondaiText1;
    public GameObject MondaiText2;

    public GameObject Number_Create_obj;
    Number_Create Number_Create_scr;

    public GameObject Seikou_Img;

    public GameObject Fade_Img;

    [SyncVar]
    public int isClear = 0;

    // Use this for initialization
    void Start () {
        Number_Create_obj = GameObject.FindGameObjectWithTag("Code");

        AnswerText_text = AnswerText_obj.GetComponent<Text>();
        Number_Create_scr = Number_Create_obj.GetComponent<Number_Create>();

        Set_Mondai();

        //iTween.FadeTo(Seikou_Img, iTween.Hash("alpha", 0, "time", 2));
    }

    // Update is called once per frame
    void Update () {

        //ButtonEnter();

        //Debug.Log("isclear " + isClear);
        //isClear++;
        //if (isClear == 1)
        //{
        //    Clear();
        //    isClear = 0;
        //}
        //Set_Mondai();
    }

    public void Set_Mondai()
    {
        selectColor(MondaiImage1, Number_Create_scr.arrayColor1);
        selectColor(MondaiImage2, Number_Create_scr.arrayColor2);
        selectColor(MondaiImage3, Number_Create_scr.arrayColor3);

        Text Montxt1 = MondaiText1.GetComponent<Text>();
        if (Number_Create_scr.arrayOperator1 == 0)
        {
            Montxt1.text = "+";
        }
        else if (Number_Create_scr.arrayOperator1 == 1)
        {
            Montxt1.text = "-";
        }
        Text Montxt2 = MondaiText2.GetComponent<Text>();
        if (Number_Create_scr.arrayOperator2 == 0)
        {
            Montxt2.text = "+";
        }
        else if (Number_Create_scr.arrayOperator2 == 1)
        {
            Montxt2.text = "-";
        }
    }

    void selectColor(GameObject obj,int color_num)
    {
        Image img = obj.GetComponent<Image>();
        switch (color_num)
        {
            case 1:
                img.color = Color.red;
                break;
            case 2:
                img.color = Color.blue;
                break;
            case 3:
                img.color = Color.green;
                break;
        }
    }

    //答えの判定
    void Judge_Answer()
    {
        AnswerText_text.text = Answer.ToString();
        if (Answer == Number_Create_scr.m_answer)
        {
            //Debug.Log("SEIKAI");
        }
    }

    public void OnClick_Button0(int num)
    {
        if(Mathf.Abs(Answer) < 1)
        {
            Answer = num;
        }
        else
        {
            if (Answer > 0)
            {
                Answer = Answer * 10 + num;
            }
            else if(Answer < 0)
            {
                Answer = Answer * 10 - num;
            }      
        }

        Judge_Answer();
    }

    public void CmdOnClick_ButtonEnter()
    {
        if (Answer == Number_Create_scr.m_answer)
        {
            Number_Create_scr.ButtonEnter();
            transform.localPosition = new Vector3(0.0f, 1300.0f, 0.0f);
        }
        else
        {
            iTween.ShakePosition(gameObject, iTween.Hash("x", 5.0f, "y", 5.0f, "time", 0.5f));
        }
    }

    //[Server]
    //void ButtonEnter()
    //{
    //    isClear = 1;
    //}

    void Clear()
    {
        transform.localPosition = new Vector3(0.0f, 1300.0f, 0.0f);
        Seikou_Img = GameObject.Find("Seikou");
        iTween.MoveTo(Seikou_Img, iTween.Hash("y", 0.0f, "time", 5.0f, "isLocal", true));
        //Debug.Log("SEIKAI");

        //フェードイン
        GameObject fade_obj = GameObject.Find("FadeManager");
        FadeManager fade = fade_obj.GetComponent<FadeManager>();
        Fade_Img = GameObject.Find("FadeImage");
        fade.FadeImage = Fade_Img.GetComponent<Image>();
        fade.enableFade = true;
        fade.enableFadeOut = true;
    }
    public void OnClick_ButtonCancel()
    {
        if(Mathf.Abs(Answer) < 10)
        {
            Answer = 0;
        }
        else
        {
            Answer = Answer / 10;
        }

        Judge_Answer();
    }

    public void OnClick_ButtonMinus()
    {
        if(Answer > 0)
        {
            Answer *= -1;
        }

        Judge_Answer();
    }


    public void OnClick_ButtonPuls()
    {
        if (Answer < 0)
        {
            Answer *= -1;
        }

        Judge_Answer();
    }
}
