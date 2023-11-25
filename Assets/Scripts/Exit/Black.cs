using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black : MonoBehaviour
{
    public void Fade()
    {
        StartCoroutine(FadeRoutine());
    }

    public void Unfade()
    {
        StartCoroutine(FadeRoutine(false));
    }

    private IEnumerator FadeRoutine(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (objectColor.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (objectColor.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
