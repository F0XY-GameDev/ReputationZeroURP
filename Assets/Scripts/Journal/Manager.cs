using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IDiscoverable
{
    public bool Discovered { get; set; }
    public int ID { get; }
}

public interface IHiddenDescription
{
    public int OwnID { get; }
    public int TargetID { get; }
    public string Message { get; }
    public bool Discovered { get; set; }
}

public class Manager : MonoBehaviour
{
    [Header("Lists")]
    public List<IDiscoverable> Discoverables;
    public List<IConditional> Conditionals;
    public List<IHiddenDescription> EvidenceDescriptions;
    public List<EvidenceDescription> EvidenceDescriptionList = new List<EvidenceDescription>();
    public List<IHiddenDescription> Testimonies = new List<IHiddenDescription>();
    public List<Testimony> TestimoniesList = new List<Testimony>();
    [Header("Null Data")]
    public PersonData NoPersons;
    public EvidenceData NoEvidence;
    public EvidenceDescription NoDescriptions;
    public Testimony NoTestimonies;
    [Header("Progress Flags")]
    public bool xTutorialFinished;
    public bool yTutorialFinished;
    public bool isGrabTutorialFinished;
    public bool isRegisterTutorialFinished;

    void Awake()
    {
        EvidenceDescriptionList = EvidenceDescriptionList.OrderBy(x => x.DescriptionID).ToList();
        TestimoniesList = TestimoniesList.OrderBy(x => x.TestimonyID).ToList();
        Discoverables = new List<IDiscoverable>();
        Conditionals = new List<IConditional>();
        EvidenceDescriptions = new List<IHiddenDescription>();        
    }
    void Start()
    {
        Discoverables.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IDiscoverable>().ToList());
        Conditionals.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IConditional>().ToList());
        EvidenceDescriptions.AddRange(EvidenceDescriptionList);
        Testimonies.AddRange(TestimoniesList);
        Debug.Log($"Discoverables = {Discoverables}, Conditionals = {Conditionals}, EvidenceDescriptions = {EvidenceDescriptions}");
    }
    public void UpdateAllLists()
    {
        EvidenceDescriptionList = EvidenceDescriptionList.OrderBy(x => x.DescriptionID).ToList();
        TestimoniesList = TestimoniesList.OrderBy(x => x.TestimonyID).ToList();
        EvidenceDescriptions.Clear();
        Testimonies.Clear();
        EvidenceDescriptions.AddRange(EvidenceDescriptionList);
        Testimonies.AddRange(TestimoniesList);
    }
    public List<IDiscoverable> Discovered()
    {
        return Discoverables.Where<IDiscoverable>(x => x.Discovered).ToList();
    }
    public IConditional GetConditionalMetByID(int id)
    {
        var conditionalList = Conditionals.Where<IConditional>(x => x.isComplete).ToList();
        for (int i = 0; i < conditionalList.Count; i++)
        {
            if (conditionalList[i].ID == id)
            {
                return conditionalList[i];
            }
            else return null;
        }
        return null;
    }
    public List<EvidenceData> GetEvidence()
    {
        var evidence = Discoverables.Where(x => x.Discovered).OfType<Evidence>().Select(x => x.EvidenceData);
        if(!evidence.Any())
        {
            return new List<EvidenceData>() { NoEvidence };
        }
        else
        {
            return evidence.ToList();
        }
    }
    public List<EvidenceDescription> GetDescriptions()
    {
        var descriptions = EvidenceDescriptions.Where(x => x.Discovered).OfType<EvidenceDescription>();
        if (!descriptions.Any())
        {
            return new List<EvidenceDescription>() { NoDescriptions };
        }
        else
        {
            return descriptions.ToList();
        }
    }
    public List<EvidenceDescription> GetEvidenceDescriptionListByID(int id)
    {
        var temporaryEvidenceDescriptions = new List<EvidenceDescription>();
        foreach (EvidenceDescription description in EvidenceDescriptionList)
        {
            if (id == description.EvidenceID)
            {
                temporaryEvidenceDescriptions.Add(description);
            }
        }
        return temporaryEvidenceDescriptions;
    }
    public List<Testimony> GetTestimonyByID(int id)
    {
        foreach (Testimony testimony in TestimoniesList)
        {
            List<Testimony> testimonyList = new List<Testimony>();
            if (testimony.TestimonyID == id)
            {
                testimonyList.Add(testimony);                
            }
            return testimonyList;
        }
        return new List<Testimony>();
    }
    public List<Testimony> GetTestimonies()
    {
        var testimonies = Testimonies.Where(x => x.Discovered).OfType<Testimony>();
        if (!testimonies.Any())
        {
            return new List<Testimony>() { NoTestimonies };
        }
        else
        {
            return testimonies.ToList();
        }
    }
    public List<PersonData> GetPersons()
    {
        var persons =  Discoverables.Where(x => x.Discovered).OfType<Person>().Select(x => x.PersonData);
        if(!persons.Any())
        {
            return new List<PersonData>() { NoPersons };
        }
        else
        {
            return persons.ToList();
        }
    }
    public List<string> GetEvidenceDescriptionsByID(int id)
    {
        var descriptionsList = EvidenceDescriptions.Where<IHiddenDescription>(x => x.TargetID == id).ToList();
        if (!descriptionsList.Any())
        {
            return new List<string>();
        }
        List<string> secondDescriptionsList = new List<string>();
        var temporaryDescriptionsList = descriptionsList.Where<IHiddenDescription>(x => x.Discovered).ToList();
        foreach (var description in temporaryDescriptionsList)
        {
            secondDescriptionsList.Add(description.Message);
        }
        return secondDescriptionsList;
    }
    public List<string> GetTestimonyTextByID(int id)
    {
        var descriptionsList = Testimonies.Where<IHiddenDescription>(x => x.TargetID == id).ToList();
        if (!descriptionsList.Any())
        {
            return new List<string>();
        }
        List<string> secondDescriptionsList = new List<string>();
        var temporaryDescriptionsList = descriptionsList.Where<IHiddenDescription>(x => x.Discovered).ToList();
        foreach (var description in temporaryDescriptionsList)
        {
            secondDescriptionsList.Add(description.Message);
        }
        return secondDescriptionsList;
    }    
    public void UnlockTestimonyByID(int id)
    {
        foreach (Testimony testimony in TestimoniesList)
        {
            if (testimony.TestimonyID == id)
            {
                testimony.Discover();
            }
        }
        UpdateAllLists();
    }
    public void UnlockEvidenceDescriptionByID(int id)
    {
        foreach (EvidenceDescription description in EvidenceDescriptionList)
        {            
            if (description.DescriptionID == id)
            {
                description.Discover();
            }
        }
        UpdateAllLists();
    }
}
