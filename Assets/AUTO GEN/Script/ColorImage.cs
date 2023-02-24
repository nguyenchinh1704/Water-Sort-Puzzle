using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorImage : MonoBehaviour
{
    public Image img;
    bool hasColor;
    string data;
    //public void SetColor(Color color)
    //{
    //    img.color = color;
    //    hasColor = true;       
    //    img.gameObject.SetActive(true);

    //}

    public static Color GetColorByID(string colorID)
    {
        switch (colorID)
        {
            case "R": return Color.red;
            case "G": return Color.green;
            case "B": return Color.blue;
            case "Y": return Color.yellow;
            default: return Color.black;
        }
    }
    public void SetColor(string color)
    {
        img.color = GetColorByID(color);
        data = color;
        hasColor = true;
        img.gameObject.SetActive(true);

    }
    public void RemoveColor()
    {
        hasColor = false;
        img.gameObject.SetActive(false);

    }

    public void Check()
    {

    }

    internal bool IsHasColor()
    {
        return hasColor;
    }

    internal bool IsSameColor(ColorImage image)
    {
        return data == image.data;
    }
    public string ReturnColor()
    {
        return data;
    }
    // lay ra nung dung dich cung mau ten cung


}
