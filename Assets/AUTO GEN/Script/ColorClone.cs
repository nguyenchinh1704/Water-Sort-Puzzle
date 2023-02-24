using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorClone : MonoBehaviour
{
    public InputField tubeNumber;
    public GameObject message, pnStart;

    public GameObject containerColor, btnReload, btnPlay;
    public InputField sumColorInTube;
    int tubeNum, colorInTube;
    public List<ColorClone> listColor;
    public Image color;
    public InputField difColorNum;

    private void Start()
    {
        GenColors();
    }
    public void GenColors()
    {
        var a = sumColorInTube.text;
        var b = tubeNumber.text;
        tubeNum = int.Parse(b);
        colorInTube = int.Parse(a);
        var c = difColorNum.text;
        int difColor = int.Parse(c);
        /*int randomColor = UnityEngine.Random.Range(0, colorNum);*/
        if (colorInTube > 1 && colorInTube <= 5 && colorInTube < tubeNum && difColor <= colorInTube && difColor > 1)
        {
            for (int i = 0; i < colorInTube; i++)
            {
                Image colorClone = Instantiate(/*color[randomColor],*/color, transform);
                colorClone.transform.parent = containerColor.transform;
                /*colorClone.color = Color.yellow;*/
                colorClone.name = "Color" + (i + 1);
                
            }
            pnStart.SetActive(false);
        }
        else
        {
            message.SetActive(true);
            StartCoroutine(AutoOff());
        }

    }
    public void SetColor()
    {
       
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
