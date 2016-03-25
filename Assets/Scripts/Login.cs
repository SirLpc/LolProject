using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections.Generic;
using System.Text;
using System;

public class Login : MonoBehaviour
{
    public InputField userName;
    public Dropdown serverNameDrop;
    public Button loginButton;

    private const string server_url = "http://115.28.87.227/UnityFiles/LOL/server_data.txt";

    private void Start()
    {
        GetServerList();

        loginButton.onClick.AddListener(btnLogin_Click);
    }

    private void GetServerList()
    {
        WWWClient www = new WWWClient(this, server_url);
        www.Timeout = 60f;

        www.OnDone = result =>
        {
            RefreshServerList(result.text);
            msg += result.text + "\n";
        };
        www.OnFail = result =>
        {
            Debug.Log(result.error);
        };
        www.OnDisposed = () =>
        {
            Debug.Log("Connect out time");
        };

        www.Request();
    }
    private void RefreshServerList(string serverInfo)
    {
        serverNameDrop.ClearOptions();

        JsonData datas = JsonMapper.ToObject(serverInfo);
        List<string> listOfServerName = new List<string>();
        for (int i = 0; i < datas.Count; ++i)
        {
            string name = datas[i].ToString();
            listOfServerName.Add(name);
        }
        serverNameDrop.AddOptions(listOfServerName);
    }

    private void btnLogin_Click()
    {
        loginButton.DisableSeconds(1f);

        string currentServerName = serverNameDrop.options[serverNameDrop.value].text;
        currentServerName = currentServerName.Split(' ')[0];

        string path = string.Format("http://API.xunjob.cn/playerinfo.php?serverName={0}&playerName={1}",
            currentServerName.UrlEncode(Encoding.UTF8), userName.text.UrlEncode(Encoding.UTF8));
        WWWClient www = new WWWClient(this, path);
        www.Timeout = 60f;

        www.OnDone = result =>
        {
            msg += result.text + "\n" + path + "\n";
            Debug.Log(result.text);
        };
        www.OnFail = result =>
        {
            Debug.Log(result.error);
        };
        www.OnDisposed = () =>
        {
            Debug.Log("Connect out time");
        };

        www.Request();
    }

    string msg = "";
    void OnGUI()
    {
        GUILayout.Label(msg);
    }
}
