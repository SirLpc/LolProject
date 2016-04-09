using UnityEngine;
using System.Collections;

public class HeroData
{
    private static HeroData defaultData = null;
    public static HeroData DefaultData
    {
        get
        {
            if (defaultData == null)
                defaultData = new HeroData();
            return defaultData;
        }
    }
    private HeroData()
    {
        Heros = new HeroInfo[10];
        for (int i = 0; i < Heros.Length; i++)
        {
            HeroInfo hero = new HeroInfo();
            hero.ID = i;
            hero.Name = string.Format("TestHero[{0}]", i.ToString("00"));
            Heros[i] = hero;
        }
    }

    public HeroInfo[] Heros;
}
