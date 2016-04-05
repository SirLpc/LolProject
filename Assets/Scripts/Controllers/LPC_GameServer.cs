// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace AssemblyCSharp
{
    public class LPC_GameServer : MonoBehaviour
    {
        private LPC_GameServer()
        {
        }

        private static GameObject s_LPC_GameServer_object;
        private static LPC_GameServer s_LPC_GameServer = null;
        private static NetworkView s_LPC_NetworkView = null;

        public static LPC_GameServer DefaultServer
        {
            get
            {
                if (s_LPC_GameServer == null)
                {
                    s_LPC_GameServer_object = new GameObject("DefaultServer");
                    s_LPC_GameServer = s_LPC_GameServer_object.AddComponent<LPC_GameServer>();
                    s_LPC_NetworkView = s_LPC_GameServer_object.AddComponent<NetworkView>();
                }

                return s_LPC_GameServer;
            }
        }

        public static NetworkView DefalutNetworkView
        {
            get
            {
                return s_LPC_NetworkView;
            }
        }


        /// <summary>
        /// init server...
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        public bool InitServer(string ip, int port)
        {
            //set property
            MasterServer.ipAddress = ip;
            MasterServer.port = port;

            return true;
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <returns><c>true</c>, if server was started, <c>false</c> otherwise.</returns>
        public bool StartServer()
        {
            //start...
            Network.InitializeServer(Tags.PlayerLimit, 25000, !Network.HavePublicAddress());

            return true;
        }

        public bool RegisterHost(string gameType, string gameName)
        {
            //register a game
            MasterServer.RegisterHost(gameType, gameName);

            return true;
        }

        public delegate void RequestRoomComplete(HostData[] list);
        private RequestRoomComplete complete_block = null;
        public RequestRoomComplete CompleteBlock
        {
            set
            {
                complete_block = value;
            }
            get
            {
                return complete_block;
            }
        }

        public void StartRequestRoom(RequestRoomComplete block, string gameTypeName)
        {
            LPC_GameServer.DefaultServer.CompleteBlock = block;

            MasterServer.RequestHostList(gameTypeName);
        }


        public delegate void JoinHostRoomDelegate(int state);

        private JoinHostRoomDelegate join_delegate = null;
        public void JoinHostRoom(HostData room, JoinHostRoomDelegate block)
        {
            this.join_delegate = block;

            NetworkConnectionError error = Network.Connect(room.ip[0], room.port);

            DebugManager.DefaultManager.Log(error);
        }

        public void SendGameMessage(string message)
        {
            LPC_GameServer.DefalutNetworkView.RPC("RemoteReceiveMessage", RPCMode.All, message);
        }

        [RPC]
        public void RemoteReceiveMessage(string message)
        {
            DebugManager.DefaultManager.Log(message);
        }

        //==>SERVER: Spawn player's to seat
        private RectTransform[] arrayOfSeatPos;
        GameObject playerPref;
        public void SpawnPlayerToSeat_RPC(int order, string name)
        {
            LPC_GameServer.DefalutNetworkView.RPC("SpawnPlayerToSeat", RPCMode.AllBuffered,
                order, name);
        }
        //ALL
        [RPC]
        private void SpawnPlayerToSeat(int order, string userName)
        {
            arrayOfSeatPos = AppDelegate.DefaultManager.selectCanvas.GetSeatPosS();
            RectTransform seatPos = arrayOfSeatPos[order];
            playerPref = Resources.Load<GameObject>("UI/SelectHeroItem");
            GameObject go = GameObject.Instantiate(playerPref, seatPos.position,
                Quaternion.identity) as GameObject;
            go.transform.SetParent(seatPos, true);
            SelectHeroItemView shiv = go.GetComponent<SelectHeroItemView>();
            shiv.InitItem(userName);
        }

        //==>COMMU: Report info to server
        //Server
        public void GiveAndAskInfoAndSpawnSeat_RPC(NetworkPlayer nPlayer, int order)
        {
            StartCoroutine(CoGiveAndAskInfoAndSpawnSeat(nPlayer, order));
        }
        private IEnumerator CoGiveAndAskInfoAndSpawnSeat(NetworkPlayer nPlayer, int order)
        {
            yield return null;
            DefalutNetworkView.RPC("GiveAndAskInfo", nPlayer, order);
        }
        //Client
        [RPC]
        private void GiveAndAskInfo(int order)
        {
            UserInfo.DefaultUser.Order = order;
            DefalutNetworkView.RPC("ReplyInfoAndSpawnSeat", RPCMode.Server, UserInfo.DefaultUser.Name, order);
        }
        //Server
        [RPC]
        private void ReplyInfoAndSpawnSeat(string clientPlayerName, int order)
        {
            MultyController.DefaultCtr.OnlinePlayers[order].Name = clientPlayerName;
            SpawnPlayerToSeat_RPC(order, clientPlayerName);
        }




        #region Behaviour Actions

        /// <summary>
        /// some event notification from master server
        /// </summary>
        /// <param name="ev">Ev.</param>
        public void OnMasterServerEvent(MasterServerEvent ev)
        {
            switch (ev)
            {
                case MasterServerEvent.RegistrationSucceeded:
                    {
                        break;
                    }

                case MasterServerEvent.RegistrationFailedNoServer:
                    {
                        break;
                    }
                case MasterServerEvent.RegistrationFailedGameType:
                    {
                        break;
                    }
                case MasterServerEvent.RegistrationFailedGameName:
                    {
                        break;
                    }
                case MasterServerEvent.HostListReceived:
                    {
                        LPC_GameServer.DefaultServer.CompleteBlock(MasterServer.PollHostList());
                        break;
                    }
                default:
                    break;
            }
        }

        public void OnPlayerConnected(NetworkPlayer player)
        {
            NetworkPlayerInfo npi = new NetworkPlayerInfo();
            npi.Order = MultyController.DefaultCtr.OnlinePlayers.Count;
            npi.NPPlayer = player;
            MultyController.DefaultCtr.OnlinePlayers.Add(npi);
            GiveAndAskInfoAndSpawnSeat_RPC(npi.NPPlayer, npi.Order);
        }

        public void OnPlayerDisconnected(NetworkPlayer player)
        {
            List<NetworkPlayerInfo> players = new List<NetworkPlayerInfo>();
            NetworkPlayerInfo npi = players.Find(p => p.NPPlayer.Equals(player));
            players.Remove(npi);

        }

        public void OnConnectedToServer()
        {
            this.join_delegate(0);
            DebugManager.DefaultManager.Log("OnConnectedToServer");

        }

        #endregion
    }
}






