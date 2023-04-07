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
    public UIElement pnVictory, EffectVc;
    public Text textLevel;
    public int count = 0;
    int check;
    public AudioSource soundRecive;
    public AudioSource soundVictory;



    List<int[,]> DataAll = new List<int[,]>();
    List<TubeManagement> Give = new List<TubeManagement>();
    public List<Vector3> dataPosition = new List<Vector3>();
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
            Tubes[i].ResetTube();
            Tubes[i].EndChoose();
        }

        DataAll.Clear();
        Give.Clear();
        dataPosition.Clear();
        count = 0;
    }

    public void BackOneAction() // Undo Action
    {
        var newData = ReturnDataAll(DataAll[count - 1]);
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].SetColorByData(newData[i]);
        }
        DataAll.Remove(DataAll[count - 1]);
        StartCoroutine(CheckOneTube());
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
    public void CheckPositionTube()  //Kiem tra va lay ra vi tri ban dau cua tube
    {
        Vector3 originalPosition;
        for (int i = 0; i < Tubes.Count; i++)
        {
            originalPosition = Tubes[i].transform.position;
            dataPosition.Add(originalPosition);
        }
    }


    IEnumerator BackPosition(TubeManagement tube)
    {
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < Tubes.Count; i++)
        {
            if(tube == Tubes[i])
            Tubes[i].transform.position = dataPosition[i];
        }
    }

    public void MoveTubeGive()          // di chuyen tubeGive den vi tri tubeReceive
    {
        Vector3 originalPosition = tubeGive.transform.position;
        if (tubeGive.transform.position.x > tubeReceive.transform.position.x)
        {
            tubeGive.transform.position = new Vector3(tubeReceive.transform.position.x + 10, tubeReceive.transform.position.y + 11, tubeReceive.transform.position.z);
        }
        else
        {
            tubeGive.transform.position = new Vector3(tubeReceive.transform.position.x - 10, tubeReceive.transform.position.y + 11, tubeReceive.transform.position.z);
        }

    }

    public Vector3 ReturnPosition() // tra ong tube ve vi tri ban dau
    {
        Vector3 position = new Vector3(0, 0, 0);
        for (int i = 0; i < Tubes.Count; i++)
        {
            if (tubeGive == Tubes[i])
            {
                position = dataPosition[i];
            }
        }

        return position;
    }


    IEnumerator CheckTubeHiddenChange()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].CheckImageHidden();
        }
    }

    IEnumerator EffectSound()
    {
        yield return new WaitForSeconds(1f);
        soundRecive.Stop();
    }

    public void ChangeAncol()                                   // phuong thuc do mau
    {
        var newArrGive = tubeGive.GetAllAncolSameColor();
        var newArrReceive = tubeReceive.GetAllAncolNoColor();
        var newCheck = tubeReceive.ColoringCondition();
        var newData = CheckDataAlltube();
        check = CheckImageIntube(tubeGive);
        Give.Add(tubeGive);
        DataAll.Add(newData);
        if (newArrGive.Count > 0 && newCheck.Count > 0)
        {
            if (newArrGive[0].IsSameColor(newCheck[0]) && newArrReceive.Count > 0)
            {
                tubeReceive.StartChange(newArrGive);
                tubeGive.EndChoose();
                StartCoroutine(CheckTubeHiddenChange());
                soundRecive.Play();
                StartCoroutine(EffectSound());
                MoveTubeGive();
                CheckEffect();
                StartCoroutine(BackPosition(tubeGive));
                count++;
            }
            else
            {
                OnSelectUnChoose();
                tubeGive.EndChoose();
                Give.Remove(tubeGive);
                DataAll.Remove(newData);

            }
        }
        else if (newArrGive.Count == 0)
        {
            tubeGive.EndChoose();
            Give.Remove(tubeGive);
            OnSelectUnChoose();
            DataAll.Remove(newData);
        }
        else
        {
            tubeReceive.StartChange(newArrGive);
            tubeGive.EndChoose();
            StartCoroutine(CheckTubeHiddenChange());
            soundRecive.Play();
            StartCoroutine(EffectSound());
            MoveTubeGive();
            CheckEffect();
            StartCoroutine(BackPosition(tubeGive));
            count++;
        }

    }
    public int CheckLengthImageFlow(TubeManagement tube)                  //kiem tra Image dong nuoc do xuong
    {
        int count = 0;
        for (int i = 0; i < tube.listImage.Count; i++)
        {
            if (tube.listImage[i].IsHasColor() == true)
            {
                count++;
            }
        }
        return count;
    }

    public int CheckImageIntube(TubeManagement tube)
    {
        int data = 0;
        int[] array = new int[tube.listImage.Count];
        array = tube.CheckColor();
        for (int i = array.Length - 1; i >= 0; i--)
        {
            if (array[i] != 0)
            {
                data = array[i];
                break;
            }
        }
        return data;
    }
    IEnumerator SetActiveImage(ColorImage image)
    {
        var m = CheckLengthImageFlow(tubeReceive);
        yield return new WaitForSeconds(0.4f);
        image.SetSize(14, 590 - 110 * m);
        image.SetColorFlow(check);
        StartCoroutine(AutoOff(image));
    }

    IEnumerator AutoOff(ColorImage image)
    {
        yield return new WaitForSeconds(0.4f);
        image.gameObject.SetActive(false);
    }
    public void EffectReceive(TubeManagement tube)
    {
        var array = tube.GetAllAncolNoColor();
        for (int i = 0; i < array.Count; i++)
        {
            array[i].show.show();
        }
    }

    public void EffectGive(TubeManagement tube, List<ColorImage> list)
    {
        var array = tube.GetAllAncolSameColor();
        int i = array.Count - 1;
        int dem = 0;
        for (; i > 0; i--)
        {
            array[i].endShow.show();
            dem++;
            if (dem == list.Count)
            {
                break;
            }
        }
    }
    public void CheckEffect()            // kiem tra vi tri tubeGive va tubeReceive de show animation
    {
        var a = tubeGive.transform.position.x;
        var b = tubeReceive.transform.position.x;
        if (a < b)
        {
            tubeGive.ShowRight();
            StartCoroutine(SetActiveImage(tubeGive.right));
        }
        else
        {
            if (a == b)
            {
                tubeGive.ShowRight();
                StartCoroutine(SetActiveImage(tubeGive.right));
            }
            else
            {
                tubeGive.ShowLeft();
                StartCoroutine(SetActiveImage(tubeGive.left));
            }
        }

    }
    public void OnSelectTubeGive()             // Button choose
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

    public void OnSelectUnChoose()                // Bt UnChoose
    {

        for (int i = 0; i < Tubes.Count; i++)
        {
            Tubes[i].ResetTube();
        }
    }

    public void OnSelectChange()           // Bt Change
    {
        for (int i = 0; i < Tubes.Count; i++)
        {
            CheckPositionTube();
            if (Tubes[i].IsHasChange())
            {
                tubeReceive = Tubes[i];
                ChangeAncol();
                OnSelectUnChoose();               
                StartCoroutine(CheckOneTube());
                StartCoroutine(CheckFullTube());
                break;
            }
        }
    }

    public void SaveLevel()
    {       
        PlayerPrefs.SetInt("idLevel", giveData.idLevel);
    }
    IEnumerator CheckOneTube()                     // Check WinTube
    {
        yield return new WaitForSeconds(1.2f);
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

    IEnumerator CheckFullTube()               // Check WinLevel
    {
        yield return new WaitForSeconds(2f);

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
                    pnVictory.show();
                    soundVictory.Play();
                    isShow = true;
                    giveData.StopAllCoroutines();
                    textLevel.text = giveData.textLevel.text;
                    DataAll.Clear();
                    Give.Clear();
                    dataPosition.Clear();
                    count = 0;
                    SaveLevel();
                    StartCoroutine(LoopEffect());
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
    bool isShow = false;
    IEnumerator LoopEffect()
    {
        while (isShow == true)
        {
            EffectVc.show();
            yield return new WaitForSeconds(3f);
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
