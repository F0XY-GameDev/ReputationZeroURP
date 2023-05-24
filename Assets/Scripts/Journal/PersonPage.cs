using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PersonPage : MonoBehaviour
{
    public int CurrentPerson = 0;
    public Journal journal;
    public TMPro.TextMeshProUGUI Relation;
    public TMPro.TextMeshProUGUI PersonName;
    public List<TMPro.TextMeshProUGUI> Testimony;
    public Image PersonImage;
    public Button Next;
    public InputActionReference pageFlip;
    public UnityEvent nextSection;

    
    // Update is called once per frame
    void Update()
    {
        PersonName.text = journal.Persons[CurrentPerson].Name;
        PersonImage.sprite = journal.Persons[CurrentPerson].Mugshot;
        Relation.text = journal.Persons[CurrentPerson].Relation;
        for (int i = 0; i < Testimony.Count; i++)
        {
            if (journal.Evidence[i] != null)
            {
                Testimony[i].text = journal.Persons[CurrentPerson].Descriptions[i];
            }
            else
            {
                break;
            }
        }
    }

    private void OnEnable()
    {
        pageFlip.action.started += NextPage;
        if (CurrentPerson < (journal.Persons.Count - 1))
        {
            Next.interactable = true;
        }
    }

    private void OnDisable()
    {
        pageFlip.action.started -= NextPage;
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
        if (CurrentPerson == (journal.Persons.Count - 1))
        {
            nextSection.Invoke();
            return;
        }
        CurrentPerson++;
        if (CurrentPerson == (journal.Persons.Count - 1))
        {
            Next.interactable = false;
        }

    }

    private void OnDisable()
    {
        CurrentPerson = 0;
    }
}
