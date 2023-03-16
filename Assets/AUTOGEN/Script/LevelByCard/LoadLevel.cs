using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EazyEngine.UI;

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
    public AllLevelData card1;
    public ManagementGame game;
    public int count = 0;

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
            if (time == 0)
            {
                pnShow.show();
            }
            yield return new WaitForSeconds(1f);

        }

    }

    public void ButtonRestart()
    {
        pnShow.close();
        for (int i = 0; i < listTube.Count; i++)
        {
            listTube[i].ResetDataTube();
            listTube[i].EndPar();
        }
        StopAllCoroutines();
        StartCoroutine(UpdateTimer(card.listLevel[idLevel].totalTime));
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

        for (int i = 0; i < level.listTubeData.Count; i++)
        {
            GameObject tubeClone = Instantiate(tube, tubeContainer.transform);
            tubeClone.name = "Tube" + i;
            TubeManagement newtubeClone = tubeClone.GetComponent<TubeManagement>();
            listTube.Add(newtubeClone);
            listTube[i].SetColorTube(level.listTubeData[i]);

        }
        textLevel.text = "LEVEL " + (level.listTubeData.Count - 3);
        StartCoroutine(UpdateTimer(level.totalTime));
        idLevel++;
        ActiveLevel = level;
    }
    public void ButtonNextLevel()
    {
        tubeContainer.transform.DetachChildren();
        GenTubes(card.listLevel[idLevel]);
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
        /*var a = card1.listLevel.Count;*/
        GenTubes(card.listLevel[count]);
    }
}
