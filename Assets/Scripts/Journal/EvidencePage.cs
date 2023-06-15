using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EvidencePage : MonoBehaviour
{

    public int CurrentItem = 0;
    public int CurrentItemID;
    public Journal journal;    
    public Manager manager;
    public TMPro.TextMeshProUGUI EvidenceFoundLocation;
    public TMPro.TextMeshProUGUI ItemName;
    public List<TMPro.TextMeshProUGUI> EvidenceDescriptions;
    public Image ItemImage;
    public Button Next;
    public InputActionReference pageFlip;
    public UnityEvent nextSection;
    public UnityEvent nextPage;
    public List<string> linkedDescriptions = new List<string>();
    EvidenceData evidenceData;
    
    // Update is called once per frame
    void Update()
    {        
        ItemName.text = journal.Evidence[CurrentItem].ItemName;
        ItemImage.sprite = journal.Evidence[CurrentItem].ItemPicture;
        EvidenceFoundLocation.text = journal.Evidence[CurrentItem].FoundLocation;
        evidenceData = journal.Evidence[CurrentItem];
        linkedDescriptions = manager.GetEvidenceDescriptionsByID(evidenceData.EvidenceID);
        for (int i = 0; i < EvidenceDescriptions.Count; i++) 
        {            
            EvidenceDescriptions[i].text = linkedDescriptions[i];
            if (i >= linkedDescriptions.Count - 1)
            {
                break;
            }
            //EvidenceDescriptions[i].text = journal.Evidence[CurrentItem].Descriptions[i];                     
        }
    }  

    private void OnEnable()
    {        
        pageFlip.action.started += NextPage;        
    }

    private void OnDisable()
    {
        pageFlip.action.started -= NextPage;
        CurrentItem = 0;
    }

    private void OnDestroy()
    {
        pageFlip.action.started -= NextPage;
    }
    private void NextPage(InputAction.CallbackContext context)
    {
        NextItem();
    }
    public void NextItem()
    {
        nextPage.Invoke();
        if (CurrentItem == (journal.Evidence.Count - 1))
        {
            nextSection.Invoke();
            return;
        }
        CurrentItem++;
    }
}
