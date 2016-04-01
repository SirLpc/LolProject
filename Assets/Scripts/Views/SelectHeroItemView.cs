using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectHeroItemView : MonoBehaviour
{
    public Text nameText;

    public void InitItem(string userName)
    {
        nameText.text = userName;
    }
}
