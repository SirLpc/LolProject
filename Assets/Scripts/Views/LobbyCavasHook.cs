using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyCavasHook : MonoBehaviour
{
    public delegate void CanvasHook();

    public CanvasHook OnClickStartServerHook;
    public CanvasHook OnClickCreateRoomHook;
    public CanvasHook OnClickRefreshRoomHook;
    public System.Action<HostData> BindJoinRoomHook;

    public Button startServerButton;
    public Button createRoomButton;
    public Button refreshRoomButton;

    public Text gameTypeName;
    public Text gameName;
    public RectTransform viewPortContainer;
    public RectTransform roomContentContainer;

    public GameObject roomItemPref;

    private void Awake()
    {
        startServerButton.onClick.AddListener(btnStartServer_Click);
        createRoomButton.onClick.AddListener(btnCreateRoom_Click);
        refreshRoomButton.onClick.AddListener(btnRefreshRoom_Click);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AssemblyCSharp.LPC_GameServer.DefaultServer.SendGameMessage("sssss");
        }
    }

    public string GetGameTypeName()
    {
        return gameTypeName.text;
    }

    public string GetGameName()
    {
        return gameName.text;
    }

    public void RefreshRoomView(HostData[] roomDataS)
    {
        LobbyRoomItemView[] rooms = roomContentContainer.GetComponentsInChildren<LobbyRoomItemView>();
        for (int i = 0; i < rooms.Length; ++i)
        {
            GameObject.Destroy(rooms[i].gameObject);
        }

        for (int i = 0; i < roomDataS.Length; i++)
        {
            GameObject go = GameObject.Instantiate(roomItemPref);
            go.transform.SetParent(roomContentContainer, false);
            LobbyRoomItemView lriv = go.GetComponent<LobbyRoomItemView>();
            HostData hd = new HostData();
            hd = roomDataS[i];
            lriv.InitRoomItem(hd);
            Button btn = go.GetComponent<Button>();
            if(BindJoinRoomHook != null)
                btn.onClick.AddListener(() => BindJoinRoomHook(hd));
        }

        float h = viewPortContainer.rect.height;
        h /= 5;
        h *= roomDataS.Length;
        roomContentContainer.sizeDelta = new Vector2(viewPortContainer.rect.width, h);
    }

    private void btnStartServer_Click()
    {
        startServerButton.DisableSeconds();

        if (OnClickStartServerHook != null)
            OnClickStartServerHook.Invoke();
    }

    private void btnCreateRoom_Click()
    {
        createRoomButton.DisableSeconds();

        if (OnClickCreateRoomHook != null)
            OnClickCreateRoomHook.Invoke();
    }

    private void btnRefreshRoom_Click()
    {
        refreshRoomButton.DisableSeconds();

        if (OnClickRefreshRoomHook != null)
            OnClickRefreshRoomHook.Invoke();
    }

}
