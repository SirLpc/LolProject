using UnityEngine;

public class DebugManager : MonoBehaviour
{
    #region Singleton
    private DebugManager() { }
    private static DebugManager s_defaultManager = null;
    public static DebugManager DefaultManager
    {
        get {
            if(s_defaultManager == null)
            {
                GameObject s_defaultManager_object = new GameObject("_DebugManager");
                s_defaultManager = s_defaultManager_object.AddComponent<DebugManager>();
            }
            return s_defaultManager;
        }
    }
    #endregion

    private string msg = string.Empty;

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void Log(string msg)
    {
        this.msg += string.Format("{0}\n", msg);
    }
    public void Log(object msgObj)
    {
        Debug.Log(msgObj.ToString());
        this.msg += string.Format("{0}\n", msgObj.ToString());
    }
    public void Clear()
    {
        this.msg = string.Empty;
    }

    void OnGUI()
    {
        if (!string.IsNullOrEmpty(msg))
        {
            GUILayout.Label(msg);
        }
    }
}
