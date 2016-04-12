using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleCanvasHook : MonoBehaviour
{
    public delegate void CanvasHook();

    /// <summary>
    /// 0->me, 1'2->my troop, 3'4'5->enemy troop.
    /// </summary>
    public RectTransform[] arrayOfSeatPos;
    public GameObject mePref;
    public GameObject otherPref;

    private List<NetworkPlayerInfo> onlinePlayers;
    private NetworkPlayerInfo me;
    private List<NetworkPlayerInfo> friendPlayers;
    private List<NetworkPlayerInfo> enemyPlayers;
    private const int meIndex = 0;
    private int friendIndexer, enemyIndexer;

    public void InitHerosOnSeats()
    {
        onlinePlayers = MultyController.DefaultCtr.OnlinePlayers;
        friendPlayers = new List<NetworkPlayerInfo>();
        enemyPlayers = new List<NetworkPlayerInfo>();
        friendIndexer = meIndex + 1;
        enemyIndexer = Tags.PlayerLimit / Tags.TroopNum;

        for (int i = 0; i < onlinePlayers.Count; i++)
        {
            NetworkPlayerInfo curPlayer = onlinePlayers[i];
            if (curPlayer.Order != UserInfo.DefaultUser.Order)
            {
                InitAndSpawnOtherPlayers(curPlayer);
            }
            else
            {
                me = curPlayer;
                SpawnAHero(PlayerTypeModel.ME, me, meIndex);
            }
        }
    }

    private void InitAndSpawnOtherPlayers(NetworkPlayerInfo player)
    {
        bool isFriend = player.Troop == UserInfo.DefaultUser.Troop;
        if (isFriend)
        {
            friendPlayers.Add(player);
            SpawnAHero(PlayerTypeModel.FRIEND_TROOP, player, friendIndexer++);
        }
        else
        { 
            enemyPlayers.Add(player);
            SpawnAHero(PlayerTypeModel.ENEMY_TROOP, player, enemyIndexer++);
        }
    }

    private void SpawnAHero(PlayerTypeModel type, NetworkPlayerInfo player, int pos)
    {
        GameObject curPref = type == PlayerTypeModel.ME ? mePref : otherPref;
        Transform curGo = GameObject.Instantiate(curPref).transform;
        curGo.SetParent(arrayOfSeatPos[pos], false);
        BattleHeroItemView bhiv = curGo.GetComponent<BattleHeroItemView>();
        bhiv.InitHeroItem(type, player);
    }
}





