using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonEven : MonoBehaviour
{
    public GameObject imaeCircle1, imaeCircle2, imaeCircle3;
    public UIElement pnStart, pnVictory, pnDefeated, pnPause, pnLevel, pnSetting,pnSettingPause, pnMessage;
    public Text textLevel;
    public LoadLevel level;
    public AllLevelData card;
    public ManagementGame game;
    public GameObject sound;
    public GameObject soundMute;
    bool isMute = false;
    public GameObject soundPause;
    public GameObject soundMutePause;
    public GameObject soundEffect;
    bool isMutePause = false;

    public void RotateImage()
    {
        imaeCircle1.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
        imaeCircle2.transform.Rotate(new Vector3(0, 0, -30) * Time.deltaTime);
        imaeCircle3.transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
    }

    public void ButtonMute()
    {
        if(isMute == false)
        {
            isMute = true;
            sound.SetActive(false);
            soundPause.SetActive(false);
            soundMute.SetActive(true);
            soundMutePause.SetActive(true);
            soundEffect.SetActive(false);
        }
        else
        {
            isMute = false;
            sound.SetActive(true);
            soundMute.SetActive(false);
            soundPause.SetActive(true);
            soundMutePause.SetActive(false);
            soundEffect.SetActive(true);
        }
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
    /*    textLevel.text = level.textLevel.text;*/
        pnLevel.close();
    }
    public void BttuonSettingPause()
    {
        pnSettingPause.show();
    }
    public void CloseSettingPause()
    {
        pnSettingPause.close();
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
