using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EazyEngine.UI;
using UnityExtensions.Tween;

public class LoadLevel : MonoBehaviour
{
    public Level ActiveLevel;
    public GameObject tube, tubeContainer;
    public Text textLevel;
    public List<TubeManagement> listTube = new List<TubeManagement>();
    public AllLevelData card;
    public int idLevel = 0;
    [SerializeField] private Text timer;
    [SerializeField] private Image imageLight;
    [SerializeField] UIElement pnShow;
    public UIElement pnVictory;
    public GameObject Clock;
    public AllLevelData card1;
    public ManagementGame game;
    public int count = 0;
    public GameObject pnWarning;
    IEnumerator UpdateTimer(int time)
    {
        while (time >= 0)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float sec = Mathf.FloorToInt(time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, sec);
            imageLight.fillAmount = Mathf.InverseLerp(0, time, time);
            time--;
            count++;           
            yield return new WaitForSeconds(1f);   
            if(time <11)
            {
                ControlWarning(time);
            }
            if (time == 10)
            {
                timer.color = Color.red;
            }
            if (time == 0)
            {
                StartCoroutine(AutoBackColor());
            }
        }

    }

    public void ControlWarning(int time)
    {
        int i = time;
        for (; i >= 0; i--)
        {
            StartCoroutine(OnWarning());
        }
    }
    IEnumerator OnWarning()
    {
        yield return new WaitForSeconds(0f);
        pnWarning.SetActive(true);
        StartCoroutine(OffWarning());
    }
    IEnumerator OffWarning()
    {
        yield return new WaitForSeconds(0.3f);
        pnWarning.SetActive(false);
    }
    IEnumerator AutoBackColor()
    {
        yield return new WaitForSeconds(1f);
        timer.color = Color.yellow;
        StartCoroutine(AutoShow());
    }

    IEnumerator AutoShow()
    {
        yield return new WaitForSeconds(1.5f);
        pnShow.show();
    }

    public void ButtonRestart()
    {
        pnShow.close();
        for (int i = 0; i < listTube.Count; i++)
        {
            listTube[i].ResetDataTube();
            listTube[i].transform.position = game.dataPosition[i];
            listTube[i].EndPar();
            listTube[i].ResetTube();
        }
        game.Tubes  = listTube;
        game.count = 0;
        count = 0;
    }
    public void ButtonRestartVictory()
    {
        pnVictory.close();
        for (int i = 0; i < listTube.Count; i++)
        {
            listTube[i].ResetDataTube();
            listTube[i].EndPar();
        }
        StartCoroutine(UpdateTimer(card.listLevel[idLevel - 1].totalTime));
    }

    public void GenTubes(Level level)
    {
        listTube.Clear();
        idLevel = PlayerPrefs.GetInt("idLevel");
        for (int i = 0; i < level.listTubeData.Count; i++)
        {
            GameObject tubeClone = Instantiate(tube, tubeContainer.transform);
            tubeClone.name = "Tube" + i;
            TubeManagement newtubeClone = tubeClone.GetComponent<TubeManagement>();
            listTube.Add(newtubeClone);
            listTube[i].SetColorTube(level.listTubeData[i]);

        }
        textLevel.text = "LEVEL " + (PlayerPrefs.GetInt("idLevel") + 1);
        StartCoroutine(UpdateTimer(level.totalTime));
        ActiveLevel = level;
        idLevel++;

    }
    public void ButtonNextLevel()
    {
        tubeContainer.transform.DetachChildren();
        GenTubes(card.listLevel[idLevel]);
        timer.color = Color.yellow;
    }
    private void OnEnable()
    {

        StartCoroutine(UpdateTimer(ActiveLevel.totalTime - count));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Auto Gen");
    }
    void Start()
    {
        count = PlayerPrefs.GetInt("idLevel");
        GenTubes(card.listLevel[count]);
    }
}
