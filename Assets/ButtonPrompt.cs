using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tmpugui;
    public Canvas canvas;
    public Vector3 sizeSmall;
    public Vector3 sizeLarge;
    public Manager manager;
    public void Show(string text = "Press all the buttons to interact (methodically please)")
    {
        canvas.transform.localScale = sizeSmall;
        tmpugui.text = text;
        canvas.enabled = true;
    }
    
    public void Hide()
    {
        canvas.enabled = false;
    }

    public void ShowThenFade(string text)
    {
        tmpugui.text = text;
        canvas.transform.localScale = sizeLarge;
        canvas.enabled = true;
        StartCoroutine(FadeAfterTime(5f));
    }

    IEnumerator FadeAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Hide();
    }
}
