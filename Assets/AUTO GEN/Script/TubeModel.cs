using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TubeModel
{
	public int id;
	public int[] Color;
	public int maxColor;
	public int tubeNum, colorNum;
	

	List<int> colorData = new List<int>();
	List<TubeModel> newData = new List<TubeModel>();

	public TubeModel(int id, int maxColor)
	{
		this.id = id;
		this.maxColor = maxColor;
		Color = new int[maxColor];
	}
	public void SetColor(int colorNum)
	{
			Color = new int[maxColor];

			for (int j = 0; j < Color.Length; j++)
			{
				Color[j] = j;
				
			}
		
	}
    private void GetDummyLevel()
    {
        
   
        int[] Color = new int[colorNum];
        List<int> listData = new List<int>();
        List<TubeModel> newData = new List<TubeModel>();

        for (int j = 0; j < Color.Length; j++)
        {
            Color[j] = j + 1;

        }


        int[,] arrayTube = new int[maxColor, tubeNum - 2];

        for (int idColor = 1; idColor <= Color.Length; idColor++)
        {
            for (int m = 0; m < (maxColor); m++)
            {
                var pickTube = UnityEngine.Random.Range(0, tubeNum - 2);

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
                    for (int i = 0; i < tubeNum - 2; i++)
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

        for (int i = 0; i < tubeNum - 2; i++)
        {
            TubeModel data = new TubeModel(0, colorNum);
            data.Color = new int[maxColor];
            for (int j = 0; j < maxColor; j++)
            {
                data.Color[j] = arrayTube[j, i];
            }
            newData.Add(data);
        }

    }




}
