using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Number_Create : NetworkBehaviour
{

    public GameObject Number_obj;
    public GameObject[] Number_pos1;
    public GameObject[] Number_pos2;
    public GameObject[] Number_pos3;

    [SyncVar]
    int Rand_num1;
    [SyncVar]
    int Rand_num2;
    [SyncVar]
    int Rand_num3;

    [SyncVar]
    int number1;
    [SyncVar]
    int number2;
    [SyncVar]
    int number3;

    [SyncVar]//問題の色の順番
    public int arrayColor1;
    [SyncVar]
    public int arrayColor2;
    [SyncVar]
    public int arrayColor3;
    [SyncVar]//問題の演算子の順番
    public int arrayOperator1;
    [SyncVar]
    public int arrayOperator2;

    [SyncVar]
    public int m_answer;    //問題の答え

    public GameObject Code_Input_obj;
    Code_Input Code_Input_scr;

    [SyncVar]
    public int isClear = 0;

    // Use this for initialization
    void Start () {

        Random_num();

        Code_Input_scr = Code_Input_obj.GetComponent<Code_Input>();
        //Code_Input_scr.Set_Mondai();

        Create_num(Number_pos1,Rand_num1,number1,1);
        Create_num(Number_pos2,Rand_num2,number2,2);
        Create_num(Number_pos3,Rand_num3,number3,3);

       
    }
	
	// Update is called once per frame
	void Update () {



        //Debug.Log("isClear " + isClear);
        //isClear++;
        if (isClear == 1)
        {
            Clear();
            //isClear = 2;

            //Enterキーを押したらタイトルへ戻る
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Title");
            }
        }
        //ButtonEnter();
    }

    [Server]
    void Random_num()
    {
        Rand_num1 = Random.Range(0, Number_pos1.Length - 1);
        Rand_num2 = Random.Range(0, Number_pos2.Length - 1);
        Rand_num3 = Random.Range(0, Number_pos3.Length - 1);

        number1 = Random.Range(11, 99);
        number2 = Random.Range(11, 99);
        number3 = Random.Range(11, 99);

        int[] arrayColor = { 1, 2, 3 };
        //色のランダム入れ替え
        for (int i = 0; i < arrayColor.Length; i++)
        {
            int j = Random.Range(0, arrayColor.Length - 1);
            int t = arrayColor[i];
            arrayColor[i] = arrayColor[j];
            arrayColor[j] = t;
        }

        arrayColor1 = arrayColor[0];
        arrayColor2 = arrayColor[1];
        arrayColor3 = arrayColor[2];

        arrayOperator1 = Random.Range(0, 2);
        arrayOperator2 = Random.Range(0, 2);

        Math_Answer();
    }

    //答えの計算
    void Math_Answer()
    {
        int c1 = 0, c2 = 0, c3 = 0;

        if(arrayColor1 == 1) c1 = number1;
        else if(arrayColor1 == 2) c1 = number2;
        else if (arrayColor1 == 3) c1 = number3;

        if (arrayColor2 == 1) c2 = number1;
        else if (arrayColor2 == 2) c2 = number2;
        else if (arrayColor2 == 3) c2 = number3;

        if (arrayColor3 == 1) c3 = number1;
        else if (arrayColor3 == 2) c3 = number2;
        else if (arrayColor3 == 3) c3 = number3;

        int o1 = 0, o2 = 0;

        if (arrayOperator1 == 0) o1 = 1;
        else if (arrayOperator1 == 1) o1 = -1;

        if (arrayOperator2 == 0) o2 = 1;
        else if (arrayOperator2 == 1) o2 = -1;

        m_answer = c1 + c2 * o1 + c3 * o2;


        Debug.Log(number1 + " " + number2 + " " + number3 + " A" + m_answer );

    }

    //[Command]
    void Create_num(GameObject[] num_pos,int rand,int number,int col)
    {
        int num = rand;
        for(int i = 0;i < num_pos.Length;i++)
        {
            if(i == num)
            {
                GameObject obj = Instantiate(Number_obj, num_pos[num].transform.position, num_pos[num].transform.rotation);
                obj.transform.parent = this.transform;

                Text targetText = obj.GetComponent<Text>();
                targetText.text = number.ToString();

                switch (col)
                {
                    case 1:
                        targetText.color = Color.red;
                        break;
                    case 2:
                        targetText.color = Color.blue;
                        break;
                    case 3:
                        targetText.color = Color.green;
                        break;
                }
            }
            else
            {
                num_pos[i].SetActive(false);
            }
        }
    }


    //[Server]
    public void ButtonEnter()
    {
        isClear = 1;
    }

    void Clear()
    {
        GameObject Seikou_Img = GameObject.Find("Seikou");
        iTween.MoveTo(Seikou_Img, iTween.Hash("y", 0.0f, "time", 5.0f, "isLocal", true));
        //Debug.Log("SEIKAI");


        GameObject Enter_txt = GameObject.Find("Enter");
        iTween.MoveTo(Enter_txt, iTween.Hash("y", -170.0f, "time", 5.0f, "isLocal", true));

        //フェードイン
        GameObject fade_obj = GameObject.Find("FadeManager");
        fade_obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        FadeManager fade = fade_obj.GetComponent<FadeManager>();
        GameObject Fade_Img = GameObject.Find("FadeImage");
        fade.FadeImage = Fade_Img.GetComponent<Image>();
        fade.enableFade = true;
        fade.enableFadeOut = true;
    }

}
