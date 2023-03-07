using EazyEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityExtensions.Tween;

public class TubeManagement : MonoBehaviour
{
    public List<ColorImage> listImage;
    /*public int maxItem;*/
    public TubeData card;
    bool isChoose = false;
    bool isChange = false;
    public GameObject btnChoose, btnUnChoose, btnChange;
    /* public GameObject particeSys;*/



    #region buttonChoose
    [SerializeField] UIElement showChoose;
    public TweenPlayer localTweenShow;



    public void ResetDataTube()
    {
        int count = 0;
        for (int i = 0; i < card.Color.Length; i++)
        {
            listImage[i].SetColor(card.Color[i]);
            count++;

        }
        for (int i = count; i < listImage.Count; i++)
        {
            listImage[i].RemoveColor();
        }
    }
   /* public void StartEffect()
    {
        particeSys.SetActive(true);
    }
    public void EndEffect()
    {
        particeSys.SetActive(false);
    }*/
    public void Choose()
    {
        showChoose.show();
        btnChoose.SetActive(false);
        isChoose = true;
        /*StartCoroutine(RotateTube());*/
    }
    public void UnChoose()
    {
        close();
        btnChoose.SetActive(true);
        btnUnChoose.SetActive(false);
        isChoose = false;
    }
    public void close()
    {
        localTweenShow.Stop();
        localTweenShow.normalizedTime = 0;
    }


    #endregion

    #region ButtonChange

    /* public UIElement showChange;
     public TweenPlayer localTweenShowChange;
     public TweenPlayer localTweenShow1Change;*/
    /*public void AnimationEnd()
    {
        showChange.show();
        StartCoroutine(AutoOff());
    }*/
    public void Change()
    {
        btnChange.SetActive(false);
        isChange = true;
    }
    /*IEnumerator AutoOff()
    {
        yield return new WaitForSeconds(1f);
        closeChange();
        close1();
    }
    public void closeChange()
    {
        localTweenShowChange.Stop();
        localTweenShowChange.normalizedTime = 0;
    }
    public void close1()
    {
        localTweenShow1Change.Stop();
        localTweenShow1Change.normalizedTime = 0;
    }*/
    #endregion


    public ColorImage GettopAncol()
    {
        for (int i = 0; i <= listImage.Count; i++)
        {
            if (listImage[i].IsHasColor())
            {
                return listImage[i];
            }
        }
        return null;
    }
    public List<ColorImage> GetAllAncolSameColor()
    {
        List<ColorImage> result = new List<ColorImage>();
        int i = listImage.Count - 1;
        for (; i >= 0; i--)
        {
            if (listImage[i].IsHasColor())
            {
                result.Add(listImage[i]);
                i--;
                break;
            }
        }
        if (result.Count > 0)
        {
            for (; i >= 0; i--)
            {
                if (!listImage[i].IsSameColor(result[0]))
                {
                    break;
                }
                result.Add(listImage[i]);
            }
        }
        return result;

    }

    public List<ColorImage> GetAllAncolNoColor()
    {
        List<ColorImage> nocolor = new List<ColorImage>();
        for (int i = 0; i < listImage.Count; i++)
        {
            if (listImage[i].IsHasColor() == false)
            {
                nocolor.Add(listImage[i]);
            }
        }
        return nocolor;
    }

    public void ReceiveAllAncol(List<ColorImage> listImage)
    {

        for (int i = 0; i < listImage.Count; i++)
        {
            ReceiveOneAncol(listImage[i]);
        }

    }
    public void ReceiveOneAncol(ColorImage image)
    {
        for (int i = 0; i < listImage.Count; i++)
        {
            if (listImage[i].IsHasColor() == false)
            {
                listImage[i].SetColor(image.ReturnColor());
                image.RemoveColor();
                break;
            }
        }
    }
    public void SetColorTube(TubeData card)
    {
        this.card = card;
        
        for (int i = 0; i < card.Color.Length; i++)
        {
            listImage[i].SetColor(card.Color[i]);

            listImage[i].Check(card.Color[i]);
        }
        
    }



    internal bool IsHasChoose()
    {
        return isChoose;
    }
    internal bool IsHasChange()
    {
        return isChange;
    }
    public void ResetTube()
    {
        isChoose = false;
        isChange = false;
        btnUnChoose.SetActive(false);
        btnChange.SetActive(false);
        btnChoose.SetActive(true);
    }
    public void ReadytoChangeReceive()
    {
        isChoose = false;
        btnUnChoose.SetActive(false);
        btnChange.SetActive(true);
        btnChoose.SetActive(false);
    }
    public void ReadytoChangeGive()
    {
        isChoose = true;
        isChange = false;
        btnUnChoose.SetActive(true);
        btnChange.SetActive(false);
        btnChoose.SetActive(false);
    }

    public float timeRotate = 0.6f;
    public Transform leftRotationPoint;
    public Transform rightRotationPoint;
    public Transform chosenRotationPoint;
    public float directionMutiplier = 1.0f;
    public TubeManagement bottleControllerRef;
    Vector3 originalPosition, startPosition, endPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    IEnumerator MoveTube()
    {
        startPosition = transform.position;
        if(chosenRotationPoint = leftRotationPoint)
        {
            endPosition = bottleControllerRef.rightRotationPoint.position;
        }
        else
        {
            endPosition = bottleControllerRef.leftRotationPoint.position;
        }

        float t = 0;
        while(t<= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }
        /*StartCoroutine(RotateTube()); */
    }

    IEnumerator MoveTubeBack()
    {
        startPosition = transform.position;
        endPosition = originalPosition;

        float t = 0;
        while (t <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }      
    }

    IEnumerator RotateTube()
    {
        float t = 0;
        float lerpValue, angleValue;
        float lastAnglevalue = 0;
        while(t< timeRotate)
        {
            lerpValue = t / timeRotate;
            angleValue = Mathf.Lerp(0.0f, 60.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            /*transform.RotateAround(chosenRotationPoint.position, Vector3.forward, lastAnglevalue - angleValue);*/
            t += Time.deltaTime;
            lastAnglevalue = angleValue;
            yield return new WaitForEndOfFrame();
        }
        angleValue =  60.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        StartCoroutine(RotateTubeBack());
    }
    IEnumerator RotateTubeBack()
    {
        float t = 0;
        float lerpValue, angleValue;
        float lastAnglevalue = directionMutiplier;
        while (t < timeRotate)
        {
            lerpValue = t / timeRotate;
            angleValue = Mathf.Lerp(directionMutiplier*60.0f, 0.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            /*transform.RotateAround(chosenRotationPoint.position, Vector3.forward, lastAnglevalue - angleValue);*/

            lastAnglevalue = angleValue;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        angleValue = 0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);

        /*StartCoroutine(MoveTubeBack());*/
    }
    private void ChoseRotationPointAndDriection()
    {
        if(transform.position.x > bottleControllerRef.transform.position.x)
        {
            chosenRotationPoint = rightRotationPoint;
            directionMutiplier = -1.0f;
        } else
        {
            chosenRotationPoint = leftRotationPoint;
            directionMutiplier = 1.0f;
        }
    }
}