using UnityEngine;
using System.Collections;

public class NetworkPlayerInfo
{
    public int Order { get; set; }
    public string Name { get; set; }
    public int HeroId { get; set; }
    public bool IsReady { get; set; }
    public NetworkPlayer NPPlayer { get; set; }
}
