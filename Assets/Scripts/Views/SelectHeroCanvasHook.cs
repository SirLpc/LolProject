using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

public class SelectHeroCanvasHook : MonoBehaviour
{
    public delegate void CanvasHook();
    public delegate void CanvasChangeHeroHook(HeroInfo hero);
    public CanvasChangeHeroHook OnChangeHeroHook = null;
    public CanvasHook OnClickReadyHook = null;
    public CanvasHook StartGameWhenCountFinishHook = null;

    public RectTransform[] arrayOfSeatPos;
    public RectTransform heroContentContainer;
    public Button readyButton;
    public Text countText;

    public GameObject heroItemPref;

    private void Awake()
    {
        readyButton.onClick.AddListener(btnReady_Click);
    }

    public void SpawnPlayerToSeat_SYNC()
    {
        LPC_GameServer.DefaultServer.SpawnPlayerToSeat_RPC(
            UserInfo.DefaultUser.Order, UserInfo.DefaultUser.Name);
    }

    public RectTransform[] GetSeatPosS()
    {
        return arrayOfSeatPos;
    }

    public void InitHeroSelector()
    {
        for (int i = 0; i < HeroData.DefaultData.Heros.Length; i++)
        {
            HeroInfo hero = new HeroInfo();
            hero = HeroData.DefaultData.Heros[i];
            Transform tr = GameObject.Instantiate(heroItemPref).transform;
            tr.SetParent(heroContentContainer, false);
            SelectHeroItemView shiv = tr.GetComponent<SelectHeroItemView>();
            shiv.InitItem(hero.Name);
            shiv.GetChangeButton().onClick.AddListener(() => {
                OnChangeHeroHook.Invoke(hero);
            });
        }
    }

    private int countTimer;
    public void CountDown(int count)
    {
        countTimer = count;
        if (count == Tags.StopCountFlag)
        {
            CancelInvoke("IvCountDown");
        }
        else
        {
            InvokeRepeating("IvCountDown", 0f, 1f);
        }
    }
    private void IvCountDown()
    {
        if(countTimer <= 0)
        {
            CancelInvoke("IvCountDown");
            if (StartGameWhenCountFinishHook != null)
                StartGameWhenCountFinishHook.Invoke();
        }
        else
        {
            countTimer--;
            countText.text = countTimer.ToString();
        }
    }

    private void btnReady_Click()
    {
        readyButton.interactable = false;
        if (OnClickReadyHook != null)
            OnClickReadyHook.Invoke();
    }
}
