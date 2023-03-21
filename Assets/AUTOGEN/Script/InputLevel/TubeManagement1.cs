using EazyEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityExtensions.Tween;

public class TubeManagement1 : MonoBehaviour
{
    public List<ColorImage1> listImage;
    public TubeData card;
    bool isChoose = false;
    bool isChange = false;
    public GameObject btnChoose, btnUnChoose, btnChange;
    public GameObject particeSys;
   /* public UIElement showChoose;
    public TweenPlayer closeChoose;*/
  /*  public UIElement showChangeLeft, showChangeRight;
    public TweenPlayer endChangeLeft, endChangeRight;
    public ColorImage left, right;*/
    public GamePlay game;


    #region buttonChoose

    
   /* public void ShowLeft()
    {
        showChangeLeft.show();
        StartCoroutine(EndLeft());
    }

    IEnumerator EndLeft()
    {
        yield return new WaitForSeconds(1f);
        EndChangeLeft();
        left.gameObject.SetActive(false);
    }
    public void EndChangeLeft()
    {
        endChangeLeft.Stop();
        endChangeLeft.normalizedTime = 0;
    }
    public void ShowRight()
    {
        showChangeRight.show();      
        StartCoroutine(EndRight());
    }

    IEnumerator EndRight()
    {
        yield return new WaitForSeconds(1f);
        EndChangeRight();
        right.gameObject.SetActive(false);
    }
    public void EndChangeRight()
    {
        endChangeRight.Stop();
        endChangeRight.normalizedTime = 0;
    }*/
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
        /*showChoose.show();*/
        MoveTube();
        CheckColor();
    }
    public void UnChoose()
    {
        btnChoose.SetActive(true);
        btnUnChoose.SetActive(false);
        isChoose = false;
        /* EndChoose();*/
        MoveTubeBack();
    }
    /*public void EndChoose()
    {
        closeChoose.Stop();
        closeChoose.normalizedTime = 0;
    }*/


    #endregion

    #region ButtonChange


    public void Change()
    {
        btnChange.SetActive(false);
        isChange = true;
    }
    #endregion
    public int[] GetAllAncol()
    {
        int[] Color = new int[listImage.Count];
        for (int i = 0; i < listImage.Count; i++)
        {
            Color[i] = listImage[i].ReturnColor();
        }

        return Color;
    }

    public ColorImage1 GettopAncol()
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
    public List<ColorImage1> GetAllAncolSameColor()
    {
        List<ColorImage1> result = new List<ColorImage1>();
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

    public List<ColorImage1> GetAllAncolNoColor()
    {
        List<ColorImage1> nocolor = new List<ColorImage1>();
        for (int i = 0; i < listImage.Count; i++)
        {
            if (listImage[i].IsHasColor() == false)
            {
                nocolor.Add(listImage[i]);
            }
        }
        return nocolor;
    }
    public List<ColorImage1> ColoringCondition()
    {
        List<ColorImage1> listCheck = new List<ColorImage1>();
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

    public void StartChange(List<ColorImage1> listImage)
    {
        StartCoroutine(ReceiveAllAncol(listImage));
    }
    IEnumerator ReceiveAllAncol(List<ColorImage1> listImage)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < listImage.Count; i++)
        {
            ReceiveOneAncol(listImage[i]);
        }

    }
    public void ReceiveOneAncol(ColorImage1 image)
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
    public void SetColorByData(int[] color)
    {
        for (int i = 0; i < color.Length; i++)
        {          
                listImage[i].SetColor(color[i]);
                listImage[i].Check(color[i]);
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
    public void LockTube()
    {
        btnUnChoose.SetActive(false);
        btnChange.SetActive(false);
        btnChoose.SetActive(false);
    }

    public float timeRotate = 20f;
    public float directionMutiplier = 20f;


    public void MoveTube()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);
    }

    public void MoveTubeBack()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y - 6, transform.position.z);
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
            angleValue = Mathf.Lerp(0.0f, 45.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            t += Time.deltaTime;
            lastAnglevalue = angleValue;
            yield return new WaitForEndOfFrame();
        }
        angleValue = 45.0f;
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
            angleValue = Mathf.Lerp(directionMutiplier * 45.0f, 0.0f, lerpValue);

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
            angleValue = Mathf.Lerp(0.0f, -45.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);
            t += Time.deltaTime;
            lastAnglevalue = angleValue;
            yield return new WaitForEndOfFrame();
        }
        angleValue = -45.0f;
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
            angleValue = Mathf.Lerp(directionMutiplier * -45.0f, 0.0f, lerpValue);

            transform.eulerAngles = new Vector3(0, 0, angleValue);

            lastAnglevalue = angleValue;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        angleValue = 0f;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
    }
    public int[] CheckColor()
    {
        int[] data = new int[listImage.Count];
        for (int i = 0; i < listImage.Count; i++)
        {
            if(listImage[i].IsHasActive() == true)
            {
                if (listImage[i].img.color == Color.green)
                {
                    data[i] = 1;
                }
                if (listImage[i].img.color == Color.blue)
                {
                    data[i] = 2;
                }
                if (listImage[i].img.color == Color.yellow)
                {
                    data[i] = 3;
                }
                if (listImage[i].img.color == Color.gray)
                {
                    data[i] = 4;
                }
                if (listImage[i].img.color == Color.cyan)
                {
                    data[i] = 5;
                }
                if (listImage[i].img.color == Color.magenta)
                {
                    data[i] = 6;
                }
                if (listImage[i].img.color == Color.black)
                {
                    data[i] = 7;
                }
                if (listImage[i].img.color == Color.red)
                {
                    data[i] = 8;
                }
            }
            
        }

        return data;
    }

}