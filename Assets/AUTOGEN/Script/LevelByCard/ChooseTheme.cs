using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTheme : MonoBehaviour
{
    public Image imageTheme;
    public GroupTheme theme;
    public Sprite Imgtheme;
    public void ButtonChoose()
    {      
        imageTheme.color = Color.yellow;
    }

    public void BackColorTheme()
    {
        for (int i = 0; i < theme.listImageTheme.Count; i++)
        {
            if (theme.listImageTheme[i].imageTheme != imageTheme)
            {
                theme.listImageTheme[i].imageTheme.color = Color.white;
            }

        }
    }
}
