using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public List<Tube> Tubes;
    bool isSelected;
    bool isChanged;
    public Tube tubeGive;
    public Tube tubeReceive;
    int count = 0;
    public GameObject level;
    public GameObject levelNext;
    public GameObject pnVictory;
    public GameObject btnNextLevel, text;


    public void ResetDataAllTube()
    {
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].ResetDataTube();
            Tubes[i].EndEffect();
        }
    }


    public void TakeAStepBack()
    {
       
    }
    public void ShowText()
    {
        text.SetActive(true);
        StartCoroutine(OffText());
    }

    IEnumerator OffText()
    {
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
    }
    public void ChangeAncol()
    {
        var newArrGive = tubeGive.GetAllAncolSameColor();
        var newArrReceive = tubeReceive.GetAllAncolNoColor();
        if (newArrGive.Count <= newArrReceive.Count)
        {
            tubeReceive.ReceiveAllAncol(newArrGive);
            tubeGive.AnimationEnd();

        } else
        {
            OnSelectUnChoose();
        }
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].ResetTube();
            Tubes[i].close();
        }
    }
    public void OnSelectTubeGive()
    {
        List<Tube> TubeGive = new List<Tube>();
        for (int i = 0; i < Tubes.Count; i++)
        {
            if (Tubes[i].IsHasChoose() == true)
            {
                TubeGive.Add(Tubes[i]);
                tubeGive = TubeGive[0];
                tubeGive.ReadytoChangeGive();
                break;
            }

        }
    
        List<Tube> TubeReceive = new List<Tube>();
        for (int i = 0; i < Tubes.Count; i++)
        {
            if (TubeGive.Count == 1 && Tubes[i] != TubeGive[0])
            {                
                Tubes[i].ReadytoChangeReceive();
            }
            
        }
        
    }

    public void OnSelectUnChoose()
    {
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].ResetTube();
        }
    }

    public void OnSelectChange()
    {
        for (int i = 0; i < Tubes.Count; i++)
        {
            if(Tubes[i].IsHasChange ())
            {
               /* Tubes[i].ReadytoChangeGive();*/
                tubeReceive = Tubes[i];
                ChangeAncol();
                StartEffect();
                CheckFullTube();
                break;
            }
           
        }
    }

    public void StartEffect()
    {
        List<Tube> tubeFull = new List<Tube>();
        for (int i = 0; i < Tubes.Count; i++)
        {
            var ArrAncolColor = Tubes[i].GetAllAncolSameColor();
            if (ArrAncolColor.Count == 3)
            {
                Tubes[i].StartEffect();
            }
            else
            {
                Tubes[i].EndEffect();
            }
        }
    }
    public void CheckFullTube()
    {
        List<Tube> tubeFull = new List<Tube>();
        for (int i = 0; i < Tubes.Count; i++)
        {
            var ArrAncolColor = Tubes[i].GetAllAncolSameColor();
            var ArrAncolNoColor = Tubes[i].GetAllAncolNoColor();
            if (ArrAncolColor.Count == 3 || ArrAncolNoColor.Count == 3)
            {
                tubeFull.Add(Tubes[i]);
                    if(tubeFull.Count == Tubes.Count)
                    {
                        pnVictory.SetActive(true);
                        btnNextLevel.SetActive(true);
                    }
            }
        }
    }
    public void BtnNextLevel()
    {
        level.SetActive(false);
        pnVictory.SetActive(false);
        levelNext.SetActive(true);
        btnNextLevel.SetActive(false);
    }
    private void Update()
    {
       
    }

}
