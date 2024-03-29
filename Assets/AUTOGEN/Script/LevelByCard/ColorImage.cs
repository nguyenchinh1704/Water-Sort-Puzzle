using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EazyEngine.UI;
using UnityExtensions.Tween;

public class ColorImage : MonoBehaviour
{
    public Image img;
    bool hasColor;
    /*string data;*/
    int data_int;
    bool hasActivel;
    public UIElement show, endShow;
    /*public void SetColor(Color color)
    {
        img.color = color;
        hasColor = true;
        img.gameObject.SetActive(true);

    }*/

    /*public static Color GetColorByID(string colorID)
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
    }*/
    /*public void SetColor(string color)
    {
        img.color = GetColorByID(color);
        data = color;
        hasColor = true;
        img.gameObject.SetActive(true);

    }*/
    public const int NO_COLOR = 0;

    // Set value<int> return color Image
    public static Color GetColorByID_Int(int colorID) 
    {
        switch (colorID)
        {
            case 1: return Color.green;
            case 2: return Color.blue;
            case 3: return Color.yellow;
            case 4: return Color.gray; 
            case 5: return Color.cyan;
            case 6: return Color.magenta;
            case 7: return Color.black;
            case 8: return Color.red;
            default: return Color.white;
        }
    }

    // phuong thuc truyen mau
    public void SetColor(int color)
    {      
        img.color = GetColorByID_Int(color);
        data_int = color;
        if (color == 0)
        {
            RemoveColor();
        }
        else
        {
            hasColor = true;
            show.show();
            /*img.gameObject.SetActive(true);*/
            hasActivel = true;
        }


    }
    // Image dong nuoc do xuong
    public void SetColorFlow(int color)
    {
        img.color = GetColorByID_Int(color);
        show.show();
    }

    // Kiem tra va Off cac anh khong mau
    public void RemoveColor()
    {
        hasColor = false;
        /*show.close();*/
        endShow.show();
        /*img.gameObject.SetActive(false);*/
        hasActivel = false;
    }

    public void Check(int valueData)
    {
        if (valueData == 0)
        {
            /*img.gameObject.SetActive(false);*/
            /*show.close();*/
            endShow.show();
            hasActivel = false;
        }
    }
    public Image SetSize(int a, int b)
    {
        img.rectTransform.sizeDelta = new Vector2(a, b);
        return img;
    }
    internal bool IsHasColor()
    {
        return hasColor;
    }
    internal bool IsHasActive()
    {
        return hasActivel;
    }

    internal bool IsSameColor(ColorImage image)
    {
        return data_int == image.data_int;
    }
    public int ReturnColor()
    {
        return data_int;
    }
    // lay ra nung dung dich cung mau ten cung


}
