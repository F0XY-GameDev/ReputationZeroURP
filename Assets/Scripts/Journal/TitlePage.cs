using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;
public class TitlePage : MonoBehaviour
{
    public GameObject leftVictim;
    public GameObject rightVictim;
    public GameObject centerVictim;
    public TextMeshProUGUI caseTitle;
    public TextMeshProUGUI caseBackground;
    public InputActionReference pageFlip;
    public UnityEvent nextSection;  
    void OnEnable()
    {
        pageFlip.action.started += NextSection;
    }
    private void OnDisable()
    {
        pageFlip.action.started -= NextSection;
    }
    void OnDestroy()
    {
        pageFlip.action.started -= NextSection;
    }
    void NextSection(InputAction.CallbackContext context)
    {
        nextSection.Invoke();
    }
    public void ToggleLeftVictim(bool value)
    {
        leftVictim.SetActive(value);
    }
    public void ToggleCenterVictim(bool value)
    {
        centerVictim.SetActive(value);
    }
    public void ToggleRightVictim(bool value)
    { 
        rightVictim.SetActive(value);
    }
    public void SetTitle(string newTitle)
    {
        caseTitle.text = newTitle;
    }
    public void SetDescription(string newDescription)
    {
        caseBackground.text = newDescription;
    }
}
