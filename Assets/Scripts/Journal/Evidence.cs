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
    AudioSource audioSource;
    public AudioClip audioClip;

    public bool Discovered;
    bool IDiscoverable.Discovered { get => Discovered; set => Discovered = value; }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(BagEvidence);
        EvidenceData.EvidenceDescriptions = FindObjectOfType<Manager>().GetEvidenceDescriptionListByID(EvidenceData.EvidenceID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BagEvidence(ActivateEventArgs args)
    {
        if (Discovered == true) { Debug.Log(EvidenceData.ItemName + " already bagged"); return; }
        Discovered = true;        
        Debug.Log(EvidenceData.ItemName + " Bagged");
        audioSource.PlayOneShot(audioClip, 1f);
        FindObjectOfType<Journal>().UpdateEvidence();
        OnBagEvidence.Invoke();
    }

    void SetEvidenceConditions()
    {
        if (!isCondition) { return; }
    }
}
