using UnityEngine;
using System.Collections;
using System;

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
    }

    public RectTransform[] GetSeatPosS()
    {
        return hooks.GetSeatPosS();
    }
}
