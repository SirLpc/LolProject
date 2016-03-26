using UnityEngine;
using System.Collections;

public class AppDelegate : MonoBehaviour
{
    public LogInCanvasControl loginCanvas;
    public LobbyCanvasControl lobbyCanvas;

    void Awake()
    {
        Application.runInBackground = true;
    }

    void Start ()
    {
        //loginCanvas.Show();
        lobbyCanvas.Show();
	}
	
}
