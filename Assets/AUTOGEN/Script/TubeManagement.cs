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
    public TubeData card;
    bool isChoose = false;
    bool isChange = false;
    public GameObject btnChoose, btnUnChoose, btnChange;
    public GameObject particeSys;



    #region buttonChoose

    public void Particice()
    {
        particeSys.SetActive(true);
    }
    public void EndPar()
    {
        particeSys.SetActive(false);
    }
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
    public void Choose()
    {      
        btnChoose.SetActive(false);
        isChoose = true;
        MoveTube();
    }
    public void UnChoose()
    {
        btnChoose.SetActive(true);
        btnUnChoose.SetActive(false);
        isChoose = false;
        MoveTubeBack();
    }


    #endregion

    #region ButtonChange

    
    public void Change()
    {
        btnChange.SetActive(false);
        isChange = true;
    }
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
    public List<ColorImage> ColoringCondition()
    {
        List<ColorImage> listCheck = new List<ColorImage>();
        int i = listImage.Count - 1;
        for (; i >= 0; i--)
        {
            if (listImage[i].IsHasColor() == true)
            {
                listCheck.Add(listImage[i]);
                break;
            }
        }
        return listCheck;
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

    public float timeRotate = 0.3f;
    public float directionMutiplier = 0.5f;


    public void MoveTube()
    {    
        transform.position = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);
    }

    public void MoveTubeBack()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y -6 , transform.position.z);
    }

    IEnumerator TubeBack(Vector3 a)
    {
        yield return new WaitForSeconds(0.5f);

    }


    public void EffectRotateLeft()
    {
        StartCoroutine(RotateTubeLeft());
    }
    public void EffectRotateRight()
    {
        StartCoroutine(RotateTubeRight());
    }
    IEnumerator RotateTubeLeft()
    {
        float t = 0;
        float lerpValue, angleValue;
        float lastAnglevalue = 0;
        while (t < timeRotate)
        {
            lerpValue = t / timeRotate;
            angleValue = Mathf.Lerp(0.0f, 60.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            t += Time.deltaTime;
            lastAnglevalue = angleValue;
            yield return new WaitForEndOfFrame();
        }
        angleValue = 60.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        StartCoroutine(RotateTubeLeftBack());
    }
    IEnumerator RotateTubeLeftBack()
    {
        float t = 0;
        float lerpValue, angleValue;
        float lastAnglevalue = directionMutiplier;
        while (t < timeRotate)
        {
            lerpValue = t / timeRotate;
            angleValue = Mathf.Lerp(directionMutiplier * 60.0f, 0.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);

            lastAnglevalue = angleValue;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        angleValue = 0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);

    }
    IEnumerator RotateTubeRight()
    {
        float t = 0;
        float lerpValue, angleValue;
        float lastAnglevalue = 0;
        while (t < timeRotate)
        {
            lerpValue = t / timeRotate;
            angleValue = Mathf.Lerp(0.0f, -60.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            t += Time.deltaTime;
            lastAnglevalue = angleValue;
            yield return new WaitForEndOfFrame();
        }
        angleValue = -60.0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        StartCoroutine(RotateTubeRightBack());
    }
    IEnumerator RotateTubeRightBack()
    {
        float t = 0;
        float lerpValue, angleValue;
        float lastAnglevalue = directionMutiplier;
        while (t < timeRotate)
        {
            lerpValue = t / timeRotate;
            angleValue = Mathf.Lerp(directionMutiplier * -60.0f, 0.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);           

            lastAnglevalue = angleValue;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        angleValue = 0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
    }
}