using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectHeroItemView : MonoBehaviour
{
    public Text nameText;
    public Button changeHeroButton;

    public void InitItem(string heroName)
    {
        nameText.text = heroName;
    }

    public Button GetChangeButton()
    {
        return changeHeroButton;
    }
}
