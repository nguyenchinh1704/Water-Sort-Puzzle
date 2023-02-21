using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;
using UnityExtensions.Tween;

public class EvenButton : MonoBehaviour
{
    [SerializeField] UIElement show;
    public TweenPlayer localTweenShow;
    /*[SerializeField] GameObject btnChange;*/
    public List<GameObject> btnChange;

    // Start is called before the first frame update
    public void Show()
    {
        
            show.show();
            
            for (int i = 0; i <btnChange.Count ; i++)
            {
                btnChange[i].SetActive(true);
            }

    }
    public void UnChoose()
    {
        close();
        
        for (int i = 0; i < btnChange.Count; i++)
        {
            btnChange[i].SetActive(false);
        }
    }

    public void close()
    {
                localTweenShow.Stop();
                localTweenShow.normalizedTime = 0;
                /*localTweenShow._onBackArrived.Invoke();*/
    }

}
