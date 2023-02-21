using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;
using UnityEngine.UI;
using UnityExtensions.Tween;

public class ButtonChange : MonoBehaviour
{
    public List<UIElement> show;
    public List<TweenPlayer> localTweenShow;
    public List<TweenPlayer> localTweenShow1;
    public List<GameObject> btnChange;
   


    public void Show()
    {
        for (int i = 0; i < show.Count; i++)
        {
            show[i].show();
            StartCoroutine(AutoOff());
        }
        
        
    }
    IEnumerator AutoOff()
    {
        yield return new WaitForSeconds(1f);
        close();
        close1();
        for (int i = 0; i < btnChange.Count; i++)
        {
            btnChange[i].SetActive(false);
        }
    }
    public void close()
    {
        for (int i = 0; i < localTweenShow.Count; i++)
        {
            localTweenShow[i].Stop();
            localTweenShow[i].normalizedTime = 0;
        }
        /*localTweenShow.Stop();
        localTweenShow.normalizedTime = 0; */      
    }
    public void close1()
    {
        for (int i = 0; i < localTweenShow1.Count; i++)
        {
            localTweenShow1[i].Stop();
            localTweenShow1[i].normalizedTime = 0;
        }
        /* localTweenShow1.Stop();
         localTweenShow1.normalizedTime = 0;*/
    }
    private void Update()
    {
        
    }
}
