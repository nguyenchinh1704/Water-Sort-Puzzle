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
    int data_int;
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
            case "X": return Color.gray;
            case "W": return Color.white;
            case "P": return Color.magenta;
            case "K": return Color.black;
            default: return Color.clear;
        }
    }
    public void SetColor(string color)
    {
        img.color = GetColorByID(color);
        data = color;
        hasColor = true;
        img.gameObject.SetActive(true);

    }
    public static Color GetColorByID_Int(int colorID)
    {
        switch (colorID)
        {
            case 0: return Color.red;
            case 1: return Color.green;
            case 2: return Color.blue;
            case 3: return Color.yellow;
            case 4: return Color.gray;
            case 5: return Color.white;
            case 6: return Color.magenta;
            case 7: return Color.black;
            default: return Color.clear;
        }
    }
    public void SetColor(int color)
    {
        img.color = GetColorByID_Int(color);
        data_int = color;
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
