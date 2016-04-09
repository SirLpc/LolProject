using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectPlayerItemView : MonoBehaviour
{
    public Text nameText;

    public void InitItem(string userName)
    {
        nameText.text = userName;
    }

    public void ChangeHero(int heroId)
    {
        nameText.text = HeroData.DefaultData.Heros[heroId].Name;
    }

    public void Ready()
    {
        nameText.text = nameText.text + "[Ready]";
    }
}
