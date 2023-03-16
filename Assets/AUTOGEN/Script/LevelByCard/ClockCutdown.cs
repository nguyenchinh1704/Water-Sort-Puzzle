using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using EazyEngine.UI;


public class ClockCutdown : MonoBehaviour
{
    [SerializeField] private Text timer;
    [SerializeField] private Image imageLight;
    [SerializeField] UIElement pnShow;
    public int totaltime;
    private int time;
    // Start is called before the first frame update

   /* private void Start()
    {
        var a = level.idLevel - 1;
        totaltime = card.listLevel[a].totalTime;
        Being(totaltime);
    }*/

    public void Being(int Second)
    {
        time = Second;
        StartCoroutine(UpdateTimer());
    }
    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {
        while (time >=  0)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float sec = Mathf.FloorToInt(time % 60);
            timer.text = string.Format("{0:00}:{1:00}", minutes, sec);
            imageLight.fillAmount = Mathf.InverseLerp(0, totaltime, time);
            time--;
            yield return new WaitForSeconds(1f);
        }
    }
   /* private void Update()
    {
        if (time == 0)
        {
            timer.text = time.ToString("0");
            imageLight.fillAmount = 0;
            pnShow.show();
            
        }
        else
        {
            pnShow.close();
        }
    }*/
    public void BtnReset()
    {
        time = totaltime;
        Being(totaltime);
    }

    private void OnEnable()
    {

        StartCoroutine(UpdateTimer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Close()
    {
        time = totaltime;
    }
}