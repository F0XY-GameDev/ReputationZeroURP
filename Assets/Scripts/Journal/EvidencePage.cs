using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class EvidencePage : MonoBehaviour
{

    public int CurrentItem = 0;
    public Journal journal;    
    public TMPro.TextMeshProUGUI EvidenceFoundLocation;
    public TMPro.TextMeshProUGUI ItemName;
    public List<TMPro.TextMeshProUGUI> EvidenceDescriptions;
    public Image ItemImage;
    public Button Next;
    public InputActionReference pageFlip;
    public UnityEvent nextSection;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {        
        ItemName.text = journal.Evidence[CurrentItem].ItemName;
        ItemImage.sprite = journal.Evidence[CurrentItem].ItemPicture;
        EvidenceFoundLocation.text = journal.Evidence[CurrentItem].FoundLocation;
        for (int i = 0; i < EvidenceDescriptions.Count; i++) 
        {
            if (journal.Evidence[i] != null)
            {
                EvidenceDescriptions[i].text = journal.Evidence[CurrentItem].Descriptions[i];
            } else
            {
                break;
            }            
        }
    }  

    private void OnEnable()
    {
        pageFlip.action.started += NextPage;
        if(CurrentItem < (journal.Evidence.Count - 1))
        {
            Next.interactable = true;
        }
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
        if (CurrentItem == (journal.Evidence.Count - 1))
        {
            nextSection.Invoke();
            return;
        }
        CurrentItem++;
        if (CurrentItem == (journal.Evidence.Count - 1))
        {
            Next.interactable = false;
        }

    }
}
