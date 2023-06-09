using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class Evidence : MonoBehaviour, IDiscoverable
{
    // Start is called before the first frame update
    public EvidenceData EvidenceData;    
    public bool isCondition;
    public int conditionID;
    public UnityEvent OnBagEvidence;
    public UnityEvent OnPlayerEnteredTrigger;
    public UnityEvent OnPlayerExitedTrigger;
    AudioSource audioSource;
    public AudioClip audioClip;


    public bool Discovered;
    bool IDiscoverable.Discovered { get => Discovered; set => Discovered = value; }
    int IDiscoverable.ID { get => EvidenceData.EvidenceID; }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(BagEvidence);
        EvidenceData.EvidenceDescriptions = FindObjectOfType<Manager>().GetEvidenceDescriptionListByID(EvidenceData.EvidenceID);
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            OnPlayerEnteredTrigger.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            OnPlayerExitedTrigger.Invoke();
        }
    }*/

    public void BagEvidence(ActivateEventArgs args)
    {
        if (Discovered == true) { Debug.Log(EvidenceData.ItemName + " already bagged"); return; }
        Discovered = true;        
        Debug.Log(EvidenceData.ItemName + " Bagged");
        audioSource.PlayOneShot(audioClip, 1f);
        FindObjectOfType<Journal>().UpdateEvidence();
        OnBagEvidence.Invoke();
        var manager = FindObjectOfType<Manager>();
        if (manager != null && !manager.hasEvidence)
        {
            FindObjectOfType<Manager>().hasEvidence = true;
            var allPersons = FindObjectsOfType<Person>();
            foreach (Person person in allPersons)
            {
                if (person.PersonData.PersonID == 7)
                {
                    var cop = person;
                    cop.SwapToPersonData(cop.alternatePersonData);
                }
            }
        }       
    }

    void SetEvidenceConditions()
    {
        if (!isCondition) { return; }
    }
}
