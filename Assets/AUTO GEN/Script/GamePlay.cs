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
        var newCheck = tubeReceive.ColoringCondition();
        if (newCheck.Count > 0)
        {
            if (newArrGive[0].IsSameColor(newCheck[0]))
            {
                tubeReceive.ReceiveAllAncol(newArrGive);
                tubeGive.MoveTubeBack();
                CheckEffect();

            }
            else
            {
                OnSelectUnChoose();
                tubeGive.MoveTubeBack();
            }
        }
        else
        {
            tubeReceive.ReceiveAllAncol(newArrGive);
            tubeGive.MoveTubeBack();
            CheckEffect();
        }

        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].ResetTube();
        }
    }
    public void CheckEffect()
    {
        var a = tubeGive.transform.position.x;
        var b = tubeReceive.transform.position.x;
        if (a < b)
        {
            tubeGive.EffectRotateRight();
        }
        else
        {
            tubeGive.EffectRotateLeft();
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
                CheckOneTube();
                CheckFullTube();
                break;
            }

        }
    }
    public void CheckOneTube()
    {
        for (int i = 0; i < Tubes.Count; i++)
        {
            var ArrAncolColor = Tubes[i].GetAllAncolSameColor();
            if (ArrAncolColor.Count == 4 )
            {
                Tubes[i].Particice();
            }
            else
            {
                Tubes[i].EndPar();
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
            if (ArrAncolColor.Count == 4 || ArrAncolNoColor.Count == 4)
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
    private void Update()
    {
       
    }
}
