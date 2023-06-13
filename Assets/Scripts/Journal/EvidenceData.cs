using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EvidenceData")]
public class EvidenceData : ScriptableObject
{
    public Sprite ItemPicture;
    public string ItemName;
    public int EvidenceID;
    public string FoundLocation;
    public List<EvidenceDescription> EvidenceDescriptions = new List<EvidenceDescription>();
    public List<string> Descriptions = new List<string>();
    public Manager manager;
    

    public void UpdateDescriptions()
    {
        
    }
}
