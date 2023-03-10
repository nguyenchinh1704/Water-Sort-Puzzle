using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public GameObject imaeCircle1, imaeCircle2, imaeCircle3;
    public UIElement pnPause;
    public InputField tubeNumber;
    public InputField colorNumber;
    public Text textLevel;


    public void RotateImage()
    {
        imaeCircle1.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
        imaeCircle2.transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
        imaeCircle3.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
    }

    public void ButtonPause()
    {
        pnPause.show();
    }
    public void ButtonClosePause()
    {
        pnPause.close();
    }

    private void Start()
    {
        RotateImage();
        var a = tubeNumber.text;
        var b = colorNumber.text;
        int tubeNum = int.Parse(a);
        int colorNum = int.Parse(b);
        textLevel.text = "LEVEL " + tubeNum + "." + colorNum;
    }
}
