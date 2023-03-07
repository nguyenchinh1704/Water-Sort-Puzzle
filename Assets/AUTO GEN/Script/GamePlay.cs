using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public List<TubeManagement> Tubes;
    public TubeManagement tubeGive;
    public TubeManagement tubeReceive;
    public ControllerGame giveData;

    private void Start()
    {
        Tubes = giveData.listTube;
    }
    public void ResetDataAllTube()
    {
        
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].ResetDataTube();
            /*Tubes[i].EndEffect();*/
        }
    }

    public void ChangeAncol()
    {
       
        var newArrGive = tubeGive.GetAllAncolSameColor();
        var newArrReceive = tubeReceive.GetAllAncolNoColor();
        if (newArrGive.Count <= newArrReceive.Count)
        {
            tubeReceive.ReceiveAllAncol(newArrGive);
            /*tubeGive.AnimationEnd();*/

        }
        else
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
        
        List<TubeManagement> TubeGive = new List<TubeManagement>();
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
            if (Tubes[i].IsHasChange())
            {
                /* Tubes[i].ReadytoChangeGive();*/
                tubeReceive = Tubes[i];
                ChangeAncol();
                /*StartEffect();*/
                CheckFullTube();
                break;
            }

        }
    }
    public void CheckFullTube()
    {
       
        List<TubeManagement> tubeFull = new List<TubeManagement>();
        for (int i = 0; i < Tubes.Count; i++)
        {
            var ArrAncolColor = Tubes[i].GetAllAncolSameColor();
            var ArrAncolNoColor = Tubes[i].GetAllAncolNoColor();
            if (ArrAncolColor.Count == 3 || ArrAncolNoColor.Count == 3)
            {
                tubeFull.Add(Tubes[i]);
                if (tubeFull.Count == Tubes.Count)
                {
                    /*pnVictory.SetActive(true);
                    btnNextLevel.SetActive(true);*/
                }
            }
        }
    }
}
