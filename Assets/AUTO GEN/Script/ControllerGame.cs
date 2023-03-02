using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{
    public Level ActiveLevel;
    public GameObject tube, tubeContainer;
    public InputField tubeNumber;
    public InputField colorNumber;
    public GameObject pnStart, pnLevel, message, btnReload, btnStart;




    List<TubeManagement> listTube = new List<TubeManagement>();
    string[] colorR = new string[8] { "R", "G", "B", "Y", "X", "w", "P", "K" };


    /*public void GenTubes()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        listTube.Clear();
        listImageHasColor.Clear();

        
            for (int i = 0; i < tubeNum; i++)
            {
                GameObject tubeClone = Instantiate(tube, tubeContainer.transform);
                tubeClone.name = "Tube" + (i + 1);
                TubeManagement newtubeClone = tubeClone.GetComponent<TubeManagement>();
                listImageHasColor.AddRange(newtubeClone.listImage);
                listTube.Add(newtubeClone);
            for (int j = 0; j < ActiveLevel.listTubeData.Count; j++)
            {
                if(j == (colorNum - 2) && j < 3)
                {
                   listTube[i].SetColorRandom(ActiveLevel.listTubeData[j]);
                   
                    
                }
                else
                {
                    if (j == (colorNum - 2) && j > 2)
                    {
                        int sumImageHasColor = (4 * tubeNum - 8);                     
                            for (int k = 0; k < colorNum; k++)
                            {
                                for (int x = 0; x < 4; x++)
                                {
                                    var y = UnityEngine.Random.Range(0, 4);
                                    if(listImageHasColor[y].IsHasColor() == false)
                                    {
                                          listImageHasColor[y].SetColor(ActiveLevel.listTubeData[j].datas[k]);
                                    }
                                }
                            }
                    }
                }
            }

            }

       
    }*/
    public void GenTubes()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        ActiveLevel = GetDummyLevel();
        listTube.Clear();

        for (int i = 0; i < ActiveLevel.listTubeData.Count; i++)
        {
            GameObject tubeClone = Instantiate(tube, tubeContainer.transform);
            tubeClone.name = "Tube" + (i + 1);
            TubeManagement newtubeClone = tubeClone.GetComponent<TubeManagement>();
            newtubeClone.SetColorTube(ActiveLevel.listTubeData[i]);
        }
    }


    public void Colorize()
    {
        //var a = tubeNumber.text;
        //var b = colorNumber.text;
        //int tubeNum = int.Parse(a);
        //int colorNum = int.Parse(b);

        //TubeModel[] tubeArray = TubHelper.CreateTub(tubeNum, colorNum);

        //for (int i = 0; i < tubeNum; i++)
        //{
        //    for (int j = 0; j < 4; j++)
        //    {
        //        if (listTube[i].listImage[j].IsHasColor() == false)
        //        {
        //            //newImage[c].SetColor(ActiveLevel.listTubeData[i].datas[j]);
        //            listTube[i].listImage[j].SetColor(tubeArray[0].datas[j]);

        //        }

        //    }
        //}
    }

    /* public void Colorize()
     {
         var a = tubeNumber.text;
         var b = colorNumber.text;
         int tubeNum = int.Parse(a);
         int colorNum = int.Parse(b);
         for (int i = 0; i < newImage.Count; i++)
         {
             var c = UnityEngine.Random.Range(0, newImage.Count);
             for (int j = 0; j < colorNum; j++)
             {
                 if (newImage[c].IsHasColor() == false)
                 {
                     newImage[c].SetColor(ActiveLevel.listTubeData[i].datas[j]);
                 }

             }
         }
     }*/
    private Level GetDummyLevel()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        Level level = new Level();
        level.name = "Level 1";
        TubeModel data1 = new TubeModel(1, 5);
        data1.Color = new int[3] { 1, 2, 3 };
        TubeModel data2 = new TubeModel(1, 5);
        data2.Color = new int[3] { 3, 1, 2 };
        TubeModel data3 = new TubeModel(1, 5);
        data3.Color = new int[3] { 2, 3, 1 };
        level.listTubeData = new List<TubeModel>();
        level.listTubeData.Add(data1);
        level.listTubeData.Add(data2);
        level.listTubeData.Add(data3);


        return level;
    }
   /* private Level GenLevelByInput()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);

        Level level = new Level();
        TubeData data1 = new TubeData();
        level.name = "Level 1";

        List<string> dataColor = new List<string>();
        data1.datas = new string[colorNum];

        for (int i = 0; i < colorNum; i++)
        {
            data1.datas[i] = colorR[i];
        }

        level.listTubeData = new List<TubeData>();
        level.listTubeData.Add(data1);
        return level;
    }*/
    private Level GenLevelByInput()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        Level level = new Level();
        level.listTubeData = TubHelper.CreateTub(tubeNum, colorNum).ToList();


        return level;
    }


    public void StartButton()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        if (tubeNum > colorNum && colorNum >= 2 && tubeNum <= 10 && tubeNum >= 3)
        {
            pnStart.SetActive(false);
            pnLevel.SetActive(true);
            GenTubes();
            Colorize();
        }
        else
        {
            message.SetActive(true);
            btnReload.SetActive(true);
            btnStart.SetActive(false);
            StartCoroutine(AutoOff());
        }
    }
    IEnumerator AutoOff()
    {
        yield return new WaitForSeconds(2f);
        message.SetActive(false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("Auto Gen");
    }


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
