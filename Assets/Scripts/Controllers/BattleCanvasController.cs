using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BattleCanvasController : CanvasControl
{
    public BattleCanvasHook hooks;

    public override void Show()
    {
        base.Show();

        AppDelegate.CurrentStage = GameStage.BATTLE;
        hooks = canvas.GetComponent<BattleCanvasHook>();
        if (hooks == null)
            return;

        hooks.InitHerosOnSeats();
    }

}
