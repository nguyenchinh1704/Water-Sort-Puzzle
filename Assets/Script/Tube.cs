using EazyEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityExtensions.Tween;

public class Tube : MonoBehaviour
{
    public List<Ancol> listAncols;
    public CardTestTube card;
    bool isChoose = false ;
    bool isChange = false ;
    public GameObject btnChoose, btnUnChoose, btnChange;
    public GameObject particeSys;



    #region buttonChoose
    [SerializeField] UIElement showChoose;
    public TweenPlayer localTweenShow;


   
    public void ResetDataTube()
    {
        int count = 0;
        for (int i = 0; i < card.datas.Length; i++)
        {
            listAncols[i].SetColor(card.datas[i]);
            count++;

        }
        for (int i = count; i < listAncols.Count; i++)
        {
            listAncols[i].RemoveColor();
        }
    }
    public void StartEffect()
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
        

    #endregion

    #region ButtonChange

    public UIElement showChange;
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
    }
    #endregion


    public Ancol GettopAncol()
    {
        for (int i = 0; i <= listAncols.Count; i++)
        {
            if (listAncols[i].IsHasColor())
            {
                return listAncols[i];
            }
        }
        return null;
    }
    public List<Ancol> GetAllAncolSameColor()
    {
        List<Ancol> result = new List<Ancol>();
        int i = listAncols.Count - 1;
        for (; i >= 0; i--)
        {
            if (listAncols[i].IsHasColor())
            {
                result.Add(listAncols[i]);
                i--;
                break;
            }
        }
        if(result.Count > 0)
        {
            for (; i >= 0; i--)
            {
                if (!listAncols[i].IsSameColor(result[0]))
                {
                    break;
                }
                result.Add(listAncols[i]);
            }
        }
        return result;
        
    }

    public List<Ancol> GetAllAncolNoColor()
    {
        List<Ancol> nocolor = new List<Ancol>();
        for (int i = 0; i < listAncols.Count; i++)
        {
            if(listAncols[i].IsHasColor() == false)
            {
                nocolor.Add(listAncols[i]);
            }
        }
        return nocolor;
    }

    public void ReceiveAllAncol(List<Ancol> tubeAncol )
    {

        for (int i = 0; i < tubeAncol.Count; i++)
        {
            ReceiveOneAncol(tubeAncol[i]);
        }

    }
    public void ReceiveOneAncol(Ancol ancol)
    { 
       for(int i = 0; i< listAncols.Count; i++)
        {
            if(listAncols[i].IsHasColor() == false)
            {
                listAncols[i].SetColor(ancol.ReturnColor());
                ancol.RemoveColor();
                break;
            }
        }
    }
    private void Start()
    {
        int count = 0;
        for (int i = 0; i < card.datas.Length; i++)
        {
            listAncols[i].SetColor(card.datas[i]);
            count++;
          
        }
        for (int i = count; i < listAncols.Count; i++)
        {
            listAncols[i].RemoveColor();
        }
    }
    internal bool IsHasChoose()
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
    }

}
