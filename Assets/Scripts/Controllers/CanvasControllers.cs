using UnityEngine;
using System;

[Serializable]
public class CanvasControl
{
    [SerializeField]
    public Canvas prefab;
    Canvas m_Canvas;

    public Canvas canvas { get { return m_Canvas; } }

    public virtual void Show()
    {
        if (prefab == null)
            return;

        if (m_Canvas != null)
            return;

        m_Canvas = (Canvas)GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        GameObject.DontDestroyOnLoad(m_Canvas.gameObject);
    }

    public void Hide()
    {
        if (m_Canvas == null)
            return;

        GameObject.Destroy(m_Canvas.gameObject);
        m_Canvas = null;
    }

    public virtual void OnLevelWasLoaded()
    {
    }
}
