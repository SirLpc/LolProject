using UnityEngine;
using System.Collections.Generic;

public class MultyController : MonoBehaviour
{
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
    }

    public List<NetworkPlayerInfo> OnlinePlayers;



}
