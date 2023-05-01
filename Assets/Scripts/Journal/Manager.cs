using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface IDiscoverable
{
    public bool Discovered { get; set; }

}

public interface IHiddenDescription
{
    public int ID { get; }
    public int EvidenceID { get; }
    public string Message { get; }
    public bool Discovered { get; set; }
}



public class Manager : MonoBehaviour
{

    public List<IDiscoverable> Discoverables;
    public List<IConditional> Conditionals;
    public List<IHiddenDescription> EvidenceDescriptions;
    public List<EvidenceDescription> EvidenceDescriptionList;
    public PersonData NoPersons;
    public EvidenceData NoEvidence;


    private void Awake()
    {
        Discoverables = new List<IDiscoverable>();
        Conditionals = new List<IConditional>();
        EvidenceDescriptions = new List<IHiddenDescription>();        
    }
    // Start is called before the first frame update
    void Start()
    {
        Discoverables.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IDiscoverable>().ToList());
        Conditionals.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IConditional>().ToList());
        EvidenceDescriptions.AddRange(EvidenceDescriptionList);
        Debug.Log($"Discoverables = {Discoverables}, Conditionals = {Conditionals}, EvidenceDescriptions = {EvidenceDescriptions}");
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public List<string> GetEvidenceDescriptionsByID(int id)
    {
        var descriptionsList = EvidenceDescriptions.Where<IHiddenDescription>(x => x.EvidenceID == id).ToList();
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


}
