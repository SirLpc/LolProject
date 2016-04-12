using UnityEngine;
using System.Collections.Generic;
using AssemblyCSharp;

public class MultyController
{
    #region Singleton And Init
    private static MultyController instance = null;
    public static MultyController DefaultCtr
    {
        get
        {
            if (instance == null)
                instance = new MultyController();
            return instance;
        }
    }
    private MultyController()
    {
        OnlinePlayers = new List<NetworkPlayerInfo>();
        DisconnectedPlayerOrders = new Queue<int>();
    } 
    #endregion

    public List<NetworkPlayerInfo> OnlinePlayers;
    public Queue<int> DisconnectedPlayerOrders; 

    public void OnPlayerReady()
    {
        if (!Network.isServer)
        {
            Debug.LogWarning("Only SERVER can call this Method!");
            return;
        }

        if (OnlinePlayers.Count >= Tags.PlayerLimit)
        {
            int readyPlayerNum = 0;
            for (int i = 0; i < OnlinePlayers.Count; i++)
                if (OnlinePlayers[i].IsReady)
                    readyPlayerNum++;
            if (readyPlayerNum >= Tags.PlayerLimit)
            {
                AppDelegate.CurrentStage = GameStage.READY_COUNTING;
                LPC_GameServer.DefaultServer.CountDownAndStartIfFinish_RPC(Tags.ReadyToStartSeconds);
            }
        }
    }

}
