﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EazyEngine.UI;
using UnityExtensions.Tween;

public class LoadLevel : MonoBehaviour
{
    public GroupTheme theme;
    public Level ActiveLevel;
    public Image backGroundLevel;
    public GameObject tube,tubeHidden,tubeContainer;
    public Text textLevel;
    public List<TubeManagement> listTube = new List<TubeManagement>();
    public AllLevelData card;
    public int idLevel = 0;
    [SerializeField] private Text timer;
    [SerializeField] private Image imageLight;
    [SerializeField] UIElement pnShow;
    public UIElement pnVictory;
    public GameObject Clock;
    public AllLevelData card1;
    public ManagementGame game;
    public int count = 0;
    public GameObject pnWarning;
    int i = 5;
    public AudioSource soundDefeat;


    IEnumerator UpdateTimer(int time) // Coroutine clockCutdown
    {
        int totalTime = time;
        while (time >= 0)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float sec = Mathf.FloorToInt(time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, sec);
            imageLight.fillAmount = Mathf.InverseLerp(0, totalTime, time);
            time--;
            count++;
            yield return new WaitForSeconds(1f);
            if (time < 11)
            {
                ControlWarning(time);
            }
            if (time == 10)
            {
                timer.color = Color.red;
            }
            if (time == 0)
            {
                StartCoroutine(AutoBackColor());
            }
        }

    }

    public void ControlWarning(int time) //Warning time
    {
        int i = time;
        for (; i >= 0; i--)
        {
            StartCoroutine(OnWarning());
        }
    }
    IEnumerator OnWarning()
    {
        yield return new WaitForSeconds(0f);
        pnWarning.SetActive(true);
        StartCoroutine(OffWarning());
    }
    IEnumerator OffWarning()
    {
        yield return new WaitForSeconds(0.3f);
        pnWarning.SetActive(false);
    }
    IEnumerator AutoBackColor()
    {
        yield return new WaitForSeconds(1f);
        timer.color = Color.yellow;
        StartCoroutine(AutoShow());
    }

    IEnumerator AutoShow()
    {
        yield return new WaitForSeconds(1.5f);
        pnShow.show();
        soundDefeat.Play();
    }

    /*public void ButtonRestart()
    {
        pnShow.close();
        for (int i = 0; i < listTube.Count; i++)
        {
            listTube[i].ResetDataTube();
            listTube[i].transform.position = game.dataPosition[i];
            listTube[i].EndPar();
            listTube[i].ResetTube();
        }
        game.Tubes  = listTube;
        game.count = 0;
        count = 0;
    }*/
    public void ButtonRestart() // Reload in leve
    {
        StopAllCoroutines();
        pnShow.close();
        var oldData = SaveOldData(ActiveLevel);
        Level newLevel = CreatNewData(ActiveLevel, oldData);
        newLevel.totalTime = ActiveLevel.totalTime;
        foreach(Transform child in tubeContainer.transform)
        {
            Destroy(child.gameObject);
        }
        if((idLevel -1) % i == 0 && (idLevel -1) >0)
        {
            GenTubeHidden(newLevel);
        }else
        {
            GenTubes(newLevel);
        }
        /*tubeContainer.transform.DetachChildren();*/
        /*StartCoroutine(UpdateTimer(newLevel.totalTime));*/
        ActiveLevel = newLevel;
        timer.color = Color.yellow;
        game.Tubes = listTube;
        game.count = 0;
        count = 0;
        Debug.Log(idLevel);
    }


    public void GenTubes(Level level) // Gen level by idLevel
    {
        listTube.Clear();
        idLevel = PlayerPrefs.GetInt("idLevel");
        for (int i = 0; i < level.listTubeData.Count; i++)
        {
            GameObject tubeClone = Instantiate(tube, tubeContainer.transform);
            tubeClone.name = "Tube" + i;
            TubeManagement newtubeClone = tubeClone.GetComponent<TubeManagement>();
            listTube.Add(newtubeClone);
            listTube[i].SetColorTube(level.listTubeData[i]);
            listTube[i].CheckImageHidden();

        }
        textLevel.text = "LEVEL " + (PlayerPrefs.GetInt("idLevel") + 1);
        StartCoroutine(UpdateTimer(level.totalTime));
        ActiveLevel = level;
        idLevel++;

    }
    public void GenTubeHidden(Level level) // Gen level by idLevel // tubeHidden
    {
        listTube.Clear();
        idLevel = PlayerPrefs.GetInt("idLevel");
        for (int i = 0; i < level.listTubeData.Count; i++)
        {
            GameObject tubeClone = Instantiate(tubeHidden, tubeContainer.transform);
            tubeClone.name = "Tube" + i;
            TubeManagement newtubeClone = tubeClone.GetComponent<TubeManagement>();
            listTube.Add(newtubeClone);
            listTube[i].SetColorTube(level.listTubeData[i]);
            listTube[i].CheckImageHidden();
        }
        textLevel.text = "LEVEL " + (PlayerPrefs.GetInt("idLevel") + 1);
        StartCoroutine(UpdateTimer(level.totalTime));
        ActiveLevel = level;
        idLevel++;

    }

    public void ButtonNextLevel()
    {
        if(idLevel<card.listLevel.Count) // Next level in DataLevel
        {
            count = 0;
            if (idLevel > 0 && idLevel %i == 0)
            {
                StopAllCoroutines();
                foreach (Transform child in tubeContainer.transform)
                {
                    Destroy(child.gameObject);
                }
                GenTubeHidden(card.listLevel[idLevel]);
            } else
            {
                StopAllCoroutines();
                foreach (Transform child in tubeContainer.transform)
                {
                    Destroy(child.gameObject);
                }
                GenTubes(card.listLevel[idLevel]);
                timer.color = Color.yellow;
            }

        }
        else   //  Tao level moiw va luu lai vao SO 
        {
            StopAllCoroutines();
            pnShow.close();
            var oldData = SaveOldData(ActiveLevel);
            Level newLevel = CreatNewData(ActiveLevel, oldData);
            newLevel.totalTime = ActiveLevel.totalTime;
            foreach (Transform child in tubeContainer.transform)
            {
                Destroy(child.gameObject);
            }
            ActiveLevel = newLevel;
            card.listLevel.Add(newLevel);
            timer.color = Color.yellow;
            game.Tubes = listTube;
            game.count = 0;
            count = 0;
            if (idLevel > 0 && idLevel % i == 0)
            {              
                GenTubeHidden(newLevel);                
            }
            else
            {               
                GenTubes(newLevel);               
            }
            
        }
    }
    private void OnEnable()
    {

        StartCoroutine(UpdateTimer(ActiveLevel.totalTime - count));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Auto Gen");
    }
    void Start()
    {
        count = PlayerPrefs.GetInt("idLevel");
        if (count> 0 && count % i == 0)
        {
            GenTubeHidden(card.listLevel[count]);
        }
        else
        {
            GenTubes(card.listLevel[count]);
        }

        backGroundLevel.GetComponent<Image>().sprite = theme.listImageTheme[PlayerPrefs.GetInt("idTheme")].Imgtheme;
    }

    public int[,] SaveOldData(Level level) // Tao array data tu level hien tai
    {
        int column = level.listTubeData.Count;
        int row = listTube[0].listImage.Count;
        int[,] dataLevel = new int[row, column];
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                dataLevel[j, i] = level.listTubeData[i].Color[j];
            }

        }
        return dataLevel;
    }
    public Level CreatNewData(Level level, int[,] data) // tao data Level moi tu data cua level hien tai
    {
        Level newLevel = new Level();
        //data = SaveOldData(ActiveLevel);   //thừa
        List<TubeData> newListTubeData = new List<TubeData>();
        List<int> listData = new List<int>();
        int column = level.listTubeData.Count;
        int row = listTube[0].listImage.Count;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column - 2; j++)
            {
                listData.Add(data[i, j]);
            }
        }
        var newData = SetValue(listData, level);
        
        //for (int i = 0; i < column; i++)
        //{
        //    TubeData dataTube = new TubeData();
        //    dataTube.Color = new int[row];
        //    for (int j = 0; j < row; j++)
        //    {
        //        dataTube.Color[j] = newData[j, i];
        //    }
        //    newListTubeData.Add(dataTube);
        //}

        newLevel.listTubeData = BuildAllTubeFromData(column,row,newData,new List<TubeData>());
        return newLevel;
    }

    private List<TubeData> BuildAllTubeFromData(int column,int row,int[,] newData,List<TubeData> newListTubeData)
    {
        for (int i = 0; i < column; i++)
        {
            TubeData dataTube = new TubeData();
            dataTube.Color = new int[row];
            for (int j = 0; j < row; j++)
            {
                dataTube.Color[j] = newData[j, i];
            }
            newListTubeData.Add(dataTube);
        }
        return newListTubeData;
    }

    public int[,] SetValue(List<int> data, Level level)       //Phuong thuc tao data cua level moi
    {
        int column = level.listTubeData.Count;
        int row = listTube[0].listImage.Count;
        int[,] newData = new int[row, column];
        for (int i = 0; i < data.Count; i++)
        {
            int random = UnityEngine.Random.Range(0, column - 2);
            int count = 0, count1 = 0;
            for (int m = 0; m < row; m++)
            {
                if (newData[m, random] == 0)
                {
                    newData[m, random] = data[i];
                    count++;
                    break;
                }
            }
            if (count < 1)
            {
                for (int k = 0; k < column - 2; k++)
                {
                    for (int h = 0; h < row; h++)
                    {
                        if (newData[h, k] == 0)
                        {
                            newData[h, k] = data[i];
                            count1++;
                            break;
                        }

                    }
                    if (count1 > 0)
                    {
                        break;
                    }

                }
            }
        }
        return newData;
    }
}
