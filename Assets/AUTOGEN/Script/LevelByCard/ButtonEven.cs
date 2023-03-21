using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonEven : MonoBehaviour
{
    public GameObject imaeCircle1, imaeCircle2, imaeCircle3;
    public GameObject imaeCircle1ST, imaeCircle2ST, imaeCircle3ST;
    public UIElement pnStart, pnVictory, pnDefeated, pnPause, pnLevel, pnSetting, pnMessage;
    public Text textLevel;
    public LoadLevel level;
    public AllLevelData card;
    public ManagementGame game;
    


    public void RotateImage()
    {
        imaeCircle1.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
        imaeCircle2.transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
        imaeCircle3.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
        imaeCircle1ST.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
        imaeCircle2ST.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
        imaeCircle3ST.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
    }


    public void ButtonStart()
    {
        pnStart.close();
        pnLevel.show();
        pnSetting.close();

    }
    public void ButtonPause()
    {
        pnPause.show();
        textLevel.text = level.textLevel.text;
        pnLevel.close();
    }

    public void ButtonClosePause()
    {
        pnPause.close();
        pnLevel.show();
       
    }
    public void BtnNextLevel()
    {
        pnVictory.close();
    }

    public void BtnRestartDf()
    {
        pnDefeated.close();
        
    }
    public void BtnExitV()
    {
        pnVictory.close();
        pnStart.show();
        pnLevel.close();
        PlayerPrefs.SetInt("idLevel", level.idLevel);
        SceneManager.LoadScene("Auto Gen");

    }
    public void BtnQuitDf()
    {
        pnStart.show();
        pnLevel.close();
        pnDefeated.close();
        level.ButtonRestart();
    }   
    public void BtnSetting()
    {
        pnSetting.show();
    }
    public void CloseSetting()
    {
        pnSetting.close();
    }
    public void BtnOk()
    {
        PlayerPrefs.DeleteKey("idLevel");
        SceneManager.LoadScene("Auto Gen");
    }
    public void BtnHome()
    {
        /*pnPause.close();
        pnLevel.close();
        pnStart.show();
        PlayerPrefs.SetInt("idLevel", level.idLevel);*/
        SceneManager.LoadScene("Auto Gen");
    }
    IEnumerator Autooff()
    {
        yield return new WaitForSeconds(1f);
        pnMessage.close();
    }

    private void Start()
    {

    }
    private void Update()
    {
        RotateImage();
    }
}
