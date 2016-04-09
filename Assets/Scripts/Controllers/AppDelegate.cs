using UnityEngine;
using System.Collections;

public class AppDelegate : MonoBehaviour
{
    public static AppDelegate DefaultManager = null;
    public LogInCanvasControl loginCanvas;
    public LobbyCanvasControl lobbyCanvas;
    public SelectHeroCanvasControl selectCanvas;
    public BattleCanvasController battleCanvas;

    void Awake()
    {
        Application.runInBackground = true;
        DefaultManager = this;
    }

    void Start ()
    {
        //loginCanvas.Show();
        lobbyCanvas.Show();
	}

    public void ChangeCanvas<T, M>(T fromCanvas, M toCanvas) where T : CanvasControl where M : CanvasControl
    {
        fromCanvas.Hide();
        toCanvas.Show();
    }
	
}
