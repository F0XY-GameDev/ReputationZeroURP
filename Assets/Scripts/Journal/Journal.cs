using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Journal : MonoBehaviour
{
    public Manager Manager;
    public List<EvidenceData> Evidence;
    public List<PersonData> Persons;
    public List<List<Testimony>> TestimonyPerPerson;
    public Canvas CurrentPage;
    public Canvas CoverPage;
    public Canvas PeoplePage;
    public Canvas EvidencePage;
    public UnityEvent OnPageFlip;

    private void Awake()
    {
        CurrentPage = CoverPage;
        CurrentPage.enabled = true;
        Evidence = new List<EvidenceData>();
        Persons = new List<PersonData>();        
    }

    public void Start()
    {
        Evidence = Manager.GetEvidence();
        Persons = Manager.GetPersons();
    }

    public void ChangeCurrentPage(Canvas newPage)
    {
        CurrentPage.enabled = false;
        CurrentPage = newPage;
        CurrentPage.enabled = true;
        OnPageFlip.Invoke();
    }

    private void OnEnable()
    {
        Evidence = Manager.GetEvidence();
        Persons = Manager.GetPersons();
    }

    public void UpdateJournal()
    {
        UpdateEvidence();
        UpdatePersons();
    }

    public void UpdateEvidence()
    {
        Debug.Log("Evidence Updated");
        Evidence = Manager.GetEvidence();
    }

    public void UpdatePersons()
    {
        Debug.Log("Persons Updated");
        Persons = Manager.GetPersons();
    }
}
