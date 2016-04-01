using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyRoomItemView : MonoBehaviour
{
    public Text roomText;

    public void InitRoomItem(HostData data)
    {
        string roomName = data.gameName;
        int limit = data.playerLimit;
        int curNum = data.connectedPlayers;
        roomText.text = string.Format("   {0}||{1}/{2}", roomName, curNum, limit);
    }

}
