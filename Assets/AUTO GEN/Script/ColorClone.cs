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
    public InputField colorNumber;
    int tubeNum, colorNum;
    public List<ColorClone> listColor;
    public Image color;
    public ColorCard card;

    private void Start()
    {
        GenColors();
    }
    public void GenColors()
    {
        var a = colorNumber.text;
        var b = tubeNumber.text;
        tubeNum = int.Parse(b);
        colorNum = int.Parse(a);
        /*int randomColor = UnityEngine.Random.Range(0, colorNum);*/
        if (colorNum > 1 && colorNum <= 5 && colorNum < tubeNum)
        {
            for (int i = 0; i < colorNum; i++)
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
