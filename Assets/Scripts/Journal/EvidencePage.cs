using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidencePage : MonoBehaviour
{

    public int CurrentItem = 0;
    public Journal journal;    
    public TMPro.TextMeshProUGUI EvidenceFoundLocation;
    public TMPro.TextMeshProUGUI ItemName;
    public List<TMPro.TextMeshProUGUI> EvidenceDescriptions;
    public Image ItemImage;
    public Button Next;
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

    public void SetPageItem(EvidenceData data)
    {

    }

    private void OnEnable()
    {
        if(CurrentItem < (journal.Evidence.Count - 1))
        {
            Next.interactable = true;
        }
    }

    public void NextItem()
    {
        CurrentItem++;
        if (CurrentItem == (journal.Evidence.Count - 1))
        {
            Next.interactable = false;
        }

    }

    private void OnDisable()
    {
        CurrentItem = 0;
    }


}
