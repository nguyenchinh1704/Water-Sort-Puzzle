using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;
using UnityEngine.UI;

public class ManagementGame : MonoBehaviour
{
    public List<TubeManagement> Tubes;
    public TubeManagement tubeGive;
    public TubeManagement tubeReceive;
    public LoadLevel giveData;
    public UIElement pnVictory;
    public Text textLevel;
    public int count = 0;

    List<int[,]> DataAll = new List<int[,]>();
    private void Start()
    {
        Tubes = giveData.listTube;
    }
    public void ResetDataAllTube()
    {

        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].ResetDataTube();
            Tubes[i].EndPar();
        }

        DataAll.Clear();
    }

    public void BackOneAction()
    { 
        var newData = ReturnDataAll(DataAll[count - 1]);
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].SetColorByData(newData[i]);
        }
        DataAll.Remove(DataAll[count - 1]);
        CheckOneTube();
        count--;
    }
    public List<int[]> ReturnDataAll(int[,] array)
    {
        List<int[]> saveData = new List<int[]>();       
        for (int i = 0; i < Tubes.Count; i++)
        {
            int[] data = new int[tubeGive.listImage.Count];
            for (int j = 0; j < tubeGive.listImage.Count; j++)
            {
                data[j] = array[i, j];
            }
            saveData.Add(data);
        }
        return saveData;
    }


    public void MoveTubeGive()
    {
        Vector3 originalPosition = tubeGive.transform.position;
        if (tubeGive.transform.position.x > tubeReceive.transform.position.x)
        {
            tubeGive.transform.position = new Vector3(tubeReceive.transform.position.x + 10, tubeReceive.transform.position.y + 6, tubeReceive.transform.position.z);
            StartCoroutine(TubeBackPosition(originalPosition));
        }
        else
        {
            tubeGive.transform.position = new Vector3(tubeReceive.transform.position.x - 10, tubeReceive.transform.position.y + 6, tubeReceive.transform.position.z);
            StartCoroutine(TubeBackPosition(originalPosition));
        }

    }
    IEnumerator TubeBackPosition(Vector3 a)
    {

        yield return new WaitForSeconds(1.2f);
        tubeGive.transform.position = a;
    }
    public void LockTube()
    {
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].LockTube();
        }
    }
    public void ChangeAncol()
    {
        var newArrGive = tubeGive.GetAllAncolSameColor();
        var newArrReceive = tubeReceive.GetAllAncolNoColor();
        var newCheck = tubeReceive.ColoringCondition();
        var newData = CheckDataAlltube();
        DataAll.Add(newData);
        if (newCheck.Count > 0)
        {
            if (newArrGive[0].IsSameColor(newCheck[0]))
            {
                tubeReceive.ReceiveAllAncol(newArrGive);
                tubeGive.MoveTubeBack();
                CheckEffect();
                MoveTubeGive();
                LockTube();
                count++;

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
            MoveTubeGive();
            LockTube();
            count++;
        }
        StartCoroutine(UnlockTube());
    }
    IEnumerator UnlockTube()
    {
        yield return new WaitForSeconds(1.5f);
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
                StartCoroutine(CheckFullTube());
                
                break;
            }

        }
    }

    public void SaveLevel()
    {
        PlayerPrefs.SetInt("idLevel", giveData.idLevel);
    }
    public void CheckOneTube()
    {
        for (int i = 0; i < Tubes.Count; i++)
        {
            var ArrAncolColor = Tubes[i].GetAllAncolSameColor();
            if (ArrAncolColor.Count == 4)
            {
                Tubes[i].Particice();
            }
            else
            {
                Tubes[i].EndPar();
            }
        }
    }

    IEnumerator CheckFullTube()
    {
        yield return new WaitForSeconds(1.5f);

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
                    pnVictory.show(true);
                    giveData.StopAllCoroutines();                  
                    textLevel.text = giveData.textLevel.text;
                    SaveLevel();

                }
            }
        }

        if (tubeFull.Count == Tubes.Count)
        {
            for (int i = 0; i < Tubes.Count; i++)
            {
                Tubes[i].EndPar();
            }
        }
    }


    public int[,] CheckDataAlltube()
    {
        int a = Tubes.Count;
        int b = tubeGive.listImage.Count;
        int[,] data = new int[a, b];
        List<int[]> save = new List<int[]>();
        for (int i = 0; i < Tubes.Count; i++)
        {
            save.Add(Tubes[i].CheckColor());
        }
        for (int m = 0; m < a; m++)
        {
            for (int n = 0; n < b; n++)
            {
                data[m, n] = save[m][n];
            }

        }

        return data;
    }
    private void Update()
    {

    }
}
