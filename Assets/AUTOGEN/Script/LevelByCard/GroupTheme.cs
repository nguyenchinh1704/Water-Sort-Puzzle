using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EazyEngine.UI;

public class GroupTheme : MonoBehaviour
{
    public List<ChooseTheme> listImageTheme;
    public Image backGroundLevel;
    public GameObject textMessage;
    public UIElement pnObject;
    public Text textTheme;

    public void ButtonSaveTheme()
    {
        for (int i = 0; i < listImageTheme.Count; i++)
        {
            if (listImageTheme[i].imageTheme.color == Color.yellow)
            {
                backGroundLevel.GetComponent<Image>().sprite = listImageTheme[i].Imgtheme;
                textMessage.SetActive(true);
                PlayerPrefs.SetInt("idTheme", i);
                break;
            }
        }
        StartCoroutine(AutoOff());
    }

    IEnumerator AutoOff()
    {
        yield return new WaitForSeconds(0.5f);
        textMessage.SetActive(false);
        pnObject.close();
    }

    public void CheckBackGround()
    {
        for (int i = 0; i < listImageTheme.Count; i++)
        {
            if (backGroundLevel.GetComponent<Image>().sprite == listImageTheme[i].Imgtheme)
            {
                listImageTheme[i].imageTheme.color = Color.yellow;
            }
            else
            {
                listImageTheme[i].imageTheme.color = Color.white;
            }
        }
    }

    public void SetColorText()
    {
        StartCoroutine(TextColor());
        StartCoroutine(TextColor1());
        StartCoroutine(TextColor2());
    }
    IEnumerator TextColor()
    {
        yield return new WaitForSeconds(0.5f);
        textTheme.color = Color.red;
    }
    IEnumerator TextColor1()
    {
        yield return new WaitForSeconds(1f);
        textTheme.color = Color.yellow;
    }
    IEnumerator TextColor2()
    {
        yield return new WaitForSeconds(1.5f);
        textTheme.color = Color.green;
    }
    private IEnumerator OnColorText()
    {
        float time = 1 * Time.deltaTime;
        while(time > 0)
        {
            SetColorText();
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(OnColorText());
    }
}
