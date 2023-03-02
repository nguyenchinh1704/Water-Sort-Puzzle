using EazyEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityExtensions.Tween;

public class TubeManagement : MonoBehaviour
{
    public List<ColorImage> listImage;
    /*public int maxItem;*/
    public TubeModel card;
    /*bool isChoose = false;
    bool isChange = false;
    public GameObject btnChoose, btnUnChoose, btnChange;
    public GameObject particeSys;*/



    #region buttonChoose
    /*[SerializeField] UIElement showChoose;
    public TweenPlayer localTweenShow;*/



    public void ResetDataTube()
    {
        int count = 0;
        for (int i = 0; i < card.Color.Length; i++)
        {
            listImage[i].SetColor(card.Color[i]);
            count++;

        }
        for (int i = count; i < listImage.Count; i++)
        {
            listImage[i].RemoveColor();
        }
    }
    /*public void StartEffect()
    {
        particeSys.SetActive(true);
    }
    public void EndEffect()
    {
        particeSys.SetActive(false);
    }

    public void Choose()
    {

        showChoose.show();
        btnChoose.SetActive(false);
        isChoose = true;
    }
    public void UnChoose()
    {
        close();
        btnChoose.SetActive(true);
        btnUnChoose.SetActive(false);
        isChoose = false;

    }

    public void close()
    {
        localTweenShow.Stop();
        localTweenShow.normalizedTime = 0;
    }
*/

    #endregion

    #region ButtonChange

   /* public UIElement showChange;
    public TweenPlayer localTweenShowChange;
    public TweenPlayer localTweenShow1Change;



    public void AnimationEnd()
    {
        showChange.show();
        StartCoroutine(AutoOff());
    }
    public void Change()
    {


        btnChange.SetActive(false);
        isChange = true;
    }
    IEnumerator AutoOff()
    {
        yield return new WaitForSeconds(1f);
        closeChange();
        close1();
    }
    public void closeChange()
    {
        localTweenShowChange.Stop();
        localTweenShowChange.normalizedTime = 0;
    }
    public void close1()
    {
        localTweenShow1Change.Stop();
        localTweenShow1Change.normalizedTime = 0;
    }*/
    #endregion


    public ColorImage GettopAncol()
    {
        for (int i = 0; i <= listImage.Count; i++)
        {
            if (listImage[i].IsHasColor())
            {
                return listImage[i];
            }
        }
        return null;
    }
    public List<ColorImage> GetAllAncolSameColor()
    {
        List<ColorImage> result = new List<ColorImage>();
        int i = listImage.Count - 1;
        for (; i >= 0; i--)
        {
            if (listImage[i].IsHasColor())
            {
                result.Add(listImage[i]);
                i--;
                break;
            }
        }
        if (result.Count > 0)
        {
            for (; i >= 0; i--)
            {
                if (!listImage[i].IsSameColor(result[0]))
                {
                    break;
                }
                result.Add(listImage[i]);
            }
        }
        return result;

    }

    public List<ColorImage> GetAllAncolNoColor()
    {
        List<ColorImage> nocolor = new List<ColorImage>();
        for (int i = 0; i < listImage.Count; i++)
        {
            if (listImage[i].IsHasColor() == false)
            {
                nocolor.Add(listImage[i]);
            }
        }
        return nocolor;
    }

    public void ReceiveAllAncol(List<ColorImage> tubeAncol)
    {

        for (int i = 0; i < tubeAncol.Count; i++)
        {
            ReceiveOneAncol(tubeAncol[i]);
        }

    }
    public void ReceiveOneAncol(ColorImage image)
    {
        for (int i = 0; i < listImage.Count; i++)
        {
            if (listImage[i].IsHasColor() == false)
            {
                listImage[i].SetColor(image.ReturnColor());
                image.RemoveColor();
                break;
            }
        }
    }
    public void SetColorTube(TubeModel card)
    {
        this.card = card;
        int count = 0;
        for (int i = 0; i < card.Color.Length; i++)
        {
            listImage[i].SetColor(card.Color[i]);
            count++;

        }
        for (int i = count; i < listImage.Count; i++)
        {
            listImage[i].RemoveColor();
        }
    }

    //public void SetColorTube1(TubeData card1)
    //{
    //    this.card = card1;
    //    int count = 0;
    //    for (int i = 0; i < listImage.Count; i++)
    //    {
    //        listImage[i].SetColor(card1.datas[i]);
    //        count++;

    //    }

    //}
    //public void Randomcolor(TubeData card)
    //{
    //    this.card = card;
    //    int a = 0;
    //    for (int i = 0; i < listImage.Count; i++)
    //    {
    //        if(listImage[i].IsHasColor() == true)
    //        {
    //            a++;
    //        }
    //    }
    //    if ( a < 4 )
    //    {
    //        for (int i = 0; i < card.datas.Length; i++)
    //        {
    //            var c = UnityEngine.Random.Range(0, listImage.Count);
    //            if (listImage[c].IsHasColor() == false)
    //            {
    //                listImage[c].SetColor(card.datas[i]);
                    
    //            }
    //        }
           
    //    }

    //}
    public int id;
    public int[] Color;
    public int maxColor;
    /*public TubeModel(int id, int maxColor)
    {
        this.id = id;
        this.maxColor = maxColor;
        Color = new int[maxColor];
    }*/
    public void RandomColor(int colorNum)
    {
        Color = new int[maxColor];

        for (int i = 0; i < Color.Length; i++)
        {
            Color[i] = UnityEngine.Random.Range(0, colorNum);
        }
    }

    public InputField tubeNumber;
    public InputField colorNumber;
   /* public void Colorize(TubeData card)
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        TubeManagement listImage = new TubeManagement();
        for (int i = 0; i < colorNum; i++)
        {
            if (listTube[i].IsHasFull() != true)
            {
                var c = UnityEngine.Random.Range(0, 3);
                if (listImage.listImage[c].IsHasColor() == false)
                {
                    listImage.listImage[c].SetColor(card.datas[i]);
                }
            }
        }
    }*/
    /* public void SetColorRandom1(TubeData card1)
     {
         this.card = card1;
         int count = 0;
         for (int i = 0; i < listImage.Count; i++)
         {
             var a = UnityEngine.Random.Range(0, card1.datas.Length);
             listImage[i].SetColor(card1.datas[a]);
             count++;
         }
         for (int i = count; i < listImage.Count; i++)
         {
             listImage[i].RemoveColor();
         }
     }*/
    /*public void SetColorRandom(TubeData card2)
    {
        this.card = card2;
        int count = 0;
        for (int i = 0; i < card.datas.Length; i++) 
        {
            var a = UnityEngine.Random.Range(0, card2.datas.Length);
            listImage[i].SetColor(card2.datas[a]);
            count++;
        }
        for (int i = count; i < listImage.Count; i++)
        {
            listImage[i].RemoveColor();
        }
    }*/
    /*internal bool IsHasChoose()
    {
        return isChoose;
    }
    internal bool IsHasChange()
    {
        return isChange;
    }
    public void ResetTube()
    {
        isChoose = false;
        isChange = false;
        btnUnChoose.SetActive(false);
        btnChange.SetActive(false);
        btnChoose.SetActive(true);

    }
    public void ReadytoChangeReceive()
    {
        isChoose = false;
        btnUnChoose.SetActive(false);
        btnChange.SetActive(true);
        btnChoose.SetActive(false);
    }
    public void ReadytoChangeGive()
    {
        isChoose = true;
        isChange = false;
        btnUnChoose.SetActive(true);
        btnChange.SetActive(false);
        btnChoose.SetActive(false);
    }*/

}
