using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class ResultPage : MonoBehaviour
{
    public int CurrentPerson = 0;
    public TMPro.TextMeshProUGUI EvidenceDiscovered;
    public TMPro.TextMeshProUGUI TestimonyDiscovered;
    public TMPro.TextMeshProUGUI InnocentsAccused;
    public TMPro.TextMeshProUGUI MurderersFound;
    public Manager manager;
    public Image EvidenceImage;
    public Image PersonImage;
    public InputActionReference pageFlip;
    public UnityEvent nextSection;
    
    private void OnEnable()
    {
        pageFlip.action.started += NextPage;        
    }

    private void OnDisable()
    {
        pageFlip.action.started -= NextPage;
        CurrentPerson = 0;

    }

    private void OnDestroy()
    {
        pageFlip.action.started -= NextPage;
    }

    private void NextPage(InputAction.CallbackContext context)
    {
        nextSection.Invoke();
        return;
    }
}
