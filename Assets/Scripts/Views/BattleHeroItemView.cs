using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleHeroItemView : MonoBehaviour
{
    public Image photoImg;
    public Text nameText;

    private PlayerTypeModel type;
    private NetworkPlayerInfo current;

    public void InitHeroItem(PlayerTypeModel type, NetworkPlayerInfo player)
    {
        this.type = type;
        this.current = player;
        switch (type)
        {
            case PlayerTypeModel.ME:
                RefreshMe();
                break;
            case PlayerTypeModel.FRIEND_TROOP:
                RefreshMyTroop();
                break;
            case PlayerTypeModel.ENEMY_TROOP:
                RefreshEnemyTroop();
                break;
        }
    }

    private void RefreshMe()
    {
        nameText.text = HeroData.DefaultData.Heros[current.HeroId].Name;
    }

    private void RefreshMyTroop()
    {
        nameText.text = HeroData.DefaultData.Heros[current.HeroId].Name;
    }

    private void RefreshEnemyTroop()
    {
        nameText.text = HeroData.DefaultData.Heros[current.HeroId].Name;
    }

}
