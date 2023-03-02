using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TubHelper
{
    public static TubeModel[] CreateTub(int tubeNum, int colorNum)
	{
		TubeModel[] result = new TubeModel[tubeNum];
		for (int i = 0; i < result.Length; i++)
		{
			TubeModel tubeTemp = new TubeModel(i, colorNum);
			tubeTemp.RandomColor(colorNum);
			result[i] = tubeTemp;
		}

		return result;
	}
}
