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
    int tubeNum, colorNum;
    public InputField colorNumber;
    public GameObject pnStart, pnLevel, message, btnReload, btnStart;
   
    public List<TubeManagement> listTube = new List<TubeManagement>();

    string[] colorR = new string[8] { "R", "G", "B", "Y", "X", "w", "P", "K" };   


    /*public void GenTubes()
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
    }*/
    public int ReturnInput(string a)
    {
        a = tubeNumber.text;
        tubeNum = int.Parse(a);

        return tubeNum;
    }



    public void GenTubes()
    {
        ActiveLevel = GetDummyLevel();
        listTube.Clear();

        for (int i = 0; i < ActiveLevel.listTubeData.Count; i++)
        {
            GameObject tubeClone = Instantiate(tube, tubeContainer.transform);
            tubeClone.name = "Tube" + i;
            TubeManagement newtubeClone = tubeClone.GetComponent<TubeManagement>();
            listTube.Add(newtubeClone);
            listTube[i].SetColorTube(ActiveLevel.listTubeData[i]);

        }
    }


    /*private Level GetDummyLevel()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        Level level = new Level();
        level.name = "Level 1";
        TubeModel data1 = new TubeModel(0, 3);
        data1.Color = new int[3] { 1, 2, 3};
        TubeModel data2 = new TubeModel(1, 3);
        data2.Color = new int[3] { 3, 1, 2 };
        TubeModel data3 = new TubeModel(2, 3);
        data3.Color = new int[3] { 2, 3, 1 };
        level.listTubeData = new List<TubeModel>();
        level.listTubeData.Add(data1);
        level.listTubeData.Add(data2);
        level.listTubeData.Add(data3);


        return level;
    }*/

    private Level GetDummyLevel()
    {
        var a = tubeNumber.text;
        int maxColor = 4;
        var b = colorNumber.text;
        tubeNum = int.Parse(a);
        colorNum = int.Parse(b);
        Level level = new Level();
        level.name = "Level 1";
        int[] Color = new int[colorNum];
        List<int> listData = new List<int>();
        List<TubeData> newData = new List<TubeData>();

        for (int j = 0; j < Color.Length; j++)
        {
            Color[j] = j + 1;

        }


        int[,] arrayTube = new int[maxColor, tubeNum];

        for (int idColor = 1; idColor <= Color.Length; idColor++)
        {
            for (int m = 0; m < (maxColor); m++)
            {
                var pickTube = UnityEngine.Random.Range(0, tubeNum);

                int count = 0, count1 = 0;
                for (int j = 0; j < maxColor; j++) ///check vi tri trong trong tube tu duoi len
                {
                    if (arrayTube[j, pickTube] == ColorImage.NO_COLOR)
                    {
                        arrayTube[j, pickTube] = idColor;
                        count++;
                        break;
                    }
                }
                if (count < 1)
                {
                    for (int i = 0; i < tubeNum; i++)
                    {
                        for (int j = 0; j < maxColor; j++)
                        {
                            if (arrayTube[j, i] == ColorImage.NO_COLOR)
                            {
                                arrayTube[j, i] = idColor;
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
        }

        for (int i = 0; i < tubeNum; i++)
        {
            TubeData data = new TubeData();
            data.Color = new int[maxColor];
            for (int j = 0; j < maxColor; j++)
            {
                data.Color[j] = arrayTube[j, i];
            }
            newData.Add(data);
        }

        level.listTubeData = newData;

        return level;
    }
    /* private Level GetDummyLevel()
     {
         var a = tubeNumber.text;
         int maxColor = 4;
         var b = colorNumber.text;
         int tubeNum = int.Parse(a);
         int colorNum = int.Parse(b);
         Level level = new Level();
         level.name = "Level 1";
         int[] Color = new int[colorNum];
         List<int> listData = new List<int>();
         List<TubeModel> newData = new List<TubeModel>();

         for (int j = 0; j < Color.Length; j++)
         {
             Color[j] = j + 1;

         }


         int[,] arrayTube = new int[tubeNum -2, maxColor];

         for (int idColor = 1; idColor <= Color.Length; idColor++)
         {
             for (int m = 0; m < ((tubeNum - 2) *maxColor)/colorNum; m++)
             {
                 var pickTube = UnityEngine.Random.Range(0, tubeNum - 2);

                 int count = 0, count1 = 0;
                 for (int j = 0; j < maxColor; j++) ///check vi tri trong trong tube tu duoi len
                 {
                     if (arrayTube[ pickTube,j] == ColorImage.NO_COLOR)
                     {
                         arrayTube[pickTube, j] = idColor;
                         count++;
                         break;
                     }
                 }
                 if (count < 1)
                 {
                     for (int i = 0; i < tubeNum - 2; i++)
                     {
                         for (int j = 0; j < maxColor; j++)
                         {
                             if (arrayTube[i,j] == ColorImage.NO_COLOR)
                             {
                                 arrayTube[i, j] = idColor;
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
         }



         for (int i = 0; i < tubeNum - 2; i++)
         {
             TubeModel data = new TubeModel(0, colorNum);
             data.Color = new int[maxColor];
             for (int j = 0; j < maxColor; j++)
             {
                 data.Color[j] = arrayTube[i, j];
             }
             newData.Add(data);
         }

         level.listTubeData = newData;

         return level;
     }*/


    public void StartButton()
    {
        var a = tubeNumber.text;
        var b = colorNumber.text;
        tubeNum = int.Parse(a);
        colorNum = int.Parse(b);
        if ((tubeNum - colorNum) >= 2 && colorNum >= 2 && tubeNum <= 10 && tubeNum >= 3)
        {
            pnStart.SetActive(false);
            pnLevel.SetActive(true);
            GenTubes();

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

   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
