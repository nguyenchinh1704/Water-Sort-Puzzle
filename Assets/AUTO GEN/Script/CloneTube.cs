using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloneTube : MonoBehaviour
{
    public GameObject tube;
    public GameObject tubeContainer;
    public InputField tubeNumber;
    public GameObject message, pnStart;

    public GameObject btnReload, btnPlay;
    /*public GameObject groupTube;*/
    int tubeNum;
    public InputField difColorNum;

   
    private void Start()
    {
        GenTubes();
    }
    public void GenTubes()
    {
        var a = tubeNumber.text;
        var c = difColorNum.text;
        int difColor = int.Parse(c);
        tubeNum = int.Parse(a);
       /* GridLayoutGroup groupTubes = groupTube.GetComponent<GridLayoutGroup>();*/
       /* groupTubes.cellSize = new Vector2(150, colorInTube * 120 + 50); */     
        if (tubeNum <=10  && tubeNum > 2 && difColor < tubeNum && difColor >1)
        {
            for (int i = 1; i < tubeNum; i++)
            {
                GameObject tubesClone = Instantiate(tube, transform);
                tubesClone.transform.parent = tubeContainer.transform;
                tubesClone.name = "Tube" + (i + 1);
            }
            pnStart.SetActive(false);
        } else
        {
            message.SetActive(true);
            btnPlay.SetActive(false);
            btnReload.SetActive(true);
            StartCoroutine(AutoOff());
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator AutoOff()
    {
        yield return new WaitForSeconds(2f);
        message.SetActive(false);
    }
}
