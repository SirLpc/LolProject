using AssemblyCSharp;
using System;
using UnityEngine;

[Serializable]
public class LobbyCanvasControl : CanvasControl
{
    LobbyCavasHook hooks;

    private LOL_UserInfo user = new LOL_UserInfo();
    private HostData[] room_list = null;

    public override void Show()
    {
        base.Show();

        hooks = canvas.GetComponent<LobbyCavasHook>();
        if (hooks == null)
            return;

        hooks.OnClickStartServerHook = UIStartServer;
        hooks.OnClickCreateRoomHook = UICreateRoom;
        hooks.OnClickRefreshRoomHook = UIRefreshRoom;

        user.Name = "Xiaohao";
        user.Age = 18;
        user.UserID = 100001;
        LPC_GameServer.DefaultServer.InitServer(ServerURI.MasterServerUri, ServerURI.MasterServerPort);
    }

    private void UIStartServer()
    {
        LPC_GameServer.DefaultServer.StartServer();
    }

    private void UICreateRoom()
    {
        LPC_GameServer.DefaultServer.RegisterHost(hooks.GetGameTypeName(), hooks.GetGameName());
    }

    private void UIRefreshRoom()
    {
        LPC_GameServer.DefaultServer.StartRequestRoom((HostData[] list) => {
            this.room_list = list;

            //如果没有房间，将会返回list.Length == 0

            hooks.RefreshRoomView(list);
        }, hooks.GetGameTypeName());
    }

    
}