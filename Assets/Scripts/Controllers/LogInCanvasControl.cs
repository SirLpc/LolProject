using LitJson;
using System.Text;
using System.Collections.Generic;
using System;

[Serializable]
public class LogInCanvasControl : CanvasControl
{
    LoginCanvasHook hooks;
    private bool isServerListReceived = false;

    public override void Show()
    {
        base.Show();

        AppDelegate.CurrentStage = GameStage.LOGIN;
        hooks = canvas.GetComponent<LoginCanvasHook>();
        if (hooks == null)
            return;

        hooks.OnClickLogInHook = UILogIn;

        GetServerList();
    }

    private void GetServerList()
    {
        WWWClient www = new WWWClient(hooks, ServerURI.ServerDataUri);
        www.Timeout = 60f;

        www.SetCommonHandler(onDone: result =>
        {
            RefreshServerList(result.text);
        });

        www.Request();
    }
    private void RefreshServerList(string serverInfo)
    {
        JsonData datas = JsonMapper.ToObject(serverInfo);
        List<string> listOfServerName = new List<string>();
        for (int i = 0; i < datas.Count; ++i)
        {
            string name = datas[i].ToString();
            listOfServerName.Add(name);
        }
        hooks.SetServerNameDropdown(listOfServerName);

        isServerListReceived = true;
    }

    private void UILogIn()
    {
        if (!isServerListReceived)
            return;

        string currentServerName = hooks.GetServerName();
        currentServerName = currentServerName.Split(' ')[0];

        string path = string.Format("{0}serverName={1}&playerName={2}",
            ServerURI.UserInoUri,
            currentServerName.UrlEncode(Encoding.UTF8),
            hooks.GetUserName().UrlEncode(Encoding.UTF8));

        WWWClient www = new WWWClient(hooks, path);
        www.Timeout = 30f;

        www.SetCommonHandler(onDone: result =>
        {
            UserInfo.DefaultUser.Name = UnityEngine.Random.Range(0, 100).ToString("00");
            DebugManager.DefaultManager.Log(result.text);
        });

        www.Request();
    }

}

