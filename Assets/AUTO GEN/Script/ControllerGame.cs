using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{
    public Level ActiveLevel;
    public GameObject tube, tubeContainer;
   



    List<TubeManagement> listTube = new List<TubeManagement>();
    public void GenTubes()
    {
        ActiveLevel = GetDummyLevel();
        listTube.Clear();
        if (ActiveLevel.listTubeData.Count<=10 )
        {
            for (int i = 0; i < ActiveLevel.listTubeData.Count; i++)
            {
                GameObject tubesClone = Instantiate(tube, tubeContainer.transform);
                tubesClone.name = "Tube" + (i + 1);
                TubeManagement newtubesClone = tubesClone.GetComponent<TubeManagement>();
                listTube.Add(newtubesClone);
                newtubesClone.SetColorTube(ActiveLevel.listTubeData[i]);

            }
        }
       
    }

    private Level GetDummyLevel()
    {
        Level level = new Level();
        
        return level;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Auto Gen");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        GenTubes();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
