using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager_Custom : NetworkManager
{

    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }

    //********** 開始 **********//
    //UnityデフォルトのAPI シーンをロードした時にlevelを引数に実行
    //各シーンのlevelはBuild Settingsにて設定
    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            //Menuシーンへ移動した場合
            SetupMenuSceneButtons();
        }
        else
        {
            //他のシーン(Mainシーン)へ移動した場合
            SetupOtherSceneButtons();
        }
    }

    void SetupMenuSceneButtons()
    {
        //RemoveListener: Buttonのイベントを削除する
        //AddListener: ボタンのイベントを登録する
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }

    void SetupOtherSceneButtons()
    {
        //DisconnectボタンにStopHostメソッドを登録する
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }
    //********** 終了 **********//
}