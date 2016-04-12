using AssemblyCSharp;
using System;
using UnityEngine;

[Serializable]
public class LobbyCanvasControl : CanvasControl
{
    LobbyCavasHook hooks;

    private bool isConnected = false;

    public override void Show()
    {
        base.Show();

        AppDelegate.CurrentStage = GameStage.SELECT_HERO;
        hooks = canvas.GetComponent<LobbyCavasHook>();
        if (hooks == null)
            return;

        hooks.OnClickStartServerHook = UIStartServer;
        hooks.OnClickCreateRoomHook = UICreateRoom;
        hooks.OnClickRefreshRoomHook = UIRefreshRoom;
        hooks.BindJoinRoomHook = UIJoinRoom;

        LPC_GameServer.DefaultServer.InitServer(ServerURI.MasterServerUri, ServerURI.MasterServerPort);
    }

    private void UIStartServer()
    {
        LPC_GameServer.DefaultServer.StartServer();
    }

    private void UICreateRoom()
    {
        UIStartServer();
        bool suc = LPC_GameServer.DefaultServer.RegisterHost(hooks.GetGameTypeName(), hooks.GetGameName());
        if (suc)
        {
            NetworkPlayerInfo npi = new NetworkPlayerInfo();
            npi.Order = 0;
            int troop = npi.Order / (Tags.PlayerLimit / Tags.TroopNum);
            npi.Troop = troop;
            UserInfo.DefaultUser.Order = 0;
            UserInfo.DefaultUser.Troop = troop;
            //todo kill me for TEST! give name in login scene
            UserInfo.DefaultUser.Name = UnityEngine.Random.Range(0, 100).ToString("00");

            npi.Name = UserInfo.DefaultUser.Name;
            MultyController.DefaultCtr.OnlinePlayers.Add(npi);
            GoSelectCanvas();
        }
    }

    private void UIRefreshRoom()
    {
        LPC_GameServer.DefaultServer.StartRequestRoom((HostData[] list) => {
            //如果没有房间，将会返回list.Length == 0

            hooks.RefreshRoomView(list);
        }, hooks.GetGameTypeName());
    }

    private void UIJoinRoom(HostData roomData)
    {
        //todo kill me for TEST! give name in login scene
        UserInfo.DefaultUser.Name = UnityEngine.Random.Range(0, 100).ToString("00");

        LPC_GameServer.DefaultServer.JoinHostRoom(roomData, (int state) =>
        {
            isConnected = state == 0;
            DebugManager.DefaultManager.Log("join success");

            GoSelectCanvas();
        });
    }
    
    private void GoSelectCanvas()
    {
        //if(roomData != null)
        //    DebugManager.DefaultManager.Log(roomData.connectedPlayers);

        AppDelegate.DefaultManager.ChangeCanvas(AppDelegate.DefaultManager.lobbyCanvas,
            AppDelegate.DefaultManager.selectCanvas);
    }
}