using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AllLevelData", menuName = "DataLevel")]
public class AllLevelData : ScriptableObject
{
    public List<Level> listLevel;

    
}
[System.Serializable]
public class Level
{
    public string name;
    public List<TubeModel> listTubeData;
    /// so tube, so color khac nhau
    
}
[System.Serializable]
public class TubeData
{
    public string[] datas;
}