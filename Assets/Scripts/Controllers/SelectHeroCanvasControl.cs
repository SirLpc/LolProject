using UnityEngine;
using System.Collections;
using System;
using AssemblyCSharp;

[Serializable]
public class SelectHeroCanvasControl : CanvasControl
{
    SelectHeroCanvasHook hooks;

    public override void Show()
    {
        base.Show();

        hooks = canvas.GetComponent<SelectHeroCanvasHook>();
        if (hooks == null)
            return;

        if(Network.isServer)
            hooks.SpawnPlayerToSeat_SYNC();

        hooks.OnClickReadyHook = UIReady;
        hooks.OnChangeHeroHook = UIChangeHero;
        hooks.StartGameWhenCountFinishHook = AutoStartGame;
        hooks.InitHeroSelector();
    }

    public RectTransform[] GetSeatPosS()
    {
        return hooks.GetSeatPosS();
    }

    public void CountDown(int count)
    {
        hooks.CountDown(count);
    }

    private void UIChangeHero(HeroInfo hero)
    {
        LPC_GameServer.DefaultServer.TellServerChangeHeroTo_RPC(UserInfo.DefaultUser.Order, hero);
    }

    private void UIReady()
    {
        LPC_GameServer.DefaultServer.TellServerImReady_RPC(UserInfo.DefaultUser.Order);
    }

    private void AutoStartGame()
    {
        if (Network.isServer)
            LPC_GameServer.DefaultServer.AutoStartGame_RPC();
    }
}
