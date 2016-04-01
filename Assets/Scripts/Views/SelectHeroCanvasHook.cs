using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

public class SelectHeroCanvasHook : MonoBehaviour
{
    public delegate void CanvasHook();

    public RectTransform[] arrayOfSeatPos;
    public GameObject playerPref;

    public void SpawnPlayerToSeat_SYNC()
    {
        LPC_GameServer.DefaultServer.SpawnPlayerToSeat_RPC(arrayOfSeatPos, playerPref);
    }



}
