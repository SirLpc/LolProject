using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginCanvasHook : MonoBehaviour
{
    public delegate void CanvasHook();

    public CanvasHook OnClickLogInHook;

    public Text userNameInput;
    public Text serverNameInput;
    public Dropdown serverNameDropdown;

    public Button loginButton;

    private void Awake()
    {
        loginButton.onClick.AddListener(btnLogIn_Click);
    }

    public string GetUserName()
    {
        return userNameInput.text;
    }

    public string GetServerName()
    {
        return serverNameInput.text;
    }

    public void SetServerNameDropdown(List<string> names)
    {
        serverNameDropdown.ClearOptions();
        serverNameDropdown.AddOptions(names);
    }

    private void btnLogIn_Click()
    {
        loginButton.DisableSeconds();

        if (OnClickLogInHook != null)
            OnClickLogInHook.Invoke();
    }

}
