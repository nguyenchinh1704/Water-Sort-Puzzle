using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReload : MonoBehaviour
{
    public LoadLevel loadLevel;
    public TubeManagement tube;

    public int[,] CreatArrayData(Level level)
    {
        int column = loadLevel.ActiveLevel.listTubeData.Count;
        int row = tube.listImage.Count;
        int[,] dataLevel = new int[row, column];
        for (int i = 0; i < column; i++)
        {
            var dataTube = ReturnDataTube(level);
            for (int j = 0; j < row; j++)
            {
                dataLevel[i, j] = dataTube[j];
            }
        }

        return dataLevel;
    }
    public int[] ReturnDataTube(Level level)
    {
        int row = tube.listImage.Count;
        int[] dataTube = new int[row];
        for (int i = 0; i < level.listTubeData.Count; i++)
        {
            for (int j = 0; j < row; j++)
            {
                dataTube[i] = level.listTubeData[i].Color[j];
            }
        }
        return dataTube;
    }

    private void Start()
    {
        var dataLevel = CreatArrayData(loadLevel.ActiveLevel);
    }
}
