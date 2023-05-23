using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EvidenceDescription")]
public class EvidenceDescription : ScriptableObject, IHiddenDescription
{
    public int EvidenceID;
    public int DescriptionID;
    public string Text;
    public bool Discovered;
    public int ConditionID;
    int IHiddenDescription.TargetID { get => EvidenceID; }
    string IHiddenDescription.Message { get => Text; }
    bool IHiddenDescription.Discovered { get => Discovered; set => Discovered = value; }
    int IHiddenDescription.OwnID { get => DescriptionID; }
}
