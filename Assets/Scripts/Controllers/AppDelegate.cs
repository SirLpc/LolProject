using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppDelegate : MonoBehaviour
{
    public static AppDelegate DefaultManager = null;
    public LogInCanvasControl loginCanvas;
    public LobbyCanvasControl lobbyCanvas;
    public SelectHeroCanvasControl selectCanvas;
    public BattleCanvasController battleCanvas;
    public static GameStage CurrentStage;

    void Awake()
    {
        Application.runInBackground = true;
        DefaultManager = this;
    }

    void Start ()
    {
        //loginCanvas.Show();
        lobbyCanvas.Show();
	}

    public void ChangeCanvas<T, M>(T fromCanvas, M toCanvas) where T : CanvasControl where M : CanvasControl
    {
        fromCanvas.Hide();
        toCanvas.Show();
    }
	
    void Update()
    {
        if (!Network.isServer) return;
        if (Input.GetMouseButtonDown(1))
        {
            string sendxml = AssemblyCSharp.LPC_XMLTool.Serializer(typeof(List<NetworkPlayerInfo>), MultyController.DefaultCtr.OnlinePlayers);
            AssemblyCSharp.LPC_GameServer.DefaultServer.SendGameMessage(sendxml);
        }
    }
}
