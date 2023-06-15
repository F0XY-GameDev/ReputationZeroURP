using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

public class PersonPage : MonoBehaviour
{
    public int CurrentPerson = 0;
    public Journal journal;
    public Manager manager;
    public TMPro.TextMeshProUGUI Relation;
    public TMPro.TextMeshProUGUI PersonName;
    public List<TMPro.TextMeshProUGUI> Testimony;
    public List<Testimony> TestimonyList;
    public Image PersonImage;
    public Button Next;
    public InputActionReference pageFlip;
    public UnityEvent nextSection;
    public UnityEvent nextPage;
    public List<Testimony> linkedTestimony = new List<Testimony>();
    public PersonData personData;
    
    // Update is called once per frame
    void Update()
    {
        PersonName.text = journal.Persons[CurrentPerson].Name;
        PersonImage.sprite = journal.Persons[CurrentPerson].Mugshot;
        Relation.text = journal.Persons[CurrentPerson].Relation;
        personData = journal.Persons[CurrentPerson];
        linkedTestimony = GetTestimonyByPersonID(personData.PersonID);
        for (int i = 0; i < Testimony.Count; i++)
        {
            if (Testimony[i] != null) { Testimony[i].SetText(linkedTestimony[i].Text); } else { Testimony[i].SetText(""); }
            //Testimony[i].text = journal.Persons[CurrentPerson].Descriptions[i];
        }
    }
    public void AddTestimony(List<Testimony> testimony)
    {
        TestimonyList.AddRange(testimony);
        List<Testimony> sortedList = TestimonyList.OrderBy(x => x.TestimonyID).ToList();
        TestimonyList = sortedList;
    }
    public List<Testimony> GetTestimonyByPersonID(int id)
    {
        List<Testimony> list = TestimonyList.Where(x => x.PersonID == id).ToList();
        List<Testimony> secondlist = list.Where(x => x.Discovered == true).ToList();
        List<Testimony> sortedList = secondlist.OrderBy(x => x.TestimonyID).ToList();
        return list;
    }

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
        NextItem();
    }

    public void NextItem()
    {
        nextPage.Invoke();
        if (CurrentPerson == (journal.Persons.Count - 1))
        {
            nextSection.Invoke();
            return;
        }
        CurrentPerson++;
    }    
}
